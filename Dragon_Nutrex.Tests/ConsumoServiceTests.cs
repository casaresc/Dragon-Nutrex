using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Moq;
using Xunit;

namespace Dragon_Nutrex.Tests;

public class ConsumoServiceTests
{
    private readonly Mock<IConsumoDiarioRepository> consumoRepositoryMock;
    private readonly ConsumoService consumoService;

    public ConsumoServiceTests()
    {
        consumoRepositoryMock = new Mock<IConsumoDiarioRepository>();
        consumoService = new ConsumoService(consumoRepositoryMock.Object);
    }

    [Theory]
    [InlineData(true)]
    public void RegistrarConsumo_CuandoEsValido_GeneraIdYGuarda(bool _)
    {
        var consumo = CrearConsumoValido();
        consumo.Id = Guid.Empty;

        consumoService.RegistrarConsumo(consumo);

        Assert.NotEqual(Guid.Empty, consumo.Id);
        consumoRepositoryMock.Verify(r => r.Create(consumo), Times.Once);
    }

    [Theory]
    [InlineData(-1)]
    public void RegistrarConsumo_CuandoCaloriasNegativas_LanzaExcepcion(decimal calorias)
    {
        var consumo = CrearConsumoValido();
        consumo.CaloriasConsumidas = calorias;

        Assert.Throws<ArgumentException>(() => consumoService.RegistrarConsumo(consumo));
    }

    [Theory]
    [InlineData(3)]
    public void RegistrarConsumosMasivos_CuandoListaEsValida_GuardaTodos(int cantidad)
    {
        var lista = Enumerable.Range(1, cantidad)
            .Select(_ => CrearConsumoValido())
            .ToList();

        consumoService.RegistrarConsumosMasivos(lista);

        consumoRepositoryMock.Verify(r => r.Create(It.IsAny<ConsumoDiario>()), Times.Exactly(cantidad));
    }

    [Theory]
    [InlineData(true)]
    public void EliminarConsumo_CuandoSeInvoca_EliminaPorId(bool _)
    {
        var id = Guid.NewGuid();

        consumoService.EliminarConsumo(id);

        consumoRepositoryMock.Verify(r => r.Delete(id), Times.Once);
    }

    [Theory]
    [InlineData(2000)]
    public void ObtenerResumenDiario_CuandoNoHayRegistros_RetornaValoresEnCero(decimal meta)
    {
        consumoRepositoryMock
            .Setup(r => r.GetAll())
            .Returns(new List<ConsumoDiario>());

        var resultado = consumoService.ObtenerResumenDiario(Guid.NewGuid(), DateTime.Today, meta);

        Assert.False(resultado.TieneRegistros);
        Assert.Equal(0, resultado.CaloriasConsumidas);
        Assert.Equal(meta, resultado.DiferenciaCalorias);
    }

    [Theory]
    [InlineData(2000)]
    public void ObtenerResumenDiario_CuandoHayRegistros_CalculaTotalesCorrectamente(decimal meta)
    {
        var usuarioId = Guid.NewGuid();

        var lista = new List<ConsumoDiario>
        {
            new ConsumoDiario
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.Today,
                CaloriasConsumidas = 1000,
                CarbohidratosConsumidos = 100,
                ProteinasConsumidas = 50,
                GrasasConsumidas = 30
            },
            new ConsumoDiario
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.Today,
                CaloriasConsumidas = 500,
                CarbohidratosConsumidos = 50,
                ProteinasConsumidas = 20,
                GrasasConsumidas = 10
            }
        };

        consumoRepositoryMock
            .Setup(r => r.GetAll())
            .Returns(lista);

        var resultado = consumoService.ObtenerResumenDiario(usuarioId, DateTime.Today, meta);

        Assert.True(resultado.TieneRegistros);
        Assert.Equal(1500, resultado.CaloriasConsumidas);
        Assert.Equal(150, resultado.CarbohidratosConsumidos);
        Assert.Equal(70, resultado.ProteinasConsumidas);
        Assert.Equal(40, resultado.GrasasConsumidas);
        Assert.Equal(meta - 1500, resultado.DiferenciaCalorias);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerResumenPorRango_CuandoFechaInicioMayor_LanzaExcepcion(bool _)
    {
        var usuarioId = Guid.NewGuid();

        Assert.Throws<ArgumentException>(() =>
            consumoService.ObtenerResumenPorRango(usuarioId, DateTime.Today, DateTime.Today.AddDays(-1)));
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerResumenPorRango_CuandoNoHayRegistros_RetornaVacio(bool _)
    {
        consumoRepositoryMock
            .Setup(r => r.GetAll())
            .Returns(new List<ConsumoDiario>());

        var resultado = consumoService.ObtenerResumenPorRango(
            Guid.NewGuid(),
            DateTime.Today.AddDays(-5),
            DateTime.Today);

        Assert.Equal(0, resultado.TotalCalorias);
        Assert.Equal(0, resultado.DiasConRegistros);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerResumenPorRango_CuandoHayRegistros_CalculaPromedios(bool _)
    {
        var usuarioId = Guid.NewGuid();

        var lista = new List<ConsumoDiario>
        {
            new ConsumoDiario
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.Today,
                CaloriasConsumidas = 1000,
                CarbohidratosConsumidos = 100,
                ProteinasConsumidas = 50,
                GrasasConsumidas = 30
            },
            new ConsumoDiario
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.Today.AddDays(-1),
                CaloriasConsumidas = 2000,
                CarbohidratosConsumidos = 200,
                ProteinasConsumidas = 100,
                GrasasConsumidas = 60
            }
        };

        consumoRepositoryMock
            .Setup(r => r.GetAll())
            .Returns(lista);

        var resultado = consumoService.ObtenerResumenPorRango(
            usuarioId,
            DateTime.Today.AddDays(-2),
            DateTime.Today);

        Assert.Equal(3000, resultado.TotalCalorias);
        Assert.Equal(2, resultado.DiasConRegistros);
        Assert.Equal(1500, resultado.PromedioCalorias);
    }

    private static ConsumoDiario CrearConsumoValido()
    {
        return new ConsumoDiario
        {
            Id = Guid.NewGuid(),
            UsuarioId = Guid.NewGuid(),
            Fecha = DateTime.Today,
            CaloriasConsumidas = 1000,
            CarbohidratosConsumidos = 100,
            ProteinasConsumidas = 50,
            GrasasConsumidas = 30,
            Activo = true
        };
    }
}
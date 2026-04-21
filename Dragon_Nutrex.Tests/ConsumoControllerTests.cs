using Dragon_Nutrex_Web.Core.Controllers;
using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Moq;
using Xunit;

namespace Dragon_Nutrex.Tests;

public class ConsumoControllerTests
{
    private readonly Mock<IConsumoDiarioRepository> consumoRepositoryMock;
    private readonly Mock<IRepository<Usuario>> usuarioRepositoryMock;

    private readonly ConsumoService consumoService;
    private readonly UsuarioService usuarioService;
    private readonly NutricionService nutricionService;
    private readonly ConsumoController consumoController;

    public ConsumoControllerTests()
    {
        consumoRepositoryMock = new Mock<IConsumoDiarioRepository>();
        usuarioRepositoryMock = new Mock<IRepository<Usuario>>();

        consumoService = new ConsumoService(consumoRepositoryMock.Object);
        usuarioService = new UsuarioService(usuarioRepositoryMock.Object);
        nutricionService = new NutricionService();

        consumoController = new ConsumoController(
            consumoService,
            usuarioService,
            nutricionService);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerResumenParaFecha_CuandoNoHayUsuarios_RetornaResumenVacio(bool _)
    {
        usuarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<Usuario>());

        var resultado = consumoController.ObtenerResumenParaFecha(DateTime.Today);

        Assert.False(resultado.TieneRegistros);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerResumenParaFecha_CuandoHayUsuario_RetornaResumenCalculado(bool _)
    {
        var usuario = CrearUsuarioValido();

        usuarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<Usuario> { usuario });

        consumoRepositoryMock
            .Setup(repository => repository.GetByDate(DateTime.Today))
            .Returns(new List<ConsumoDiario>
            {
                new ConsumoDiario
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuario.Id,
                    Fecha = DateTime.Today,
                    CaloriasConsumidas = 1500m,
                    CarbohidratosConsumidos = 150m,
                    ProteinasConsumidas = 75m,
                    GrasasConsumidas = 50m,
                    Activo = true
                }
            });

        var resultado = consumoController.ObtenerResumenParaFecha(DateTime.Today);

        Assert.True(resultado.TieneRegistros);
        Assert.Equal(1500m, resultado.CaloriasConsumidas);
        Assert.True(resultado.MetaCalorias > 0);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerResumenParaUsuarioYFecha_CuandoUsuarioNoExiste_RetornaResumenVacio(bool _)
    {
        var usuarioId = Guid.NewGuid();

        usuarioRepositoryMock
            .Setup(repository => repository.GetById(usuarioId))
            .Returns((Usuario?)null);

        var resultado = consumoController.ObtenerResumenParaUsuarioYFecha(usuarioId, DateTime.Today);

        Assert.False(resultado.TieneRegistros);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerResumenParaUsuarioYFecha_CuandoUsuarioExiste_RetornaResumenCalculado(bool _)
    {
        var usuario = CrearUsuarioValido();

        usuarioRepositoryMock
            .Setup(repository => repository.GetById(usuario.Id))
            .Returns(usuario);

        consumoRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<ConsumoDiario>
            {
                new ConsumoDiario
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuario.Id,
                    Fecha = DateTime.Today,
                    CaloriasConsumidas = 1800m,
                    CarbohidratosConsumidos = 180m,
                    ProteinasConsumidas = 90m,
                    GrasasConsumidas = 60m,
                    Activo = true
                }
            });

        var resultado = consumoController.ObtenerResumenParaUsuarioYFecha(usuario.Id, DateTime.Today);

        Assert.True(resultado.TieneRegistros);
        Assert.Equal(1800m, resultado.CaloriasConsumidas);
        Assert.True(resultado.MetaCalorias > 0);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerEstadisticasRangoPorUsuario_CuandoHayDatos_RetornaResumen(bool _)
    {
        var usuarioId = Guid.NewGuid();

        consumoRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<ConsumoDiario>
            {
                new ConsumoDiario
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuarioId,
                    Fecha = DateTime.Today,
                    CaloriasConsumidas = 1000m,
                    CarbohidratosConsumidos = 100m,
                    ProteinasConsumidas = 50m,
                    GrasasConsumidas = 30m,
                    Activo = true
                },
                new ConsumoDiario
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuarioId,
                    Fecha = DateTime.Today.AddDays(-1),
                    CaloriasConsumidas = 2000m,
                    CarbohidratosConsumidos = 200m,
                    ProteinasConsumidas = 100m,
                    GrasasConsumidas = 60m,
                    Activo = true
                }
            });

        var resultado = consumoController.ObtenerEstadisticasRangoPorUsuario(
            usuarioId,
            DateTime.Today.AddDays(-2),
            DateTime.Today);

        Assert.Equal(3000m, resultado.TotalCalorias);
        Assert.Equal(2, resultado.DiasConRegistros);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerEstadisticasRango_CuandoHayDatos_RetornaResumen(bool _)
    {
        consumoRepositoryMock
            .Setup(repository => repository.GetByRange(
                DateTime.Today.AddDays(-2),
                DateTime.Today))
            .Returns(new List<ConsumoDiario>
            {
                new ConsumoDiario
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = Guid.NewGuid(),
                    Fecha = DateTime.Today,
                    CaloriasConsumidas = 2000m,
                    CarbohidratosConsumidos = 200m,
                    ProteinasConsumidas = 100m,
                    GrasasConsumidas = 60m,
                    Activo = true
                },
                new ConsumoDiario
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = Guid.NewGuid(),
                    Fecha = DateTime.Today.AddDays(-1),
                    CaloriasConsumidas = 3000m,
                    CarbohidratosConsumidos = 300m,
                    ProteinasConsumidas = 150m,
                    GrasasConsumidas = 90m,
                    Activo = true
                }
            });

        var resultado = consumoController.ObtenerEstadisticasRango(
            DateTime.Today.AddDays(-2),
            DateTime.Today);

        Assert.Equal(5000m, resultado.TotalCalorias);
        Assert.Equal(2, resultado.DiasConRegistros);
    }

    [Theory]
    [InlineData(true)]
    public void RegistrarNuevoConsumo_CuandoEsValido_GuardaConsumo(bool _)
    {
        var consumo = new ConsumoDiario
        {
            Id = Guid.NewGuid(),
            UsuarioId = Guid.NewGuid(),
            Fecha = DateTime.Today,
            CaloriasConsumidas = 1000m,
            CarbohidratosConsumidos = 100m,
            ProteinasConsumidas = 50m,
            GrasasConsumidas = 30m,
            Activo = true
        };

        consumoController.RegistrarNuevoConsumo(consumo);

        consumoRepositoryMock.Verify(repository => repository.Create(consumo), Times.Once);
    }

    private static Usuario CrearUsuarioValido()
    {
        return new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = "Usuario Test",
            Correo = "usuario@test.com",
            Contrasena = "1234",
            Rol = "Usuario",
            Peso = 70m,
            Altura = 1.75m,
            Edad = 25,
            NivelActividad = NivelActividad.Moderado,
            Objetivo = ObjetivoNutricional.MantenerPeso,
            TipoDieta = TipoDieta.Balanceada,
            Activo = true
        };
    }
}
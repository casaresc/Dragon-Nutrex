using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Moq;
using Xunit;

namespace Dragon_Nutrex.Tests;

public class AdminEstadisticasServiceTests
{
    private readonly Mock<IRepository<Usuario>> usuarioRepositoryMock;
    private readonly Mock<IRepository<Producto>> productoRepositoryMock;
    private readonly Mock<IRepository<MenuDiario>> menuDiarioRepositoryMock;
    private readonly Mock<IRepository<MenuDetalle>> menuDetalleRepositoryMock;
    private readonly AdminEstadisticasService adminEstadisticasService;

    public AdminEstadisticasServiceTests()
    {
        usuarioRepositoryMock = new Mock<IRepository<Usuario>>();
        productoRepositoryMock = new Mock<IRepository<Producto>>();
        menuDiarioRepositoryMock = new Mock<IRepository<MenuDiario>>();
        menuDetalleRepositoryMock = new Mock<IRepository<MenuDetalle>>();

        adminEstadisticasService = new AdminEstadisticasService(
            usuarioRepositoryMock.Object,
            productoRepositoryMock.Object,
            menuDiarioRepositoryMock.Object,
            menuDetalleRepositoryMock.Object);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerProductoMasConsumido_CuandoFechaInicioEsMayor_LanzaExcepcion(bool _)
    {
        Assert.Throws<ArgumentException>(() =>
            adminEstadisticasService.ObtenerProductoMasConsumido(
                DateTime.Today,
                DateTime.Today.AddDays(-1)));
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerProductoMasConsumido_CuandoNoHayMenus_RetornaNull(bool _)
    {
        menuDiarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDiario>());

        var resultado = adminEstadisticasService.ObtenerProductoMasConsumido(
            DateTime.Today.AddDays(-7),
            DateTime.Today);

        Assert.Null(resultado);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerProductoMasConsumido_CuandoNoHayDetalles_RetornaNull(bool _)
    {
        var menu = new MenuDiario
        {
            Id = Guid.NewGuid(),
            UsuarioId = Guid.NewGuid(),
            Nombre = "Menú Test",
            Fecha = DateTime.Today,
            Activo = true
        };

        menuDiarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDiario> { menu });

        menuDetalleRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDetalle>());

        var resultado = adminEstadisticasService.ObtenerProductoMasConsumido(
            DateTime.Today.AddDays(-7),
            DateTime.Today);

        Assert.Null(resultado);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerProductoMasConsumido_CuandoHayDatos_RetornaProductoCorrecto(bool _)
    {
        var producto1Id = Guid.NewGuid();
        var producto2Id = Guid.NewGuid();
        var menuId = Guid.NewGuid();

        menuDiarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDiario>
            {
                new MenuDiario
                {
                    Id = menuId,
                    UsuarioId = Guid.NewGuid(),
                    Nombre = "Menú Test",
                    Fecha = DateTime.Today,
                    Activo = true
                }
            });

        menuDetalleRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDetalle>
            {
                new MenuDetalle
                {
                    Id = Guid.NewGuid(),
                    MenuId = menuId,
                    ProductoId = producto1Id,
                    Porcion = 2
                },
                new MenuDetalle
                {
                    Id = Guid.NewGuid(),
                    MenuId = menuId,
                    ProductoId = producto1Id,
                    Porcion = 3
                },
                new MenuDetalle
                {
                    Id = Guid.NewGuid(),
                    MenuId = menuId,
                    ProductoId = producto2Id,
                    Porcion = 1
                }
            });

        productoRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<Producto>
            {
                new Producto
                {
                    Id = producto1Id,
                    Nombre = "Pollo",
                    Categoria = CategoriaProducto.Proteina,
                    Proteina = 10,
                    Carbohidratos = 0,
                    Grasas = 2,
                    PorcionGramos = 100,
                    Calorias = 58,
                    Activo = true
                },
                new Producto
                {
                    Id = producto2Id,
                    Nombre = "Arroz",
                    Categoria = CategoriaProducto.Carbohidrato,
                    Proteina = 2,
                    Carbohidratos = 20,
                    Grasas = 1,
                    PorcionGramos = 100,
                    Calorias = 97,
                    Activo = true
                }
            });

        var resultado = adminEstadisticasService.ObtenerProductoMasConsumido(
            DateTime.Today.AddDays(-7),
            DateTime.Today);

        Assert.NotNull(resultado);
        Assert.Equal(producto1Id, resultado!.ProductoId);
        Assert.Equal("Pollo", resultado.NombreProducto);
        Assert.Equal(5, resultado.TotalPorciones);
        Assert.Equal(2, resultado.TotalRegistros);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerPorcentajeTiposDieta_CuandoNoHayUsuarios_RetornaListaVacia(bool _)
    {
        usuarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<Usuario>());

        var resultado = adminEstadisticasService.ObtenerPorcentajeTiposDieta();

        Assert.Empty(resultado);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerPorcentajeTiposDieta_CuandoHayUsuarios_RetornaPorcentajesCorrectos(bool _)
    {
        usuarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<Usuario>
            {
                CrearUsuario(TipoDieta.Balanceada),
                CrearUsuario(TipoDieta.Balanceada),
                CrearUsuario(TipoDieta.Cetogenica),
                CrearUsuario(TipoDieta.AltaEnCarbohidratos)
            });

        var resultado = adminEstadisticasService.ObtenerPorcentajeTiposDieta();

        Assert.Equal(3, resultado.Count);

        var balanceada = resultado.FirstOrDefault(item => item.TipoDieta == TipoDieta.Balanceada.ToString());
        var cetogenica = resultado.FirstOrDefault(item => item.TipoDieta == TipoDieta.Cetogenica.ToString());
        var altaEnCarbohidratos = resultado.FirstOrDefault(item => item.TipoDieta == TipoDieta.AltaEnCarbohidratos.ToString());

        Assert.NotNull(balanceada);
        Assert.Equal(2, balanceada!.CantidadUsuarios);
        Assert.Equal(50.00m, balanceada.Porcentaje);

        Assert.NotNull(cetogenica);
        Assert.Equal(1, cetogenica!.CantidadUsuarios);
        Assert.Equal(25.00m, cetogenica.Porcentaje);

        Assert.NotNull(altaEnCarbohidratos);
        Assert.Equal(1, altaEnCarbohidratos!.CantidadUsuarios);
        Assert.Equal(25.00m, altaEnCarbohidratos.Porcentaje);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerUsuariosConMasMenus_CuandoNoHayMenus_RetornaListaVacia(bool _)
    {
        usuarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<Usuario>());

        menuDiarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDiario>());

        var resultado = adminEstadisticasService.ObtenerUsuariosConMasMenus();

        Assert.Empty(resultado);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerUsuariosConMasMenus_CuandoHayDatos_RetornaUsuariosOrdenados(bool _)
    {
        var usuario1 = CrearUsuario(TipoDieta.Balanceada);
        var usuario2 = CrearUsuario(TipoDieta.Cetogenica);

        usuarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<Usuario> { usuario1, usuario2 });

        menuDiarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDiario>
            {
                new MenuDiario { Id = Guid.NewGuid(), UsuarioId = usuario1.Id, Nombre = "Menú 1", Fecha = DateTime.Today, Activo = true },
                new MenuDiario { Id = Guid.NewGuid(), UsuarioId = usuario1.Id, Nombre = "Menú 2", Fecha = DateTime.Today.AddDays(-1), Activo = true },
                new MenuDiario { Id = Guid.NewGuid(), UsuarioId = usuario2.Id, Nombre = "Menú 3", Fecha = DateTime.Today, Activo = true }
            });

        var resultado = adminEstadisticasService.ObtenerUsuariosConMasMenus();

        Assert.Equal(2, resultado.Count);
        Assert.Equal(usuario1.Id, resultado[0].UsuarioId);
        Assert.Equal(usuario1.Nombre, resultado[0].NombreUsuario);
        Assert.Equal(2, resultado[0].CantidadMenus);

        Assert.Equal(usuario2.Id, resultado[1].UsuarioId);
        Assert.Equal(1, resultado[1].CantidadMenus);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerUsuariosConMasMenus_CuandoUsuarioNoExiste_UsaNombreDesconocido(bool _)
    {
        var usuarioIdSinNombre = Guid.NewGuid();

        usuarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<Usuario>());

        menuDiarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDiario>
            {
                new MenuDiario
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuarioIdSinNombre,
                    Nombre = "Menú Test",
                    Fecha = DateTime.Today,
                    Activo = true
                }
            });

        var resultado = adminEstadisticasService.ObtenerUsuariosConMasMenus();

        Assert.Single(resultado);
        Assert.Equal("Usuario desconocido", resultado[0].NombreUsuario);
    }

    private static Usuario CrearUsuario(TipoDieta tipoDieta)
    {
        return new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = $"Usuario {tipoDieta}",
            Correo = $"{Guid.NewGuid()}@test.com",
            Contrasena = "1234",
            Rol = "Usuario",
            Peso = 70m,
            Altura = 1.75m,
            Edad = 25,
            NivelActividad = NivelActividad.Moderado,
            Objetivo = ObjetivoNutricional.MantenerPeso,
            TipoDieta = tipoDieta,
            Activo = true
        };
    }
}
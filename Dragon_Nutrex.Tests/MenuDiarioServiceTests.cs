using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Moq;
using Xunit;

namespace Dragon_Nutrex.Tests;

public class MenuDiarioServiceTests
{
    private readonly Mock<IMenuDiarioRepository> menuRepositoryMock;
    private readonly Mock<IRepository<MenuDetalle>> menuDetalleRepositoryMock;
    private readonly MenuDiarioService menuDiarioService;

    public MenuDiarioServiceTests()
    {
        menuRepositoryMock = new Mock<IMenuDiarioRepository>();
        menuDetalleRepositoryMock = new Mock<IRepository<MenuDetalle>>();

        menuDiarioService = new MenuDiarioService(
            menuRepositoryMock.Object,
            menuDetalleRepositoryMock.Object);
    }

    [Theory]
    [InlineData(2)]
    public void ObtenerMenus_CuandoSeInvoca_RetornaLista(int cantidadEsperada)
    {
        menuRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDiario>
            {
                CrearMenuValido(),
                CrearMenuValido()
            });

        var resultado = menuDiarioService.ObtenerMenus();

        Assert.Equal(cantidadEsperada, resultado.Count);
        menuRepositoryMock.Verify(repository => repository.GetAll(), Times.Once);
    }

    [Theory]
    [InlineData(2)]
    public void ObtenerTodos_CuandoSeInvoca_RetornaLista(int cantidadEsperada)
    {
        menuRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDiario>
            {
                CrearMenuValido(),
                CrearMenuValido()
            });

        var resultado = menuDiarioService.ObtenerTodos();

        Assert.Equal(cantidadEsperada, resultado.Count);
        menuRepositoryMock.Verify(repository => repository.GetAll(), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerPorId_CuandoExiste_RetornaMenu(bool _)
    {
        var menu = CrearMenuValido();

        menuRepositoryMock
            .Setup(repository => repository.GetById(menu.Id))
            .Returns(menu);

        var resultado = menuDiarioService.ObtenerPorId(menu.Id);

        Assert.NotNull(resultado);
        Assert.Equal(menu.Id, resultado!.Id);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerPorUsuarioYFecha_CuandoExiste_RetornaMenu(bool _)
    {
        var menu = CrearMenuValido();

        menuRepositoryMock
            .Setup(repository => repository.GetByUsuarioYFecha(menu.UsuarioId, menu.Fecha))
            .Returns(menu);

        var resultado = menuDiarioService.ObtenerPorUsuarioYFecha(menu.UsuarioId, menu.Fecha);

        Assert.NotNull(resultado);
        Assert.Equal(menu.UsuarioId, resultado!.UsuarioId);
        Assert.Equal(menu.Fecha, resultado.Fecha);
    }

    [Theory]
    [InlineData(true)]
    public void CrearMenu_CuandoEsValido_YNoTieneId_GeneraIdYGuarda(bool _)
    {
        var menu = CrearMenuValido();
        menu.Id = Guid.Empty;

        menuDiarioService.CrearMenu(menu);

        Assert.NotEqual(Guid.Empty, menu.Id);
        menuRepositoryMock.Verify(repository => repository.Create(menu), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void CrearMenu_CuandoTieneDetalles_GuardaMenuYDetalles(bool _)
    {
        var menu = CrearMenuValido();
        menu.Id = Guid.Empty;

        var detalles = new List<MenuDetalle>
        {
            new MenuDetalle
            {
                Id = Guid.Empty,
                MenuId = Guid.Empty,
                ProductoId = Guid.NewGuid(),
                Porcion = 2
            },
            new MenuDetalle
            {
                Id = Guid.Empty,
                MenuId = Guid.Empty,
                ProductoId = Guid.NewGuid(),
                Porcion = 1
            }
        };

        menuDiarioService.CrearMenu(menu, detalles);

        Assert.NotEqual(Guid.Empty, menu.Id);
        Assert.All(detalles, detalle =>
        {
            Assert.NotEqual(Guid.Empty, detalle.Id);
            Assert.Equal(menu.Id, detalle.MenuId);
        });

        menuRepositoryMock.Verify(repository => repository.Create(menu), Times.Once);
        menuDetalleRepositoryMock.Verify(repository => repository.Create(It.IsAny<MenuDetalle>()), Times.Exactly(2));
    }

    [Theory]
    [InlineData("", "El nombre del menú es obligatorio.")]
    public void CrearMenu_CuandoNombreEsInvalido_LanzaExcepcion(
        string nombre,
        string mensajeEsperado)
    {
        var menu = CrearMenuValido();
        menu.Nombre = nombre;

        var ex = Assert.Throws<ArgumentException>(() => menuDiarioService.CrearMenu(menu));

        Assert.Contains(mensajeEsperado, ex.Message);
        menuRepositoryMock.Verify(repository => repository.Create(It.IsAny<MenuDiario>()), Times.Never);
    }

    [Theory]
    [InlineData(true, "El menú debe estar asociado a un usuario.")]
    public void CrearMenu_CuandoUsuarioEsInvalido_LanzaExcepcion(
        bool _,
        string mensajeEsperado)
    {
        var menu = CrearMenuValido();
        menu.UsuarioId = Guid.Empty;

        var ex = Assert.Throws<ArgumentException>(() => menuDiarioService.CrearMenu(menu));

        Assert.Contains(mensajeEsperado, ex.Message);
        menuRepositoryMock.Verify(repository => repository.Create(It.IsAny<MenuDiario>()), Times.Never);
    }

    [Theory]
    [InlineData(true)]
    public void ActualizarMenu_CuandoEsValido_InvocaUpdate(bool _)
    {
        var menu = CrearMenuValido();

        menuDiarioService.ActualizarMenu(menu);

        menuRepositoryMock.Verify(repository => repository.Update(menu), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void EliminarMenu_CuandoSeInvoca_EliminaPorId(bool _)
    {
        var menuId = Guid.NewGuid();

        menuDiarioService.EliminarMenu(menuId);

        menuRepositoryMock.Verify(repository => repository.Delete(menuId), Times.Once);
    }

    private static MenuDiario CrearMenuValido()
    {
        return new MenuDiario
        {
            Id = Guid.NewGuid(),
            UsuarioId = Guid.NewGuid(),
            Nombre = "Menú Test",
            Fecha = DateTime.Today,
            Activo = true
        };
    }
}
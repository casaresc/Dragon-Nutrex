using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Moq;
using Xunit;

namespace Dragon_Nutrex.Tests;

public class MenuDetalleServiceTests
{
    private readonly Mock<IMenuDetalleRepository> detalleRepositoryMock;
    private readonly MenuDetalleService menuDetalleService;

    public MenuDetalleServiceTests()
    {
        detalleRepositoryMock = new Mock<IMenuDetalleRepository>();
        menuDetalleService = new MenuDetalleService(detalleRepositoryMock.Object);
    }

    [Theory]
    [InlineData(2)]
    public void ObtenerTodos_CuandoSeInvoca_RetornaLista(int cantidadEsperada)
    {
        detalleRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(new List<MenuDetalle>
            {
                CrearDetalleValido(),
                CrearDetalleValido()
            });

        var resultado = menuDetalleService.ObtenerTodos();

        Assert.Equal(cantidadEsperada, resultado.Count);
        detalleRepositoryMock.Verify(repository => repository.GetAll(), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerPorMenu_CuandoSeInvoca_RetornaDetalles(bool _)
    {
        var menuId = Guid.NewGuid();

        detalleRepositoryMock
            .Setup(repository => repository.GetByMenu(menuId))
            .Returns(new List<MenuDetalle>
            {
                CrearDetalleValido(menuId),
                CrearDetalleValido(menuId)
            });

        var resultado = menuDetalleService.ObtenerPorMenu(menuId);

        Assert.Equal(2, resultado.Count);
        Assert.All(resultado, detalle => Assert.Equal(menuId, detalle.MenuId));
    }

    [Theory]
    [InlineData(true)]
    public void AgregarProducto_CuandoEsValido_YNoTieneId_GeneraIdYGuarda(bool _)
    {
        var detalle = CrearDetalleValido();
        detalle.Id = Guid.Empty;

        menuDetalleService.AgregarProducto(detalle);

        Assert.NotEqual(Guid.Empty, detalle.Id);
        detalleRepositoryMock.Verify(repository => repository.Create(detalle), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void ActualizarDetalle_CuandoEsValido_InvocaUpdate(bool _)
    {
        var detalle = CrearDetalleValido();

        menuDetalleService.ActualizarDetalle(detalle);

        detalleRepositoryMock.Verify(repository => repository.Update(detalle), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void EliminarProducto_CuandoSeInvoca_EliminaPorId(bool _)
    {
        var detalleId = Guid.NewGuid();

        menuDetalleService.EliminarProducto(detalleId);

        detalleRepositoryMock.Verify(repository => repository.Delete(detalleId), Times.Once);
    }

    [Theory]
    [InlineData(true, "El detalle debe estar asociado a un menú.")]
    public void AgregarProducto_CuandoMenuIdEsInvalido_LanzaExcepcion(
        bool _,
        string mensajeEsperado)
    {
        var detalle = CrearDetalleValido();
        detalle.MenuId = Guid.Empty;

        var ex = Assert.Throws<Exception>(() => menuDetalleService.AgregarProducto(detalle));

        Assert.Equal(mensajeEsperado, ex.Message);
        detalleRepositoryMock.Verify(repository => repository.Create(It.IsAny<MenuDetalle>()), Times.Never);
    }

    [Theory]
    [InlineData(true, "El detalle debe estar asociado a un producto.")]
    public void AgregarProducto_CuandoProductoIdEsInvalido_LanzaExcepcion(
        bool _,
        string mensajeEsperado)
    {
        var detalle = CrearDetalleValido();
        detalle.ProductoId = Guid.Empty;

        var ex = Assert.Throws<Exception>(() => menuDetalleService.AgregarProducto(detalle));

        Assert.Equal(mensajeEsperado, ex.Message);
        detalleRepositoryMock.Verify(repository => repository.Create(It.IsAny<MenuDetalle>()), Times.Never);
    }

    [Theory]
    [InlineData(0, "La porción debe ser mayor a cero.")]
    [InlineData(-1, "La porción debe ser mayor a cero.")]
    public void AgregarProducto_CuandoPorcionEsInvalida_LanzaExcepcion(
        decimal porcion,
        string mensajeEsperado)
    {
        var detalle = CrearDetalleValido();
        detalle.Porcion = porcion;

        var ex = Assert.Throws<Exception>(() => menuDetalleService.AgregarProducto(detalle));

        Assert.Equal(mensajeEsperado, ex.Message);
        detalleRepositoryMock.Verify(repository => repository.Create(It.IsAny<MenuDetalle>()), Times.Never);
    }

    private static MenuDetalle CrearDetalleValido(Guid? menuId = null)
    {
        return new MenuDetalle
        {
            Id = Guid.NewGuid(),
            MenuId = menuId ?? Guid.NewGuid(),
            ProductoId = Guid.NewGuid(),
            Porcion = 2
        };
    }
}
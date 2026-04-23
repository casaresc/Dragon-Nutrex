using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Moq;
using Xunit;

namespace Dragon_Nutrex.Tests;

public class ProductoServiceTests
{
    private readonly Mock<IRepository<Producto>> productoRepositoryMock;
    private readonly ProductoService productoService;

    public ProductoServiceTests()
    {
        productoRepositoryMock = new Mock<IRepository<Producto>>();
        productoService = new ProductoService(productoRepositoryMock.Object);
    }

    [Theory]
    [InlineData(2)]
    public void ObtenerProductos_CuandoSeInvoca_RetornaListaDeProductos(int cantidadEsperada)
    {
        var productos = new List<Producto>
        {
            CrearProductoValido(),
            CrearProductoValido()
        };

        productoRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(productos);

        var resultado = productoService.ObtenerProductos();

        Assert.Equal(cantidadEsperada, resultado.Count);
        productoRepositoryMock.Verify(repository => repository.GetAll(), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void CrearProducto_CuandoEsValido_YNoTieneId_GeneraId_CalculaCalorias_YLoCrea(bool _)
    {
        var producto = CrearProductoValido();
        producto.Id = Guid.Empty;

        productoService.CrearProducto(producto);

        Assert.NotEqual(Guid.Empty, producto.Id);
        Assert.Equal(165m, producto.Calorias);
        productoRepositoryMock.Verify(repository => repository.Create(producto), Times.Once);
    }

    [Theory]
    [InlineData("", 10, 20, 5, 100, "El nombre del producto es obligatorio.")]
    public void CrearProducto_CuandoNombreEsInvalido_LanzaExcepcion(
        string nombre,
        decimal proteina,
        decimal carbohidratos,
        decimal grasas,
        decimal porcionGramos,
        string mensajeEsperado)
    {
        var producto = CrearProductoValido();
        producto.Nombre = nombre;
        producto.Proteina = proteina;
        producto.Carbohidratos = carbohidratos;
        producto.Grasas = grasas;
        producto.PorcionGramos = porcionGramos;

        var ex = Assert.Throws<ArgumentException>(() => productoService.CrearProducto(producto));

        Assert.Contains(mensajeEsperado, ex.Message);
        productoRepositoryMock.Verify(repository => repository.Create(It.IsAny<Producto>()), Times.Never);
    }

    [Theory]
    [InlineData(-1, 20, 5, 100, "Los macronutrientes no pueden ser negativos.")]
    [InlineData(10, -1, 5, 100, "Los macronutrientes no pueden ser negativos.")]
    [InlineData(10, 20, -1, 100, "Los macronutrientes no pueden ser negativos.")]
    public void CrearProducto_CuandoMacronutrientesSonInvalidos_LanzaExcepcion(
        decimal proteina,
        decimal carbohidratos,
        decimal grasas,
        decimal porcionGramos,
        string mensajeEsperado)
    {
        var producto = CrearProductoValido();
        producto.Proteina = proteina;
        producto.Carbohidratos = carbohidratos;
        producto.Grasas = grasas;
        producto.PorcionGramos = porcionGramos;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => productoService.CrearProducto(producto));

        Assert.Contains(mensajeEsperado, ex.Message);
        productoRepositoryMock.Verify(repository => repository.Create(It.IsAny<Producto>()), Times.Never);
    }

    [Theory]
    [InlineData(0, "La porción debe ser mayor a 0.")]
    [InlineData(-10, "La porción debe ser mayor a 0.")]
    public void CrearProducto_CuandoPorcionEsInvalida_LanzaExcepcion(
        decimal porcionGramos,
        string mensajeEsperado)
    {
        var producto = CrearProductoValido();
        producto.PorcionGramos = porcionGramos;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => productoService.CrearProducto(producto));

        Assert.Contains(mensajeEsperado, ex.Message);
        productoRepositoryMock.Verify(repository => repository.Create(It.IsAny<Producto>()), Times.Never);
    }

    [Theory]
    [InlineData(true)]
    public void ActualizarProducto_CuandoEsValido_CalculaCalorias_EInvocaUpdate(bool _)
    {
        var producto = CrearProductoValido();
        producto.Proteina = 20m;
        producto.Carbohidratos = 10m;
        producto.Grasas = 5m;

        productoService.ActualizarProducto(producto);

        Assert.Equal(165m, producto.Calorias);
        productoRepositoryMock.Verify(repository => repository.Update(producto), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void EliminarProducto_CuandoSeInvoca_EliminaPorId(bool _)
    {
        var productoId = Guid.NewGuid();

        productoService.EliminarProducto(productoId);

        productoRepositoryMock.Verify(repository => repository.Delete(productoId), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void DesactivarProducto_CuandoProductoExiste_CambiaActivoAFalso(bool _)
    {
        var producto = CrearProductoValido();
        producto.Activo = true;

        productoRepositoryMock
            .Setup(repository => repository.GetById(producto.Id))
            .Returns(producto);

        productoService.DesactivarProducto(producto.Id);

        Assert.False(producto.Activo);
        productoRepositoryMock.Verify(repository => repository.Update(producto), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void DesactivarProducto_CuandoProductoNoExiste_LanzaExcepcion(bool _)
    {
        var productoId = Guid.NewGuid();

        productoRepositoryMock
            .Setup(repository => repository.GetById(productoId))
            .Returns((Producto?)null);

        var ex = Assert.Throws<KeyNotFoundException>(() => productoService.DesactivarProducto(productoId));

        Assert.Contains("no encontrado", ex.Message);
    }

    [Theory]
    [InlineData(true)]
    public void ActivarProducto_CuandoProductoExiste_CambiaActivoAVerdadero(bool _)
    {
        var producto = CrearProductoValido();
        producto.Activo = false;

        productoRepositoryMock
            .Setup(repository => repository.GetById(producto.Id))
            .Returns(producto);

        productoService.ActivarProducto(producto.Id);

        Assert.True(producto.Activo);
        productoRepositoryMock.Verify(repository => repository.Update(producto), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void ActivarProducto_CuandoProductoNoExiste_LanzaExcepcion(bool _)
    {
        var productoId = Guid.NewGuid();

        productoRepositoryMock
            .Setup(repository => repository.GetById(productoId))
            .Returns((Producto?)null);

        var ex = Assert.Throws<KeyNotFoundException>(() => productoService.ActivarProducto(productoId));

        Assert.Contains("no encontrado", ex.Message);
    }

    private static Producto CrearProductoValido()
    {
        return new Producto
        {
            Id = Guid.NewGuid(),
            Nombre = "Producto Test",
            Categoria = CategoriaProducto.Proteina,
            Proteina = 10m,
            Carbohidratos = 20m,
            Grasas = 5m,
            PorcionGramos = 100m,
            Calorias = 0m,
            Activo = true
        };
    }
}
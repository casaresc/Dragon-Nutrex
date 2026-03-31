using Xunit;
using Dragon_Nutrex.Services;
using Dragon_Nutrex.Models;
using System.Linq;

namespace Dragon_Nutrex.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void UsuarioCRUD_Flow_ShouldWork()
        {
            var service = new UsuarioService();
            var nuevoId = Guid.NewGuid();
            var usuario = new Usuario { Id = nuevoId, Nombre = "Test User", Peso = 75, Altura = 1.80m, Edad = 25, NivelActividad = NivelActividad.Moderado, Objetivo = ObjetivoNutricional.GanarPeso, TipoDieta = TipoDieta.Balanceada, Activo = true };

            // Create
            service.CrearUsuario(usuario);
            var recuperado = service.ObtenerPorId(nuevoId);
            Assert.NotNull(recuperado);
            Assert.Equal("Test User", recuperado.Nombre);

            // Update
            usuario.Nombre = "Updated Name";
            service.ActualizarUsuario(usuario);
            Assert.Equal("Updated Name", service.ObtenerPorId(nuevoId)?.Nombre);

            // Delete
            service.EliminarUsuario(nuevoId);
            Assert.Null(service.ObtenerPorId(nuevoId));
        }

        [Fact]
        public void ProductoCRUD_Flow_ShouldWork()
        {
            var service = new ProductoService();
            var nuevoId = Guid.NewGuid();
            var producto = new Producto
            {
                Id = nuevoId,
                Nombre = "Manzana Test",
                Calorias = 52,
                PorcionGramos = 100,
                Carbohidratos = 15
            };

            // Act: Crear
            service.CrearProducto(producto);

            // Esto fuerza a que el sistema busque en el JSON actualizado
            var productoRecuperado = service.ObtenerPorId(nuevoId);

            // Assert: Validar que existe y los datos coinciden
            Assert.NotNull(productoRecuperado);
            Assert.Equal("Manzana Test", productoRecuperado?.Nombre);

            // Act: Eliminar
            service.EliminarProducto(nuevoId);

            // Assert: Ya no existe
            Assert.Null(service.ObtenerPorId(nuevoId));
        }

        [Fact]
        public void CrearMenu_SinAlimentos_ShouldSucceed()
        {
            var service = new MenuDiarioService();
            var nuevoMenu = new MenuDiario
            {
                Id = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid(),
                Fecha = DateTime.Now.Date,
                Nombre = "Menú Vacío de Prueba"
            };

            // Act & Assert: Ya no debe lanzar ArgumentException
            var exception = Record.Exception(() => service.CrearMenu(nuevoMenu, new List<MenuDetalle>()));
            Assert.Null(exception);
        }

        [Fact]
        public void CrearMenu_DuplicadoMismoUsuario_ThrowsInvalidOperation()
        {
            var service = new MenuDiarioService();
            var userId = Guid.NewGuid();
            var fecha = DateTime.Now.Date;
            var menu1 = new MenuDiario { Id = Guid.NewGuid(), UsuarioId = userId, Fecha = fecha };

            service.CrearMenu(menu1, new List<MenuDetalle>());

            // El segundo intento para el MISMO usuario y MISMA fecha debe fallar
            Assert.Throws<InvalidOperationException>(() =>
                service.CrearMenu(new MenuDiario { UsuarioId = userId, Fecha = fecha }, new List<MenuDetalle>()));
        }

    }
}
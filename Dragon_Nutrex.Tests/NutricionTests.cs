using Dragon_Nutrex.Common;
using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using Xunit;

namespace Dragon_Nutrex.Tests
{
    public class NutricionTests
    {
        [Fact]
        public void CalcularCalorias_Mantenimiento_ReturnsCorrectValue()
        {
            // Arrange: Datos para un hombre de 70kg, 1.75m, 25 años, moderado
            decimal peso = 70;
            decimal altura = 1.75m;
            int edad = 25;

            // Act
            var resultado = NutricionService.CalcularCaloriasObjetivo(
                peso, altura, edad, NivelActividad.Moderado, ObjetivoNutricional.MantenerPeso);

            // Assert: Mifflin-St Jeor para estos datos da aprox 2594
            Assert.True(resultado > 2000);
            Assert.Equal(2594, resultado);
        }

        [Fact]
        public void CrearMenu_Duplicado_ThrowsInvalidOperationException()
        {
            // Arrange
            var service = new MenuDiarioService();
            var userId = Guid.NewGuid();
            var fecha = DateTime.Now.Date;

            var menu1 = new MenuDiario { Id = Guid.NewGuid(), UsuarioId = userId, Fecha = fecha };
            var detalles = new List<MenuDetalle> { new MenuDetalle { ProductoId = Guid.NewGuid(), Porcion = 100 } };

            // Act & Assert
            // Primero creamos uno (esto debería funcionar si el repo está limpio o mockeado)
            // Para una prueba integral real, esto fallará si el usuario ya tiene uno en el JSON
            Assert.Throws<InvalidOperationException>(() =>
            {
                service.CrearMenu(menu1, detalles);
                service.CrearMenu(menu1, detalles); // El segundo intento debe fallar
            });
        }

        [Fact]
        public void DataSeed_IntegrityCheck()
        {
            var service = new UsuarioService();
            var usuarios = service.ObtenerTodos();
            Assert.True(usuarios.Count > 0);
        }

        [Fact]
        public void ConsumoService_ResumenRango_CalculosDebenSerCorrectos()
        {
            var service = new ConsumoService();
            var userId = Guid.NewGuid();

            // 1. Arrange: 2 consumos dentro del rango y 1 fuera
            var consumos = new List<ConsumoDiario>
            {
                new ConsumoDiario {
                    Id = Guid.NewGuid(), UsuarioId = userId, Fecha = DateTime.Now.Date,
                    CaloriasConsumidas = 2000, CarbohidratosConsumidos = 200
                },
                new ConsumoDiario {
                    Id = Guid.NewGuid(), UsuarioId = userId, Fecha = DateTime.Now.Date.AddDays(-2),
                    CaloriasConsumidas = 1000, CarbohidratosConsumidos = 100
                },
                new ConsumoDiario {
                    Id = Guid.NewGuid(), UsuarioId = userId, Fecha = DateTime.Now.Date.AddDays(-20),
                    CaloriasConsumidas = 5000, CarbohidratosConsumidos = 500 // FUERA DE RANGO
                }
            };

            service.RegistrarConsumosMasivos(consumos);

            // 2. Act: Pedimos el resumen de los últimos 7 días
            DateTime inicio = DateTime.Now.Date.AddDays(-7);
            DateTime fin = DateTime.Now.Date;

            var resumen = service.ObtenerResumenPorRango(userId, inicio, fin);

            // 3. Assert: Validamos la precisión de la clase ResumenRango
            Assert.NotNull(resumen);

            // Total: 2000 + 1000 = 3000
            Assert.Equal(3000, resumen.TotalCalorias);
            Assert.Equal(300, resumen.TotalCarbohidratos);

            // Días con registros: 2
            Assert.Equal(2, resumen.DiasConRegistros);

            // Promedios: 3000 / 2 = 1500 | 300 / 2 = 150
            Assert.Equal(1500, resumen.PromedioCalorias);
            Assert.Equal(150, resumen.PromedioCarbohidratos);
        }
    }
}
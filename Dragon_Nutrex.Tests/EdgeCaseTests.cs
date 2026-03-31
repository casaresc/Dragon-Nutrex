using Xunit;
using Dragon_Nutrex.Services;
using Dragon_Nutrex.Models;
using System;

namespace Dragon_Nutrex.Tests
{
    public class EdgeCaseTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void CalcularIMC_InvalidInputs_ShouldThrowArgumentException(decimal valorInvalido)
        {
            Assert.Throws<ArgumentException>(() => SaludService.CalcularIMC(valorInvalido, 1.70m));
            Assert.Throws<ArgumentException>(() => SaludService.CalcularIMC(70, valorInvalido));
        }

        [Fact]
        public void MenuDiario_NegativeQuantity_ShouldFail()
        {
            var service = new MenuDiarioService();
            var menu = new MenuDiario { Id = Guid.NewGuid(), UsuarioId = Guid.NewGuid(), Fecha = DateTime.Now };

            var detalles = new List<MenuDetalle> {
                new MenuDetalle { ProductoId = Guid.NewGuid(), Porcion = -5 }
            };

            Assert.Throws<ArgumentException>(() => service.CrearMenu(menu, detalles));
        }
    }
}
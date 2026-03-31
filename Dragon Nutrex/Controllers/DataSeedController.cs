using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using Dragon_Nutrex.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dragon_Nutrex.Controllers
{
    public class DataSeedController
    {
        private readonly UsuarioService _usuarioService = new UsuarioService();
        private readonly ProductoService _productoService = new ProductoService();
        private readonly ConsumoService _consumoService = new ConsumoService();
        private readonly MenuDiarioService _menuDiarioService = new MenuDiarioService();

        public void GenerarDatosIniciales()
        {
            var usuarios = GenerarUsuarios(25);
            var productos = GenerarProductos(50);

            GenerarHistorialNutricional(usuarios, productos);
        }

        private List<Usuario> GenerarUsuarios(int cantidad)
        {
            var lista = new List<Usuario>();
            for (int i = 1; i <= cantidad; i++)
            {
                var user = new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nombre = $"Usuario Ejemplo {i}",
                    Peso = 60 + i,
                    Altura = 1.60m + (i * 0.01m),
                    Edad = 20 + i,
                    Objetivo = EnumHelpers.RandomEnumValue<ObjetivoNutricional>(),
                    NivelActividad = EnumHelpers.RandomEnumValue<NivelActividad>(),
                    TipoDieta = EnumHelpers.RandomEnumValue<TipoDieta>(),
                    Activo = true
                };
                _usuarioService.CrearUsuario(user);
                lista.Add(user);
            }
            return lista;
        }

        private List<Producto> GenerarProductos(int cantidad)
        {
            var rand = new Random();
            var lista = new List<Producto>();

            var datosPorCategoria = new Dictionary<CategoriaProducto, string[]>
            {
                { CategoriaProducto.Proteina, new[] { "Pollo", "Atún", "Huevo", "Ternera", "Lomo de Cerdo" } },
                { CategoriaProducto.Carbohidrato, new[] { "Arroz", "Avena", "Pasta Integral", "Papa", "Quinoa" } },
                { CategoriaProducto.Grasa, new[] { "Aguacate", "Nueces", "Aceite de Oliva", "Mantequilla de Maní" } },
                { CategoriaProducto.Fruta, new[] { "Manzana", "Banano", "Fresa", "Piña", "Arándanos" } },
                { CategoriaProducto.Verdura, new[] { "Brócoli", "Espinaca", "Zanahoria", "Lechuga" } },
                { CategoriaProducto.Bebida, new[] { "Agua Mineral", "Té Verde", "Café", "Jugo Natural" } },
                { CategoriaProducto.Lacteo, new[] { "Leche", "Yogurt Griego", "Queso Cottage", "Kefir" } },
                { CategoriaProducto.Suplemento, new[] { "Creatina", "Whey Protein", "BCAA", "Multivitamínico" } },
                { CategoriaProducto.Otro, new[] { "Salsa de Soya", "Especias", "Edulcorante" } }
            };

            var categorias = (CategoriaProducto[])Enum.GetValues(typeof(CategoriaProducto));

            for (int i = 1; i <= cantidad; i++)
            {
                var cat = categorias[rand.Next(categorias.Length)];
                var nombreBase = datosPorCategoria[cat][rand.Next(datosPorCategoria[cat].Length)];

                var prod = new Producto
                {
                    Id = Guid.NewGuid(),
                    Nombre = $"{nombreBase} {i}",
                    Categoria = cat,
                    Proteina = cat == CategoriaProducto.Proteina ? rand.Next(20, 30) : rand.Next(0, 10),
                    Carbohidratos = cat == CategoriaProducto.Carbohidrato ? rand.Next(30, 60) : rand.Next(0, 15),
                    Grasas = cat == CategoriaProducto.Grasa ? rand.Next(15, 25) : rand.Next(0, 5),
                    PorcionGramos = 100,
                    Activo = true
                };

                prod.Calorias = (prod.Proteina * 4) + (prod.Carbohidratos * 4) + (prod.Grasas * 9);
                _productoService.CrearProducto(prod);
                lista.Add(prod);
            }
            return lista;
        }
        private void GenerarHistorialNutricional(List<Usuario> usuarios, List<Producto> productos)
        {
            var rand = new Random();
            if (!usuarios.Any() || !productos.Any()) return;

            var todosLosConsumos = new List<ConsumoDiario>();

            foreach (var user in usuarios)
            {
                for (int d = 0; d < 15; d++)
                {
                    var fecha = DateTime.Now.Date.AddDays(-d);

                    // 1. CREAR EL MENÚ DIARIO (Lo que se ve en la interfaz de Menús)
                    var nuevoMenu = new MenuDiario
                    {
                        Id = Guid.NewGuid(),
                        UsuarioId = user.Id,
                        Nombre = $"Plan de {user.Nombre} - Día {d}",
                        Fecha = fecha
                    };

                    var detallesDelMenu = new List<MenuDetalle>();
                    decimal calDia = 0, protDia = 0, carbDia = 0, grasaDia = 0;

                    // 2. AGREGAR ALIMENTOS REALES AL MENÚ
                    int cantAlimentos = rand.Next(3, 6);
                    for (int j = 0; j < cantAlimentos; j++)
                    {
                        var productoAleatorio = productos[rand.Next(productos.Count)];
                        decimal porcionEscalada = rand.Next(1, 4); // 100g a 400g

                        detallesDelMenu.Add(new MenuDetalle
                        {
                            Id = Guid.NewGuid(),
                            MenuId = nuevoMenu.Id,
                            ProductoId = productoAleatorio.Id,
                            Porcion = porcionEscalada * 100
                        });

                        // Sumamos los macros reales de los productos seleccionados
                        calDia += productoAleatorio.Calorias * porcionEscalada;
                        protDia += productoAleatorio.Proteina * porcionEscalada;
                        carbDia += productoAleatorio.Carbohidratos * porcionEscalada;
                        grasaDia += productoAleatorio.Grasas * porcionEscalada;
                    }

                    try
                    {
                        // 3. GUARDAR EL MENÚ Y SUS DETALLES
                        // Esto hará que aparezcan en tu "MenusForm"
                        _menuDiarioService.CrearMenu(nuevoMenu, detallesDelMenu);

                        // 4. CREAR EL REGISTRO DE CONSUMO (Para las estadísticas)
                        var consumo = new ConsumoDiario
                        {
                            Id = Guid.NewGuid(),
                            UsuarioId = user.Id,
                            Fecha = fecha,
                            CaloriasConsumidas = calDia,
                            ProteinasConsumidas = protDia,
                            CarbohidratosConsumidos = carbDia,
                            GrasasConsumidas = grasaDia
                        };

                        todosLosConsumos.Add(consumo);
                    }
                    catch (Exception ex)
                    {
                        // Logueamos el error en la consola de salida para saber qué falló, 
                        // pero permitimos que el bucle siga naturalmente.
                        System.Diagnostics.Debug.WriteLine($"Error al generar registro para {user.Nombre}: {ex.Message}");
                    }
                }
            }
            // 5. GUARDADO MASIVO DE ESTADÍSTICAS
            _consumoService.RegistrarConsumosMasivos(todosLosConsumos);
        }
    }
}
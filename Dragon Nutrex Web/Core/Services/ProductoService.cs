using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Repositories;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona la lógica de negocio relacionada con productos.
    /// </summary>
    public class ProductoService
    {
        private readonly ProductoRepository productoRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductoService"/>.
        /// </summary>
        /// <param name="productoRepository">Repositorio de productos.</param>
        public ProductoService(ProductoRepository productoRepository)
        {
            this.productoRepository = productoRepository;
        }

        /// <summary>
        /// Obtiene todos los productos registrados.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        public List<Producto> ObtenerProductos()
        {
            return productoRepository.GetAll().ToList();
        }

        /// <summary>
        /// Alias para obtener todos los productos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        public List<Producto> ObtenerTodos()
        {
            return productoRepository.GetAll().ToList();
        }

        /// <summary>
        /// Crea un producto nuevo aplicando reglas de negocio.
        /// </summary>
        /// <param name="producto">Producto a crear.</param>
        public void CrearProducto(Producto producto)
        {
            ValidarProducto(producto);
            CalcularCalorias(producto);
            productoRepository.Create(producto);
        }

        /// <summary>
        /// Actualiza un producto existente aplicando reglas de negocio.
        /// </summary>
        /// <param name="producto">Producto a actualizar.</param>
        public void ActualizarProducto(Producto producto)
        {
            ValidarProducto(producto);
            CalcularCalorias(producto);
            productoRepository.Update(producto);
        }

        /// <summary>
        /// Elimina un producto por su identificador.
        /// </summary>
        /// <param name="id">Identificador del producto.</param>
        public void EliminarProducto(Guid id)
        {
            productoRepository.Delete(id);
        }

        /// <summary>
        /// Calcula las calorías del producto según sus macronutrientes.
        /// </summary>
        /// <param name="producto">Producto al que se le calcularán las calorías.</param>
        private static void CalcularCalorias(Producto producto)
        {
            producto.Calorias = (producto.Proteina * 4) +
                                (producto.Carbohidratos * 4) +
                                (producto.Grasas * 9);
        }

        private static void ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new Exception("El nombre del producto es obligatorio");

            if (producto.Proteina < 0 || producto.Carbohidratos < 0 || producto.Grasas < 0)
                throw new Exception("Los macronutrientes no pueden ser negativos");

            if (producto.PorcionGramos <= 0)
                throw new Exception("La porción debe ser mayor a 0");
        }
    }
}
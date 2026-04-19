using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona la lógica de negocio relacionada con productos.
    /// </summary>
    public class ProductoService
    {
        private readonly IRepository<Producto> productoRepository;

        public ProductoService(IRepository<Producto> productoRepository)
        {
            this.productoRepository = productoRepository;
        }

        /// <summary>
        /// Obtiene todos los productos registrados.
        /// </summary>
        public List<Producto> ObtenerProductos()
        {
            return productoRepository.GetAll();
        }

        /// <summary>
        /// Alias para obtener todos los productos.
        /// </summary>
        public List<Producto> ObtenerTodos()
        {
            return productoRepository.GetAll();
        }

        /// <summary>
        /// Crea un producto nuevo aplicando reglas de negocio.
        /// </summary>
        public void CrearProducto(Producto producto)
        {
            ValidarProducto(producto);

            if (producto.Id == Guid.Empty)
            {
                producto.Id = Guid.NewGuid();
            }

            CalcularCalorias(producto);
            productoRepository.Create(producto);
        }

        /// <summary>
        /// Actualiza un producto existente aplicando reglas de negocio.
        /// </summary>
        public void ActualizarProducto(Producto producto)
        {
            ValidarProducto(producto);
            CalcularCalorias(producto);
            productoRepository.Update(producto);
        }

        /// <summary>
        /// Elimina un producto por su identificador.
        /// </summary>
        public void EliminarProducto(Guid id)
        {
            productoRepository.Delete(id);
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

        private static void CalcularCalorias(Producto producto)
        {
            producto.Calorias = (producto.Proteina * 4) +
                                (producto.Carbohidratos * 4) +
                                (producto.Grasas * 9);
        }
    }
}
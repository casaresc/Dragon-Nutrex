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

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductoService"/>.
        /// </summary>
        /// <param name="productoRepository">Repositorio de persistencia de productos.</param>
        public ProductoService(IRepository<Producto> productoRepository)
        {
            this.productoRepository = productoRepository;
        }

        /// <summary>
        /// Obtiene todos los productos registrados.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        public List<Producto> ObtenerProductos()
        {
            return productoRepository.GetAll();
        }

        /// <summary>
        /// Crea un producto nuevo aplicando reglas de negocio.
        /// </summary>
        /// <param name="producto">Producto a registrar.</param>
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
        /// <param name="productoId">Identificador del producto.</param>
        public void EliminarProducto(Guid productoId)
        {
            productoRepository.Delete(productoId);
        }

        /// <summary>
        /// Desactiva un producto del sistema.
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        public void DesactivarProducto(Guid productoId)
        {
            var producto = ObtenerProductoExistente(productoId);
            producto.Activo = false;

            productoRepository.Update(producto);
        }

        /// <summary>
        /// Activa un producto del sistema.
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        public void ActivarProducto(Guid productoId)
        {
            var producto = ObtenerProductoExistente(productoId);
            producto.Activo = true;

            productoRepository.Update(producto);
        }

        /// <summary>
        /// Obtiene un producto existente o lanza una excepción si no existe.
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        /// <returns>Producto existente.</returns>
        private Producto ObtenerProductoExistente(Guid productoId)
        {
            return productoRepository.GetById(productoId)
                ?? throw new KeyNotFoundException($"Producto con ID {productoId} no encontrado.");
        }

        /// <summary>
        /// Valida las reglas de negocio requeridas para un producto.
        /// </summary>
        /// <param name="producto">Producto a validar.</param>
        private static void ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.", nameof(producto));
            }

            if (producto.Proteina < 0 || producto.Carbohidratos < 0 || producto.Grasas < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(producto), "Los macronutrientes no pueden ser negativos.");
            }

            if (producto.PorcionGramos <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(producto), "La porción debe ser mayor a 0.");
            }
        }

        /// <summary>
        /// Calcula las calorías del producto a partir de sus macronutrientes.
        /// </summary>
        /// <param name="producto">Producto al que se le calcularán las calorías.</param>
        private static void CalcularCalorias(Producto producto)
        {
            producto.Calorias = (producto.Proteina * 4) +
                                (producto.Carbohidratos * 4) +
                                (producto.Grasas * 9);
        }
    }
}
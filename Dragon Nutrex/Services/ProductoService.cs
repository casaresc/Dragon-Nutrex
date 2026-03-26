using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;

namespace Dragon_Nutrex.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _repository;

        public ProductoService()
        {
            _repository = new ProductoRepository();
        }

        public List<Producto> ObtenerProductos(bool soloActivos = true)
        {
            return _repository.ObtenerTodos(soloActivos);
        }

        public Producto? ObtenerPorId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El Id del producto no es válido.");

            return _repository.ObtenerPorId(id);
        }

        public void CrearProducto(Producto producto)
        {
            ValidarProducto(producto);

            _repository.Agregar(producto);
        }

        public void ActualizarProducto(Producto producto)
        {
            if (producto.Id == Guid.Empty)
                throw new ArgumentException("El producto no tiene un Id válido.");

            ValidarProducto(producto);

            _repository.Actualizar(producto);
        }

        public void EliminarProducto(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El Id del producto no es válido.");

            _repository.Eliminar(id);
        }

        private void ValidarProducto(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (producto.PorcionGramos <= 0)
                throw new ArgumentException("La porción debe ser mayor que cero.");

            if (producto.Calorias < 0)
                throw new ArgumentException("Las calorías no pueden ser negativas.");

            if (producto.Proteina < 0)
                throw new ArgumentException("La proteína no puede ser negativa.");

            if (producto.Carbohidratos < 0)
                throw new ArgumentException("Los carbohidratos no pueden ser negativos.");

            if (producto.Grasas < 0)
                throw new ArgumentException("Las grasas no pueden ser negativas.");
        }
    }
}
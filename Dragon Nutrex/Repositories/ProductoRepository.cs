using Dragon_Nutrex.Models;
using Dragon_Nutrex.Utils;

namespace Dragon_Nutrex.Repositories
{
    public class ProductoRepository
    {
        private readonly string _filePath = "Data/productos.json";

        public List<Producto> ObtenerTodos(bool soloActivos = true)
        {
            var productos = FileStorage.Load<Producto>(_filePath);

            if (soloActivos)
            {
                return productos
                    .Where(p => p.Activo)
                    .ToList();
            }

            return productos;
        }

        public Producto? ObtenerPorId(Guid id)
        {
            var productos = FileStorage.Load<Producto>(_filePath);

            return productos.FirstOrDefault(p => p.Id == id);
        }

        public void Agregar(Producto producto)
        {
            var productos = FileStorage.Load<Producto>(_filePath);

            producto.Id = Guid.NewGuid();
            producto.FechaCreacion = DateTime.Now;
            producto.Activo = true;

            productos.Add(producto);

            FileStorage.Save(_filePath, productos);
        }

        public void Actualizar(Producto productoActualizado)
        {
            var productos = FileStorage.Load<Producto>(_filePath);

            var producto = productos.FirstOrDefault(p => p.Id == productoActualizado.Id);

            if (producto == null)
                return;

            producto.Nombre = productoActualizado.Nombre;
            producto.Categoria = productoActualizado.Categoria;
            producto.Calorias = productoActualizado.Calorias;
            producto.Proteina = productoActualizado.Proteina;
            producto.Carbohidratos = productoActualizado.Carbohidratos;
            producto.Grasas = productoActualizado.Grasas;
            producto.PorcionGramos = productoActualizado.PorcionGramos;

            FileStorage.Save(_filePath, productos);
        }

        public void Eliminar(Guid id)
        {
            var productos = FileStorage.Load<Producto>(_filePath);

            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return;

            producto.Activo = false;

            FileStorage.Save(_filePath, productos);
        }
    }
}
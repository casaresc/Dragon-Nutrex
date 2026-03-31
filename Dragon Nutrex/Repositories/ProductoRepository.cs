using Dragon_Nutrex.Models;
using Dragon_Nutrex.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dragon_Nutrex.Repositories
{
    public class ProductoRepository : IRepository<Producto>
    {
        private readonly string _filePath = Dragon_Nutrex.Common.DataConfig.GetStoragePath("productos.json");
        public List<Producto> GetAll()
        {
            return FileStorage.Load<Producto>(_filePath)
                              .Where(p => p.Activo)
                              .ToList();
        }

        public Producto? GetById(Guid id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public void Create(Producto entity)
        {
            // Cargamos los productos existentes
            var registros = FileStorage.Load<Producto>(_filePath) ?? new List<Producto>();

            // 1. Forzamos la validación del ID de forma explícita
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            // 2. IMPORTANTE: Antes de guardar, asegúrate de que no haya un duplicado 
            // con el mismo ID (por si el test se corrió dos veces)
            registros.RemoveAll(p => p.Id == entity.Id);

            entity.Activo = true;
            entity.FechaCreacion = DateTime.Now;

            registros.Add(entity);

            // Guardamos
            FileStorage.Save(_filePath, registros);
        }

        public void Update(Producto entity)
        {
            var productos = FileStorage.Load<Producto>(_filePath);
            var index = productos.FindIndex(p => p.Id == entity.Id);

            if (index != -1)
            {
                productos[index] = entity;
                FileStorage.Save(_filePath, productos);
            }
        }

        public void Delete(Guid id)
        {
            var productos = FileStorage.Load<Producto>(_filePath);
            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto != null)
            {
                // Borrado lógico: mantiene la integridad referencial con los Menús
                producto.Activo = false;
                FileStorage.Save(_filePath, productos);
            }
        }
    }
}
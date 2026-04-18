using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Utils.FileStorage;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Common.DataConfig;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    public class MenuDetalleRepository : IRepository<MenuDetalle>
    {
        private readonly string _filePath = DataConfig.GetStoragePath("menu_detalles.json");
        public List<MenuDetalle> GetAll()
        {
            return FileStorage.Load<MenuDetalle>(_filePath)
                              .Where(d => d.Activo)
                              .ToList();
        }

        public MenuDetalle? GetById(Guid id)
        {
            return GetAll().FirstOrDefault(d => d.Id == id);
        }

        public void Create(MenuDetalle entity)
        {
            var detalles = FileStorage.Load<MenuDetalle>(_filePath);

            entity.Id = Guid.NewGuid();
            entity.Activo = true;

            detalles.Add(entity);
            FileStorage.Save(_filePath, detalles);
        }

        public void Update(MenuDetalle entity)
        {
            var detalles = FileStorage.Load<MenuDetalle>(_filePath);
            var index = detalles.FindIndex(d => d.Id == entity.Id);
            if (index != -1)
            {
                detalles[index] = entity;
                FileStorage.Save(_filePath, detalles);
            }
        }

        public void Delete(Guid id)
        {
            var detalles = FileStorage.Load<MenuDetalle>(_filePath);
            var detalle = detalles.FirstOrDefault(d => d.Id == id);
            if (detalle != null)
            {
                detalle.Activo = false;
                FileStorage.Save(_filePath, detalles);
            }
        }

        public List<MenuDetalle> GetByMenuId(Guid menuId)
        {
            return GetAll().Where(d => d.MenuId == menuId).ToList();
        }

    }
}
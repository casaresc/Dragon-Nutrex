using Dragon_Nutrex_Web.Common.DataConfig;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Utils.FileStorage;


namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    public class MenuDiarioRepository : IRepository<MenuDiario>
    {
        private readonly string _filePath = DataConfig.GetStoragePath("menus.json");
        public List<MenuDiario> GetAll()
        {
            return FileStorage.Load<MenuDiario>(_filePath)
                              .Where(m => m.Activo)
                              .OrderBy(m => m.Fecha)
                              .ToList();
        }

        public MenuDiario? GetById(Guid id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }

        public void Create(MenuDiario entity)
        {
            var menus = FileStorage.Load<MenuDiario>(_filePath);

            entity.Id = Guid.NewGuid();
            entity.FechaCreacion = DateTime.Now;
            entity.Activo = true;

            menus.Add(entity);
            FileStorage.Save(_filePath, menus);
        }

        public void Update(MenuDiario entity)
        {
            var menus = FileStorage.Load<MenuDiario>(_filePath);
            var index = menus.FindIndex(m => m.Id == entity.Id);
            if (index != -1)
            {
                menus[index] = entity;
                FileStorage.Save(_filePath, menus);
            }
        }

        public void Delete(Guid id)
        {
            var menus = FileStorage.Load<MenuDiario>(_filePath);
            var menu = menus.FirstOrDefault(m => m.Id == id);
            if (menu != null)
            {
                menu.Activo = false;
                FileStorage.Save(_filePath, menus);
            }
        }

        public bool ExistsByDate(DateTime date)
        {
            return GetAll().Any(m => m.Fecha.Date == date.Date);
        }
    }
}
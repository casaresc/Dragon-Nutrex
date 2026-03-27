using Dragon_Nutrex.Models;
using Dragon_Nutrex.Utils;

namespace Dragon_Nutrex.Repositories
{
    public class MenuDiarioRepository
    {
        private readonly string _filePath =
            "Data/menus.json";

        public List<MenuDiario> ObtenerTodos(
            bool soloActivos = true)
        {
            var menus =
                FileStorage.Load<MenuDiario>(_filePath);

            if (soloActivos)
            {
                return menus
                    .Where(m => m.Activo)
                    .OrderBy(m => m.Fecha)
                    .ToList();
            }

            return menus;
        }

        public MenuDiario? ObtenerPorId(Guid id)
        {
            var menus =
                FileStorage.Load<MenuDiario>(_filePath);

            return menus
                .FirstOrDefault(m => m.Id == id);
        }

        public void Agregar(MenuDiario menu)
        {
            var menus =
                FileStorage.Load<MenuDiario>(_filePath);

            menu.Id = Guid.NewGuid();
            menu.FechaCreacion = DateTime.Now;
            menu.Activo = true;

            menus.Add(menu);

            FileStorage.Save(_filePath, menus);
        }

        public void Actualizar(MenuDiario menuActualizado)
        {
            var menus =
                FileStorage.Load<MenuDiario>(_filePath);

            var menu = menus
                .FirstOrDefault(m =>
                    m.Id == menuActualizado.Id);

            if (menu == null)
                return;

            menu.Nombre = menuActualizado.Nombre;
            menu.Fecha = menuActualizado.Fecha;

            menu.Detalles = menuActualizado.Detalles;

            FileStorage.Save(_filePath, menus);
        }

        public void Eliminar(Guid id)
        {
            var menus =
                FileStorage.Load<MenuDiario>(_filePath);

            var menu = menus
                .FirstOrDefault(m => m.Id == id);

            if (menu == null)
                return;

            menu.Activo = false;

            FileStorage.Save(_filePath, menus);
        }
        public bool ExisteMenuParaFecha(DateTime fecha)
        {
            var menus = FileStorage.Load<MenuDiario>(_filePath);

            return menus.Any(m =>
                m.Activo &&
                m.Fecha.Date == fecha.Date);
        }
    }
}
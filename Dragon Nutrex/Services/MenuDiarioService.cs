using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;

namespace Dragon_Nutrex.Services
{
    public class MenuDiarioService
    {
        private readonly MenuDiarioRepository _repository;

        public MenuDiarioService()
        {
            _repository = new MenuDiarioRepository();
        }

        public List<MenuDiario> ObtenerMenus(
            bool soloActivos = true)
        {
            return _repository.ObtenerTodos(
                soloActivos);
        }

        public void CrearMenu(MenuDiario menu)
        {
            ValidarMenu(menu);

            if (_repository.ExisteMenuParaFecha(menu.Fecha))
            {
                throw new ArgumentException(
                    "Ya existe un menú para esa fecha.");
            }

            _repository.Agregar(menu);
        }

        public void ActualizarMenu(MenuDiario menu)
        {
            if (menu.Id == Guid.Empty)
                throw new ArgumentException(
                    "El menú no tiene un Id válido.");

            ValidarMenu(menu);

            var menus = _repository
                .ObtenerTodos(false);

            var existeOtroMenu = menus.Any(m =>
                m.Activo &&
                m.Id != menu.Id &&
                m.Fecha.Date == menu.Fecha.Date);

            if (existeOtroMenu)
            {
                throw new ArgumentException(
                    "Ya existe un menú para esa fecha.");
            }

            _repository.Actualizar(menu);
        }

        public void EliminarMenu(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(
                    "El Id del menú no es válido.");

            _repository.Eliminar(id);
        }

        private void ValidarMenu(MenuDiario menu)
        {
            if (menu == null)
                throw new ArgumentNullException(
                    nameof(menu));

            if (string.IsNullOrWhiteSpace(menu.Nombre))
                throw new ArgumentException(
                    "El nombre del menú es obligatorio.");

            if (menu.Fecha == default)
                throw new ArgumentException(
                    "La fecha del menú es obligatoria.");
        }
    }
}
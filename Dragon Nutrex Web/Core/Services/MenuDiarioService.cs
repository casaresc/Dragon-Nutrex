using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Repositories;

namespace Dragon_Nutrex_Web.Core.Services
{
    public class MenuDiarioService
    {
        private readonly IRepository<MenuDiario> menuRepository;
        private readonly MenuDetalleRepository menuDetalleRepository;

        public MenuDiarioService(
            IRepository<MenuDiario> menuRepository,
            MenuDetalleRepository menuDetalleRepository)
        {
            this.menuRepository = menuRepository;
            this.menuDetalleRepository = menuDetalleRepository;
        }

        public List<MenuDiario> ObtenerMenus()
        {
            return menuRepository.GetAll();
        }

        public List<MenuDiario> ObtenerTodos()
        {
            return menuRepository.GetAll();
        }

        public MenuDiario? ObtenerPorId(Guid id)
        {
            return menuRepository.GetById(id);
        }

        public MenuDiario? ObtenerPorUsuarioYFecha(Guid usuarioId, DateTime fecha)
        {
            return ((MenuDiarioRepository)menuRepository).GetByUsuarioYFecha(usuarioId, fecha);
        }

        public void CrearMenu(MenuDiario menu)
        {
            CrearMenu(menu, new List<MenuDetalle>());
        }

        public void CrearMenu(MenuDiario menu, List<MenuDetalle> detalles)
        {
            if (menu.Id == Guid.Empty)
            {
                menu.Id = Guid.NewGuid();
            }

            ValidarMenu(menu);

            menuRepository.Create(menu);

            foreach (var detalle in detalles)
            {
                if (detalle.Id == Guid.Empty)
                {
                    detalle.Id = Guid.NewGuid();
                }

                detalle.MenuId = menu.Id;
                menuDetalleRepository.Create(detalle);
            }
        }

        public void ActualizarMenu(MenuDiario menu)
        {
            ValidarMenu(menu);
            menuRepository.Update(menu);
        }

        public void EliminarMenu(Guid id)
        {
            menuRepository.Delete(id);
        }

        private static void ValidarMenu(MenuDiario menu)
        {
            if (menu.UsuarioId == Guid.Empty)
                throw new Exception("El menú debe estar asociado a un usuario.");

            if (string.IsNullOrWhiteSpace(menu.Nombre))
                throw new Exception("El nombre del menú es obligatorio.");
        }
    }
}
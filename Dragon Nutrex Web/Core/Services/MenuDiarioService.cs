using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona la lógica de negocio relacionada con menús diarios.
    /// </summary>
    public class MenuDiarioService
    {
        private readonly IMenuDiarioRepository menuRepository;
        private readonly IRepository<MenuDetalle> menuDetalleRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="MenuDiarioService"/>.
        /// </summary>
        /// <param name="menuRepository">Repositorio de menús diarios.</param>
        /// <param name="menuDetalleRepository">Repositorio de detalles de menú.</param>
        public MenuDiarioService(
            IMenuDiarioRepository menuRepository,
            IRepository<MenuDetalle> menuDetalleRepository)
        {
            this.menuRepository = menuRepository;
            this.menuDetalleRepository = menuDetalleRepository;
        }

        /// <summary>
        /// Obtiene todos los menús registrados.
        /// </summary>
        public List<MenuDiario> ObtenerMenus()
        {
            return menuRepository.GetAll();
        }

        /// <summary>
        /// Obtiene todos los menús registrados.
        /// </summary>
        public List<MenuDiario> ObtenerTodos()
        {
            return menuRepository.GetAll();
        }

        /// <summary>
        /// Obtiene un menú por su identificador.
        /// </summary>
        public MenuDiario? ObtenerPorId(Guid menuId)
        {
            return menuRepository.GetById(menuId);
        }

        /// <summary>
        /// Obtiene un menú por usuario y fecha.
        /// </summary>
        public MenuDiario? ObtenerPorUsuarioYFecha(Guid usuarioId, DateTime fecha)
        {
            return menuRepository.GetByUsuarioYFecha(usuarioId, fecha);
        }

        /// <summary>
        /// Crea un menú sin detalles asociados.
        /// </summary>
        public void CrearMenu(MenuDiario menu)
        {
            CrearMenu(menu, new List<MenuDetalle>());
        }

        /// <summary>
        /// Crea un menú con sus detalles asociados.
        /// </summary>
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

        /// <summary>
        /// Actualiza un menú existente.
        /// </summary>
        public void ActualizarMenu(MenuDiario menu)
        {
            ValidarMenu(menu);
            menuRepository.Update(menu);
        }

        /// <summary>
        /// Elimina un menú por su identificador.
        /// </summary>
        public void EliminarMenu(Guid menuId)
        {
            menuRepository.Delete(menuId);
        }

        /// <summary>
        /// Valida las reglas de negocio del menú.
        /// </summary>
        private static void ValidarMenu(MenuDiario menu)
        {
            if (menu.UsuarioId == Guid.Empty)
                throw new Exception("El menú debe estar asociado a un usuario.");

            if (string.IsNullOrWhiteSpace(menu.Nombre))
                throw new Exception("El nombre del menú es obligatorio.");
        }
    }
}
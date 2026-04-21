using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona la generación de estadísticas administrativas globales del sistema.
    /// </summary>
    public class AdminEstadisticasService
    {
        private readonly IRepository<Usuario> usuarioRepository;
        private readonly IRepository<Producto> productoRepository;
        private readonly IRepository<MenuDiario> menuDiarioRepository;
        private readonly IRepository<MenuDetalle> menuDetalleRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="AdminEstadisticasService"/>.
        /// </summary>
        /// <param name="usuarioRepository">Repositorio de usuarios.</param>
        /// <param name="productoRepository">Repositorio de productos.</param>
        /// <param name="menuDiarioRepository">Repositorio de menús diarios.</param>
        /// <param name="menuDetalleRepository">Repositorio de detalles de menú.</param>
        public AdminEstadisticasService(
            IRepository<Usuario> usuarioRepository,
            IRepository<Producto> productoRepository,
            IRepository<MenuDiario> menuDiarioRepository,
            IRepository<MenuDetalle> menuDetalleRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.productoRepository = productoRepository;
            this.menuDiarioRepository = menuDiarioRepository;
            this.menuDetalleRepository = menuDetalleRepository;
        }

        /// <summary>
        /// Obtiene el producto más consumido dentro de un rango de fechas.
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial del rango.</param>
        /// <param name="fechaFin">Fecha final del rango.</param>
        /// <returns>Resultado del producto más consumido o null si no hay datos.</returns>
        public ProductoMasConsumidoResultado? ObtenerProductoMasConsumido(DateTime fechaInicio, DateTime fechaFin)
        {
            ValidarRangoFechas(fechaInicio, fechaFin);

            var menus = menuDiarioRepository.GetAll()
                .Where(menu => menu.Fecha.Date >= fechaInicio.Date && menu.Fecha.Date <= fechaFin.Date)
                .ToList();

            if (!menus.Any())
            {
                return null;
            }

            var menuIds = menus.Select(menu => menu.Id).ToHashSet();

            var detalles = menuDetalleRepository.GetAll()
                .Where(detalle => menuIds.Contains(detalle.MenuId))
                .ToList();

            if (!detalles.Any())
            {
                return null;
            }

            var productos = productoRepository.GetAll();

            return detalles
                .GroupBy(detalle => detalle.ProductoId)
                .Select(grupo =>
                {
                    var producto = productos.FirstOrDefault(item => item.Id == grupo.Key);

                    return new ProductoMasConsumidoResultado
                    {
                        ProductoId = grupo.Key,
                        NombreProducto = producto?.Nombre ?? "Producto desconocido",
                        TotalPorciones = grupo.Sum(item => item.Porcion),
                        TotalRegistros = grupo.Count()
                    };
                })
                .OrderByDescending(resultado => resultado.TotalPorciones)
                .ThenByDescending(resultado => resultado.TotalRegistros)
                .FirstOrDefault();
        }

        /// <summary>
        /// Obtiene el porcentaje de usuarios por tipo de dieta.
        /// </summary>
        /// <returns>Lista de porcentajes por tipo de dieta.</returns>
        public List<PorcentajeTipoDietaResultado> ObtenerPorcentajeTiposDieta()
        {
            var usuarios = usuarioRepository.GetAll();

            if (!usuarios.Any())
            {
                return new List<PorcentajeTipoDietaResultado>();
            }

            var totalUsuarios = usuarios.Count;

            return usuarios
                .GroupBy(usuario => usuario.TipoDieta.ToString())
                .Select(grupo => new PorcentajeTipoDietaResultado
                {
                    TipoDieta = grupo.Key,
                    CantidadUsuarios = grupo.Count(),
                    Porcentaje = Math.Round((decimal)grupo.Count() * 100 / totalUsuarios, 2)
                })
                .OrderByDescending(resultado => resultado.CantidadUsuarios)
                .ToList();
        }

        /// <summary>
        /// Obtiene los usuarios ordenados por cantidad de menús registrados.
        /// </summary>
        /// <returns>Lista de usuarios con su cantidad de menús.</returns>
        public List<UsuarioMenusResultado> ObtenerUsuariosConMasMenus()
        {
            var usuarios = usuarioRepository.GetAll();
            var menus = menuDiarioRepository.GetAll();

            if (!menus.Any())
            {
                return new List<UsuarioMenusResultado>();
            }

            return menus
                .GroupBy(menu => menu.UsuarioId)
                .Select(grupo =>
                {
                    var usuario = usuarios.FirstOrDefault(item => item.Id == grupo.Key);

                    return new UsuarioMenusResultado
                    {
                        UsuarioId = grupo.Key,
                        NombreUsuario = usuario?.Nombre ?? "Usuario desconocido",
                        CantidadMenus = grupo.Count()
                    };
                })
                .OrderByDescending(resultado => resultado.CantidadMenus)
                .ThenBy(resultado => resultado.NombreUsuario)
                .ToList();
        }

        /// <summary>
        /// Valida que el rango de fechas sea correcto.
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial.</param>
        /// <param name="fechaFin">Fecha final.</param>
        private static void ValidarRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio.Date > fechaFin.Date)
            {
                throw new ArgumentException("La fecha inicio no puede ser mayor que la fecha fin.");
            }
        }
    }
}
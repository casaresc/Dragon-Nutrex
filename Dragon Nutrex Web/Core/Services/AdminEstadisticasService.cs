using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Repositories;

namespace Dragon_Nutrex_Web.Core.Services
{
    public class AdminEstadisticasService
    {
        private readonly UsuarioRepository usuarioRepository;
        private readonly ProductoRepository productoRepository;
        private readonly MenuDiarioRepository menuDiarioRepository;
        private readonly MenuDetalleRepository menuDetalleRepository;

        public AdminEstadisticasService(
            UsuarioRepository usuarioRepository,
            ProductoRepository productoRepository,
            MenuDiarioRepository menuDiarioRepository,
            MenuDetalleRepository menuDetalleRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.productoRepository = productoRepository;
            this.menuDiarioRepository = menuDiarioRepository;
            this.menuDetalleRepository = menuDetalleRepository;
        }

        public ProductoMasConsumidoResultado? ObtenerProductoMasConsumido(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio.Date > fechaFin.Date)
                throw new Exception("La fecha inicio no puede ser mayor que la fecha fin.");

            var menus = menuDiarioRepository.GetAll()
                .Where(m => m.Fecha.Date >= fechaInicio.Date && m.Fecha.Date <= fechaFin.Date)
                .ToList();

            if (!menus.Any())
                return null;

            var menuIds = menus.Select(m => m.Id).ToHashSet();

            var detalles = menuDetalleRepository.GetAll()
                .Where(d => menuIds.Contains(d.MenuId))
                .ToList();

            if (!detalles.Any())
                return null;

            var productos = productoRepository.GetAll();

            var resultado = detalles
                .GroupBy(d => d.ProductoId)
                .Select(g =>
                {
                    var producto = productos.FirstOrDefault(p => p.Id == g.Key);

                    return new ProductoMasConsumidoResultado
                    {
                        ProductoId = g.Key,
                        NombreProducto = producto?.Nombre ?? "Producto desconocido",
                        TotalPorciones = g.Sum(x => x.Porcion),
                        TotalRegistros = g.Count()
                    };
                })
                .OrderByDescending(x => x.TotalPorciones)
                .ThenByDescending(x => x.TotalRegistros)
                .FirstOrDefault();

            return resultado;
        }

        public List<PorcentajeTipoDietaResultado> ObtenerPorcentajeTiposDieta()
        {
            var usuarios = usuarioRepository.GetAll();

            if (!usuarios.Any())
                return new List<PorcentajeTipoDietaResultado>();

            var totalUsuarios = usuarios.Count;

            return usuarios
                .GroupBy(u => u.TipoDieta.ToString())
                .Select(g => new PorcentajeTipoDietaResultado
                {
                    TipoDieta = g.Key,
                    CantidadUsuarios = g.Count(),
                    Porcentaje = Math.Round((decimal)g.Count() * 100 / totalUsuarios, 2)
                })
                .OrderByDescending(x => x.CantidadUsuarios)
                .ToList();
        }

        public List<UsuarioMenusResultado> ObtenerUsuariosConMasMenus()
        {
            var usuarios = usuarioRepository.GetAll();
            var menus = menuDiarioRepository.GetAll();

            if (!menus.Any())
                return new List<UsuarioMenusResultado>();

            return menus
                .GroupBy(m => m.UsuarioId)
                .Select(g =>
                {
                    var usuario = usuarios.FirstOrDefault(u => u.Id == g.Key);

                    return new UsuarioMenusResultado
                    {
                        UsuarioId = g.Key,
                        NombreUsuario = usuario?.Nombre ?? "Usuario desconocido",
                        CantidadMenus = g.Count()
                    };
                })
                .OrderByDescending(x => x.CantidadMenus)
                .ThenBy(x => x.NombreUsuario)
                .ToList();
        }
    }
}
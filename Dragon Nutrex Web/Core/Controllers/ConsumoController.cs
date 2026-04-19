using Dragon_Nutrex_Web.Common;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;

namespace Dragon_Nutrex_Web.Core.Controllers
{
    public class ConsumoController
    {
        private readonly ConsumoService consumoService;
        private readonly UsuarioService usuarioService;
        private readonly NutricionService nutricionService;

        public ConsumoController(
            ConsumoService consumoService,
            UsuarioService usuarioService,
            NutricionService nutricionService)
        {
            this.consumoService = consumoService;
            this.usuarioService = usuarioService;
            this.nutricionService = nutricionService;
        }

        public ResumenDiario ObtenerResumenParaFecha(DateTime fecha)
        {
            try
            {
                var usuario = usuarioService.ObtenerTodos().FirstOrDefault();

                if (usuario == null)
                {
                    return new ResumenDiario { TieneRegistros = false };
                }

                var requerimientos = nutricionService.CalcularRequerimientos(usuario);

                return consumoService.ObtenerResumenDiario(fecha, requerimientos.CaloriasObjetivo);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return new ResumenDiario { TieneRegistros = false };
            }
        }

        public ResumenDiario ObtenerResumenParaUsuarioYFecha(Guid usuarioId, DateTime fecha)
        {
            try
            {
                var usuario = usuarioService.ObtenerTodos().FirstOrDefault(u => u.Id == usuarioId);

                if (usuario == null)
                {
                    return new ResumenDiario { TieneRegistros = false };
                }

                var requerimientos = nutricionService.CalcularRequerimientos(usuario);

                return consumoService.ObtenerResumenDiario(usuarioId, fecha, requerimientos.CaloriasObjetivo);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return new ResumenDiario { TieneRegistros = false };
            }
        }

        public ResumenRango ObtenerEstadisticasRangoPorUsuario(Guid usuarioId, DateTime inicio, DateTime fin)
        {
            try
            {
                return consumoService.ObtenerResumenPorRango(usuarioId, inicio, fin);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return new ResumenRango();
            }
        }

        public void RegistrarNuevoConsumo(ConsumoDiario consumo)
        {
            try
            {
                consumoService.RegistrarConsumo(consumo);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
            }
        }

        public ResumenRango ObtenerEstadisticasRango(DateTime inicio, DateTime fin)
        {
            try
            {
                return consumoService.ObtenerResumenPorRango(inicio, fin);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return new ResumenRango();
            }
        }
    }
}
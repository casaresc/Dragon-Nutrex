using Dragon_Nutrex_Web.Common;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;

namespace Dragon_Nutrex_Web.Core.Controllers
{
    /// <summary>
    /// Orquesta las consultas y operaciones relacionadas con consumos y estadísticas nutricionales.
    /// </summary>
    public class ConsumoController
    {
        private readonly ConsumoService consumoService;
        private readonly UsuarioService usuarioService;
        private readonly NutricionService nutricionService;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ConsumoController"/>.
        /// </summary>
        /// <param name="consumoService">Servicio de consumos diarios.</param>
        /// <param name="usuarioService">Servicio de usuarios.</param>
        /// <param name="nutricionService">Servicio de cálculo nutricional.</param>
        public ConsumoController(
            ConsumoService consumoService,
            UsuarioService usuarioService,
            NutricionService nutricionService)
        {
            this.consumoService = consumoService;
            this.usuarioService = usuarioService;
            this.nutricionService = nutricionService;
        }

        /// <summary>
        /// Obtiene el resumen diario de consumo para la primera persona usuaria disponible.
        /// </summary>
        /// <param name="fecha">Fecha a consultar.</param>
        /// <returns>Resumen diario calculado o un resumen vacío si ocurre un error o no existen usuarios.</returns>
        public ResumenDiario ObtenerResumenParaFecha(DateTime fecha)
        {
            try
            {
                var usuario = usuarioService.ObtenerTodos().FirstOrDefault();

                if (usuario is null)
                {
                    return CrearResumenDiarioVacio();
                }

                var requerimientos = nutricionService.CalcularRequerimientos(usuario);

                return consumoService.ObtenerResumenDiario(fecha, requerimientos.CaloriasObjetivo);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex, "ConsumoController.ObtenerResumenParaFecha");
                return CrearResumenDiarioVacio();
            }
        }

        /// <summary>
        /// Obtiene el resumen diario de consumo para un usuario específico en una fecha dada.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="fecha">Fecha a consultar.</param>
        /// <returns>Resumen diario calculado o un resumen vacío si ocurre un error o el usuario no existe.</returns>
        public ResumenDiario ObtenerResumenParaUsuarioYFecha(Guid usuarioId, DateTime fecha)
        {
            try
            {
                var usuario = usuarioService.ObtenerPorId(usuarioId);

                if (usuario is null)
                {
                    return CrearResumenDiarioVacio();
                }

                var requerimientos = nutricionService.CalcularRequerimientos(usuario);

                return consumoService.ObtenerResumenDiario(usuarioId, fecha, requerimientos.CaloriasObjetivo);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex, "ConsumoController.ObtenerResumenParaUsuarioYFecha");
                return CrearResumenDiarioVacio();
            }
        }

        /// <summary>
        /// Obtiene el resumen estadístico por rango de fechas para un usuario específico.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="fechaInicio">Fecha inicial del rango.</param>
        /// <param name="fechaFin">Fecha final del rango.</param>
        /// <returns>Resumen estadístico por rango o un resumen vacío si ocurre un error.</returns>
        public ResumenRango ObtenerEstadisticasRangoPorUsuario(Guid usuarioId, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return consumoService.ObtenerResumenPorRango(usuarioId, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex, "ConsumoController.ObtenerEstadisticasRangoPorUsuario");
                return CrearResumenRangoVacio();
            }
        }

        /// <summary>
        /// Registra un nuevo consumo diario.
        /// </summary>
        /// <param name="consumo">Consumo a registrar.</param>
        public void RegistrarNuevoConsumo(ConsumoDiario consumo)
        {
            try
            {
                consumoService.RegistrarConsumo(consumo);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex, "ConsumoController.RegistrarNuevoConsumo");
            }
        }

        /// <summary>
        /// Obtiene el resumen estadístico global por rango de fechas.
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial del rango.</param>
        /// <param name="fechaFin">Fecha final del rango.</param>
        /// <returns>Resumen estadístico por rango o un resumen vacío si ocurre un error.</returns>
        public ResumenRango ObtenerEstadisticasRango(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return consumoService.ObtenerResumenPorRango(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex, "ConsumoController.ObtenerEstadisticasRango");
                return CrearResumenRangoVacio();
            }
        }

        /// <summary>
        /// Crea un resumen diario vacío.
        /// </summary>
        /// <returns>Instancia vacía de <see cref="ResumenDiario"/>.</returns>
        private static ResumenDiario CrearResumenDiarioVacio()
        {
            return new ResumenDiario
            {
                TieneRegistros = false
            };
        }

        /// <summary>
        /// Crea un resumen por rango vacío.
        /// </summary>
        /// <returns>Instancia vacía de <see cref="ResumenRango"/>.</returns>
        private static ResumenRango CrearResumenRangoVacio()
        {
            return new ResumenRango();
        }
    }
}
using Dragon_Nutrex.Common;
using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using System;
using System.Linq;

namespace Dragon_Nutrex.Controllers
{
    public partial class ConsumoController
    {
        private readonly ConsumoService _consumoService = new ConsumoService();
        private readonly UsuarioService _usuarioService = new UsuarioService();
        private readonly NutricionService _nutricionService = new NutricionService();

        public ResumenDiario ObtenerResumenParaFecha(DateTime fecha)
        {
            try
            {
                var usuario = _usuarioService.ObtenerTodos().FirstOrDefault();

                if (usuario == null) return new ResumenDiario { TieneRegistros = false };

                var requerimientos = _nutricionService.CalcularRequerimientos(usuario);

                return _consumoService.ObtenerResumenDiario(fecha, requerimientos.CaloriasObjetivo);
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
                var usuario = _usuarioService.ObtenerTodos().FirstOrDefault(u => u.Id == usuarioId);

                if (usuario == null) return new ResumenDiario { TieneRegistros = false };

                var requerimientos = _nutricionService.CalcularRequerimientos(usuario);

                return _consumoService.ObtenerResumenDiario(usuarioId, fecha, requerimientos.CaloriasObjetivo);
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
                return _consumoService.ObtenerResumenPorRango(usuarioId, inicio, fin);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return new ResumenRango();
            }
        }

        public void RegistrarNuevoConsumo(ConsumoDiario consumo)
        {
            try { _consumoService.RegistrarConsumo(consumo); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); }
        }

        public ResumenRango ObtenerEstadisticasRango(DateTime inicio, DateTime fin)
        {
            try
            {
                return _consumoService.ObtenerResumenPorRango(inicio, fin);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return new ResumenRango(); // Devuelve un objeto vacío en caso de error
            }
        }
    }
}
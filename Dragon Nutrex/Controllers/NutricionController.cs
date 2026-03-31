using Dragon_Nutrex.Common;
using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using Dragon_Nutrex.Utils;
using System;

namespace Dragon_Nutrex.Controllers
{
    public class NutricionController
    {
        private readonly NutricionService _nutricionService = new NutricionService();
        public RequerimientoNutricional? CalcularRequerimientos(Usuario usuario)
        {
            try
            {
                return _nutricionService.CalcularRequerimientos(usuario);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return null;
            }
        }
 

        public static decimal ObtenerIMC(decimal peso, decimal altura)
        {
            try
            {
                return SaludService.CalcularIMC(peso, altura);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return 0;
            }
        }
        public RequerimientoNutricional? CalcularPlanNutricional(decimal peso, decimal altura, int edad,
            NivelActividad actividad, ObjetivoNutricional objetivo, TipoDieta dieta)
        {
            try
            {
                return _nutricionService.CalcularRequerimiento(peso, altura, edad, actividad, objetivo, dieta);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return null;
            }
        }

    }
}
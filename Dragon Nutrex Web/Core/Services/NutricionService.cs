using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona cálculos de requerimientos nutricionales y distribución de macronutrientes.
    /// </summary>
    public class NutricionService
    {
        /// <summary>
        /// Calcula los requerimientos nutricionales a partir de una persona usuaria.
        /// </summary>
        /// <param name="usuario">Usuario del cual se tomarán los datos antropométricos y nutricionales.</param>
        /// <returns>Requerimiento nutricional calculado.</returns>
        public RequerimientoNutricional CalcularRequerimientos(Usuario usuario)
        {
            if (usuario is null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            return CalcularRequerimiento(
                usuario.Peso,
                usuario.Altura,
                usuario.Edad,
                usuario.NivelActividad,
                usuario.Objetivo,
                usuario.TipoDieta);
        }

        /// <summary>
        /// Calcula el requerimiento nutricional a partir de parámetros individuales.
        /// </summary>
        /// <param name="peso">Peso corporal en kilogramos.</param>
        /// <param name="altura">Altura en metros.</param>
        /// <param name="edad">Edad en años.</param>
        /// <param name="actividad">Nivel de actividad física.</param>
        /// <param name="objetivo">Objetivo nutricional.</param>
        /// <param name="dieta">Tipo de dieta.</param>
        /// <returns>Requerimiento nutricional calculado.</returns>
        public RequerimientoNutricional CalcularRequerimiento(
            decimal peso,
            decimal altura,
            int edad,
            NivelActividad actividad,
            ObjetivoNutricional objetivo,
            TipoDieta dieta)
        {
            var caloriasObjetivo = CalcularCaloriasObjetivo(
                peso,
                altura,
                edad,
                actividad,
                objetivo);

            return CalcularDistribucionMacros(caloriasObjetivo, dieta);
        }

        /// <summary>
        /// Calcula las calorías objetivo en función de peso, altura, edad, actividad y objetivo nutricional.
        /// </summary>
        /// <param name="peso">Peso corporal en kilogramos.</param>
        /// <param name="altura">Altura en metros.</param>
        /// <param name="edad">Edad en años.</param>
        /// <param name="actividad">Nivel de actividad física.</param>
        /// <param name="objetivo">Objetivo nutricional.</param>
        /// <returns>Calorías objetivo estimadas.</returns>
        public static decimal CalcularCaloriasObjetivo(
            decimal peso,
            decimal altura,
            int edad,
            NivelActividad actividad,
            ObjetivoNutricional objetivo)
        {
            ValidarParametrosBasicos(peso, altura, edad);

            var tasaMetabolicaBasal = (10 * peso) + (6.25m * altura * 100) - (5 * edad) + 5;
            var factorActividad = ObtenerFactorActividad(actividad);
            var calorias = tasaMetabolicaBasal * factorActividad;
            var caloriasAjustadas = AjustarPorObjetivo(calorias, objetivo);

            return Math.Round(caloriasAjustadas, 0);
        }

        /// <summary>
        /// Calcula la distribución de macronutrientes según las calorías objetivo y el tipo de dieta.
        /// </summary>
        /// <param name="calorias">Calorías objetivo.</param>
        /// <param name="dieta">Tipo de dieta.</param>
        /// <returns>Distribución de macronutrientes calculada.</returns>
        public RequerimientoNutricional CalcularDistribucionMacros(decimal calorias, TipoDieta dieta)
        {
            decimal porcentajeCarbohidratos;
            decimal porcentajeProteinas;
            decimal porcentajeGrasas;

            switch (dieta)
            {
                case TipoDieta.BajaEnCarbohidratos:
                    porcentajeCarbohidratos = 0.30m;
                    porcentajeProteinas = 0.40m;
                    porcentajeGrasas = 0.30m;
                    break;

                case TipoDieta.AltaEnCarbohidratos:
                    porcentajeCarbohidratos = 0.60m;
                    porcentajeProteinas = 0.20m;
                    porcentajeGrasas = 0.20m;
                    break;

                case TipoDieta.Cetogenica:
                    porcentajeCarbohidratos = 0.05m;
                    porcentajeProteinas = 0.25m;
                    porcentajeGrasas = 0.70m;
                    break;

                default:
                    porcentajeCarbohidratos = 0.50m;
                    porcentajeProteinas = 0.20m;
                    porcentajeGrasas = 0.30m;
                    break;
            }

            var carbohidratos = (calorias * porcentajeCarbohidratos) / 4;
            var proteinas = (calorias * porcentajeProteinas) / 4;
            var grasas = (calorias * porcentajeGrasas) / 9;

            return new RequerimientoNutricional
            {
                CaloriasObjetivo = calorias,
                CarbohidratosGramos = Math.Round(carbohidratos, 0),
                ProteinasGramos = Math.Round(proteinas, 0),
                GrasasGramos = Math.Round(grasas, 0)
            };
        }

        /// <summary>
        /// Obtiene el factor multiplicador según el nivel de actividad física.
        /// </summary>
        /// <param name="actividad">Nivel de actividad física.</param>
        /// <returns>Factor de actividad correspondiente.</returns>
        private static decimal ObtenerFactorActividad(NivelActividad actividad)
        {
            return actividad switch
            {
                NivelActividad.Sedentario => 1.2m,
                NivelActividad.Ligero => 1.375m,
                NivelActividad.Moderado => 1.55m,
                NivelActividad.Intenso => 1.725m,
                _ => 1.2m
            };
        }

        /// <summary>
        /// Ajusta las calorías según el objetivo nutricional.
        /// </summary>
        /// <param name="calorias">Calorías base calculadas.</param>
        /// <param name="objetivo">Objetivo nutricional.</param>
        /// <returns>Calorías ajustadas según el objetivo.</returns>
        private static decimal AjustarPorObjetivo(decimal calorias, ObjetivoNutricional objetivo)
        {
            return objetivo switch
            {
                ObjetivoNutricional.PerderPeso => calorias - 500,
                ObjetivoNutricional.GanarPeso => calorias + 500,
                _ => calorias
            };
        }

        /// <summary>
        /// Valida los parámetros básicos necesarios para cálculos nutricionales.
        /// </summary>
        /// <param name="peso">Peso corporal en kilogramos.</param>
        /// <param name="altura">Altura en metros.</param>
        /// <param name="edad">Edad en años.</param>
        private static void ValidarParametrosBasicos(decimal peso, decimal altura, int edad)
        {
            if (peso <= 0)
            {
                throw new ArgumentException("El peso debe ser mayor a cero.", nameof(peso));
            }

            if (altura <= 0)
            {
                throw new ArgumentException("La altura debe ser mayor a cero.", nameof(altura));
            }

            if (edad <= 0)
            {
                throw new ArgumentException("La edad debe ser mayor a cero.", nameof(edad));
            }
        }
    }
}
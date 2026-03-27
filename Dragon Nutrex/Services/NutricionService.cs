using Dragon_Nutrex.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Services
{
    public class NutricionService
    {
        public RequerimientoNutricional
    CalcularRequerimiento(
        decimal peso,
        decimal altura,
        int edad,
        NivelActividad actividad,
        ObjetivoNutricional objetivo,
        TipoDieta dieta)
        {
            var caloriasBase =
                CalcularCaloriasObjetivo(
                    peso,
                    altura,
                    edad,
                    actividad,
                    objetivo);

            return CalcularDistribucionMacros(
                caloriasBase,
                dieta);
        }

        public decimal CalcularCaloriasObjetivo(
    decimal peso,
    decimal altura,
    int edad,
    NivelActividad actividad,
    ObjetivoNutricional objetivo)
        {
            if (peso <= 0)
                throw new ArgumentException(
                    "Peso inválido.");

            if (altura <= 0)
                throw new ArgumentException(
                    "Altura inválida.");

            if (edad <= 0)
                throw new ArgumentException(
                    "Edad inválida.");

            var bmr =
                (10 * peso) +
                (6.25m * altura * 100) -
                (5 * edad) +
                5;

            var factor =
                ObtenerFactorActividad(
                    actividad);

            var calorias =
                bmr * factor;

            calorias =
                AjustarPorObjetivo(
                    calorias,
                    objetivo);

            return Math.Round(
                calorias,
                0);
        }

        private decimal ObtenerFactorActividad(
    NivelActividad actividad)
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

        private decimal AjustarPorObjetivo(
    decimal calorias,
    ObjetivoNutricional objetivo)
        {
            return objetivo switch
            {
                ObjetivoNutricional.PerderPeso =>
                    calorias - 500,

                ObjetivoNutricional.GanarPeso =>
                    calorias + 500,

                _ => calorias
            };
        }

        public RequerimientoNutricional
    CalcularDistribucionMacros(
        decimal calorias,
        TipoDieta dieta)
        {
            decimal porcentajeCarbos;
            decimal porcentajeProteina;
            decimal porcentajeGrasa;

            switch (dieta)
            {
                case TipoDieta.BajaEnCarbohidratos:
                    porcentajeCarbos = 0.30m;
                    porcentajeProteina = 0.40m;
                    porcentajeGrasa = 0.30m;
                    break;

                case TipoDieta.AltaEnCarbohidratos:
                    porcentajeCarbos = 0.60m;
                    porcentajeProteina = 0.20m;
                    porcentajeGrasa = 0.20m;
                    break;

                case TipoDieta.Cetogenica:
                    porcentajeCarbos = 0.05m;
                    porcentajeProteina = 0.25m;
                    porcentajeGrasa = 0.70m;
                    break;

                default:
                    porcentajeCarbos = 0.50m;
                    porcentajeProteina = 0.20m;
                    porcentajeGrasa = 0.30m;
                    break;
            }

            var carbos =
                (calorias * porcentajeCarbos) / 4;

            var proteina =
                (calorias * porcentajeProteina) / 4;

            var grasa =
                (calorias * porcentajeGrasa) / 9;

            return new RequerimientoNutricional
            {
                CaloriasObjetivo = calorias,

                CarbohidratosGramos =
                    Math.Round(carbos, 0),

                ProteinasGramos =
                    Math.Round(proteina, 0),

                GrasasGramos =
                    Math.Round(grasa, 0)
            };
        }
    }
}

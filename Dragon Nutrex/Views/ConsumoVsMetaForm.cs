using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using Dragon_Nutrex.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class ConsumoVsMetaForm : Form
    {
        private readonly ConsumoService
            _consumoService;

        public ConsumoVsMetaForm()
        {
            var repo =
    new ConsumoDiarioRepository();

            repo.CrearRegistroPrueba();
            InitializeComponent();

            _consumoService = new ConsumoService();

            dtpFecha.ValueChanged += (_, __) => ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            var fecha =
                dtpFecha.Value.Date;

            var meta =
                ObtenerMetaUsuario();

            var resumen =
                _consumoService
                .ObtenerResumenDiario(
                    fecha,
                    meta);

            if (!resumen.TieneRegistros)
            {
                lblCaloriasConsumidas.Text =
                    "0 kcal";

                lblCarbosConsumidos.Text =
                    "0 g";

                lblDiferencia.Text =
                    $"{meta} kcal restantes";

                lblMetaCalorias.Text =
                    $"0 kcal";

                return;
            }

            lblCaloriasConsumidas.Text =
                $"{resumen.CaloriasConsumidas} kcal";

            lblCarbosConsumidos.Text =
                $"{resumen.CarbohidratosConsumidos} g";

            lblMetaCalorias.Text =
                $"{resumen.MetaCalorias} kcal";

            lblDiferencia.Text =
                $"{resumen.DiferenciaCalorias} kcal";
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private decimal ObtenerMetaUsuario()
        {
            var nutricionService =
                new NutricionService();

            // Valores de ejemplo
            // Luego leerlos desde Usuario activo

            decimal peso = 70;
            decimal altura = 1.75m;
            int edad = 30;

            var actividad =
                NivelActividad.Moderado;

            var objetivo =
                ObjetivoNutricional.MantenerPeso;

            var dieta =
                TipoDieta.Balanceada;

            var resultado =
                nutricionService
                .CalcularRequerimiento(
                    peso,
                    altura,
                    edad,
                    actividad,
                    objetivo,
                    dieta);

            return resultado.CaloriasObjetivo;
        }
    }
}

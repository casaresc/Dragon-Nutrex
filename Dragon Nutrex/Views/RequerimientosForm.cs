using Dragon_Nutrex.Models;
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
    public partial class RequerimientosForm : Form
    {
        private NutricionService _nutricionService;

        public RequerimientosForm()
        {
            InitializeComponent();
            _nutricionService  = new NutricionService();
            cmbActividad.DataSource =
                Enum.GetValues(
                    typeof(NivelActividad));

            cmbObjetivo.DataSource =
                Enum.GetValues(
                    typeof(ObjetivoNutricional));

            cmbDieta.DataSource =
                Enum.GetValues(
                    typeof(TipoDieta));

            lblCalorias.Visible = false;
            lblGrasa.Visible = false;
            lblCarbos.Visible = false;
            lblProteina.Visible = false;

            txtPeso.TextChanged += (_, __) => Recalcular();

            txtAltura.TextChanged += (_, __) => Recalcular();

            txtEdad.TextChanged += (_, __) => Recalcular();

            cmbActividad.SelectedIndexChanged += (_, __) => Recalcular();

            cmbObjetivo.SelectedIndexChanged += (_, __) => Recalcular();

            cmbDieta.SelectedIndexChanged += (_, __) => Recalcular();
        }

        private void Recalcular()
        {
            if (!decimal.TryParse(txtPeso.Text, out decimal peso)) return;
            if (!decimal.TryParse(txtAltura.Text, out decimal altura)) return;
            if (!int.TryParse(txtEdad.Text, out int edad)) return;

            if (cmbActividad.SelectedItem is NivelActividad actividad &&
                cmbObjetivo.SelectedItem is ObjetivoNutricional objetivo &&
                cmbDieta.SelectedItem is TipoDieta dieta)
            {
                var resultado = _nutricionService.CalcularRequerimiento(
                    peso, altura, edad, actividad, objetivo, dieta);

                if (resultado != null)
                {
                    lblCalorias.Visible = true;
                    lblGrasa.Visible = true;
                    lblCarbos.Visible = true;
                    lblProteina.Visible = true;

                    lblCalorias.Text =
                        $"{resultado.CaloriasObjetivo} kcal";

                    lblCarbos.Text =
                        $"{resultado.CarbohidratosGramos} g";

                    lblProteina.Text =
                        $"{resultado.ProteinasGramos} g";

                    lblGrasa.Text =
                        $"{resultado.GrasasGramos} g";
                }
            }
        }
    }
}

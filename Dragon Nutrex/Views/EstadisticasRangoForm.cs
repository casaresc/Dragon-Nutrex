using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class EstadisticasRangoForm : Form
    {

        private readonly ConsumoService _consumoService;

        public EstadisticasRangoForm()
        {
            InitializeComponent();

            _consumoService =
                new ConsumoService();
        }

        private void btnCalcular_Click(
    object sender,
    EventArgs e)
        {
            var inicio =
                dtpFechaInicio.Value.Date;

            var fin =
                dtpFechaFin.Value.Date;

            if (inicio > fin)
            {
                MessageBox.Show(
                    "La fecha inicio no puede ser mayor que la fecha fin");

                return;
            }

            var resumen =
                _consumoService
                .ObtenerResumenPorRango(
                    inicio,
                    fin);

            lblTotalCalorias.Text =
                resumen.TotalCalorias
                .ToString();

            lblTotalCarbos.Text =
                resumen.TotalCarbohidratos
                .ToString();

            lblPromedioCalorias.Text =
                resumen.PromedioCalorias
                .ToString("F2");

            lblPromedioCarbos.Text =
                resumen.PromedioCarbohidratos
                .ToString("F2");

            lblDias.Text =
                resumen.DiasConRegistros
                .ToString();
        }

    }
}

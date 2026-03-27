using Dragon_Nutrex.Services;
using Dragon_Nutrex.Services.Dragon_Nutrex.Services;
using System;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class IMCForm : Form
    {
        private readonly SaludService
            _saludService;

        public IMCForm()
        {
            InitializeComponent();

            _saludService =
                new SaludService();
        }

        private void btnCalcular_Click(
            object sender,
            EventArgs e)
        {
            if (!decimal.TryParse(
                txtPeso.Text,
                out decimal peso))
                throw new Exception(
                    "Peso inválido.");

            if (!decimal.TryParse(
                txtAltura.Text,
                out decimal altura))
                throw new Exception(
                    "Altura inválida.");

            var imc =
                _saludService
                .CalcularIMC(
                    peso,
                    altura);

            lblIMC.Text =
                imc.ToString();

            lblIMC.Visible = true;
        }
    }
}
using Dragon_Nutrex.Controllers;
using Dragon_Nutrex.Models;
using System;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class IMCForm : Form, IEstadisticaFiltrable
    {
        private readonly NutricionController _nutricionController = new NutricionController();
        private readonly UsuarioController _usuarioController = new UsuarioController();

        public IMCForm()
        {
            InitializeComponent();
        }
        public void FiltrarPorUsuario(Guid usuarioId)
        {
            var usuario = _usuarioController.ObtenerPorId(usuarioId);
            if (usuario != null)
            {
                txtPeso.Text = usuario.Peso.ToString();
                txtAltura.Text = usuario.Altura.ToString();
                CalcularYMostrarIMC();
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            CalcularYMostrarIMC();
        }

        private void CalcularYMostrarIMC()
        {
            if (!decimal.TryParse(txtPeso.Text, out decimal peso) || peso <= 0)
            {
                return;
            }

            if (!decimal.TryParse(txtAltura.Text, out decimal altura) || altura <= 0)
            {
                return;
            }

            var imc = NutricionController.ObtenerIMC(peso, altura);

            if (imc > 0)
            {
                lblIMC.Text = imc.ToString("F2");
                lblIMC.Visible = true;
                DeterminarCategoriaIMC(imc);
            }
        }

        private void DeterminarCategoriaIMC(decimal imc)
        {
            string categoria = imc switch
            {
                < 18.5m => "Bajo peso",
                < 25m => "Normal",
                < 30m => "Sobrepeso",
                _ => "Obesidad"
            };

            lblCategoria.Text = $"Categoría: {categoria}";
            lblCategoria.Visible = true;
        }
    }
}
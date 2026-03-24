using Dragon_Nutrex.Controllers;
using Dragon_Nutrex.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class UsuarioForm : Form
    {
        public UsuarioForm()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                Usuario usuario = new Usuario
                {
                    Nombre = txtNombre.Text,
                    Peso = double.Parse(txtPeso.Text),
                    Altura = double.Parse(txtAltura.Text),
                    Objetivo = cmbObjetivo.Text,
                    NivelActividad = cmbActividad.Text,
                    TipoDieta = cmbDieta.Text
                };

                UsuarioController controller = new UsuarioController();
                controller.CrearUsuario(usuario);

                MessageBox.Show("Usuario guardado correctamente");

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UsuarioForm_Load(object sender, EventArgs e)
        {
            cmbObjetivo.Items.Add("Mantener");
            cmbObjetivo.Items.Add("Perder grasa");
            cmbObjetivo.Items.Add("Ganar masa");

            cmbActividad.Items.Add("Sedentario");
            cmbActividad.Items.Add("Ligero");
            cmbActividad.Items.Add("Moderado");
            cmbActividad.Items.Add("Intenso");

            cmbDieta.Items.Add("Estándar");
            cmbDieta.Items.Add("Keto");
            cmbDieta.Items.Add("Vegetariano");
        }

        private bool ValidarCampos()
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese el nombre");
                return false;
            }

            if (!double.TryParse(txtPeso.Text, out double peso))
            {
                MessageBox.Show("Peso inválido");
                return false;
            }

            if (peso <= 0)
            {
                MessageBox.Show("El peso debe ser mayor a 0");
                return false;
            }

            if (!double.TryParse(txtAltura.Text, out double altura))
            {
                MessageBox.Show("Altura inválida");
                return false;
            }

            if (altura <= 0)
            {
                MessageBox.Show("La altura debe ser mayor a 0");
                return false;
            }

            if (cmbObjetivo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un objetivo");
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtPeso.Clear();
            txtAltura.Clear();

            cmbObjetivo.SelectedIndex = -1;
            cmbActividad.SelectedIndex = -1;
            cmbDieta.SelectedIndex = -1;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}

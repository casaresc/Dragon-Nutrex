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
        private Usuario? _usuarioEditar = null;
        private UsuarioController _controller = new UsuarioController();

        public UsuarioForm()
        {
            InitializeComponent();
        }


        public UsuarioForm(Usuario usuario)
        {
            InitializeComponent();

            _usuarioEditar = usuario;
        }

        private void CargarDatosUsuario()
        {
            txtNombre.Text = _usuarioEditar?.Nombre ?? string.Empty;
            txtPeso.Text = _usuarioEditar?.Peso.ToString() ?? string.Empty;
            txtAltura.Text = _usuarioEditar?.Altura.ToString() ?? string.Empty;

            cmbObjetivo.SelectedItem = _usuarioEditar?.Objetivo ?? string.Empty;
            cmbActividad.SelectedItem = _usuarioEditar?.NivelActividad ?? string.Empty;
            cmbDieta.SelectedItem = _usuarioEditar?.TipoDieta ?? string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                Usuario usuario = new Usuario
                {
                    Id = _usuarioEditar != null ? _usuarioEditar.Id : Guid.NewGuid(),
                    Nombre = txtNombre.Text,
                    Peso = double.Parse(txtPeso.Text),
                    Altura = double.Parse(txtAltura.Text),
                    Objetivo = cmbObjetivo.SelectedItem?.ToString() ?? "No seleccionado",
                    NivelActividad = cmbActividad.SelectedItem?.ToString() ?? "No seleccionado",
                    TipoDieta = cmbDieta.SelectedItem?.ToString() ?? "No seleccionado"
                };

                if (_usuarioEditar == null)
                {
                    _controller.CrearUsuario(usuario);

                    MessageBox.Show(
                        "Usuario creado correctamente",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    LimpiarCampos();
                }
                else
                {
                    _controller.ActualizarUsuario(usuario);

                    MessageBox.Show(
                        "Usuario actualizado correctamente",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
           
            if (_usuarioEditar != null)
            {
                CargarDatosUsuario();
            }
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

            if (cmbActividad.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un nivel de actividad");
                return false;
            }

            if (cmbDieta.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de dieta");
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

        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            UsuarioListForm lista = new UsuarioListForm();

            lista.ShowDialog();
        }
    }
}

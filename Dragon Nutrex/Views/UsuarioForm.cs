using Dragon_Nutrex.Common;
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
            if (_usuarioEditar == null) return;

            txtNombre.Text = _usuarioEditar.Nombre;
            txtPeso.Text = _usuarioEditar.Peso.ToString();
            txtAltura.Text = _usuarioEditar.Altura.ToString();
            txtEdad.Text = _usuarioEditar.Edad.ToString();

            cmbObjetivo.SelectedItem = _usuarioEditar.Objetivo.ToString();
            cmbActividad.SelectedItem = _usuarioEditar.NivelActividad.ToString();
            cmbDieta.SelectedItem = _usuarioEditar.TipoDieta.ToString();
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
                    Peso = decimal.Parse(txtPeso.Text),
                    Altura = decimal.Parse(txtAltura.Text),
                    Edad = int.Parse(txtEdad.Text),
                    Objetivo = (ObjetivoNutricional)(cmbObjetivo.SelectedItem ?? ObjetivoNutricional.MantenerPeso),
                    NivelActividad = (NivelActividad)(cmbActividad.SelectedItem ?? NivelActividad.Moderado),
                    TipoDieta = (TipoDieta)(cmbDieta.SelectedItem ?? TipoDieta.Balanceada),
                    Activo = true // Aseguramos que el nuevo usuario esté activo
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
                GlobalExceptionHandler.Handle(ex);
            }
        }
        private void UsuarioForm_Load(object sender, EventArgs e)
        {
            cmbObjetivo.Items.AddRange(Enum.GetNames(typeof(ObjetivoNutricional)));
            cmbActividad.Items.AddRange(Enum.GetNames(typeof(NivelActividad)));
            cmbDieta.Items.AddRange(Enum.GetNames(typeof(TipoDieta)));

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

            if (!decimal.TryParse(txtPeso.Text, out decimal peso))
            {
                MessageBox.Show("Peso inválido");
                return false;
            }

            if (peso <= 0)
            {
                MessageBox.Show("El peso debe ser mayor a 0");
                return false;
            }

            if (!decimal.TryParse(txtAltura.Text, out decimal altura))
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
            this.Close();
        }

        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            UsuarioListForm lista = new UsuarioListForm();

            lista.ShowDialog();
        }
    }
}

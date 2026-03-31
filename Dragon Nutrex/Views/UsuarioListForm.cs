using Dragon_Nutrex.Controllers;
using Dragon_Nutrex.Models;
using System;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class UsuarioListForm : Form
    {
        private readonly UsuarioController _controller = new UsuarioController();

        public UsuarioListForm()
        {
            InitializeComponent();
            this.Load += UsuarioListForm_Load;
        }
        private void UsuarioListForm_Load(object? sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            ConfigurarGrid();
            CargarUsuarios();
        }
        private void ConfigurarGrid()
        {
            dgvUsuarios.AutoGenerateColumns = true;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.ReadOnly = true;
        }

        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = _controller.ObtenerUsuariosActivos();

            if (dgvUsuarios.Columns["Id"] != null) dgvUsuarios.Columns["Id"]?.Visible = false;
            if (dgvUsuarios.Columns["Activo"] != null) dgvUsuarios.Columns["Activo"]?.Visible = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow?.DataBoundItem is Usuario usuario)
            {
                var confirm = MessageBox.Show($"¿Está seguro de eliminar a {usuario.Nombre}?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _controller.EliminarUsuario(usuario.Id);
                    CargarUsuarios();
                    MessageBox.Show("Usuario eliminado correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario de la lista.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow?.DataBoundItem is Usuario usuarioSeleccionado)
            {
                UsuarioForm formEditar = new UsuarioForm(usuarioSeleccionado);
                formEditar.ShowDialog();
                CargarUsuarios();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e) => CargarUsuarios();
        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var formAgregar = new UsuarioForm())
            {
                formAgregar.ShowDialog();
                CargarUsuarios();
            }
        }
    }
}
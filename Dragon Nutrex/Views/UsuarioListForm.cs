using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class UsuarioListForm : Form
    {
        private UsuarioRepository repo = new UsuarioRepository();

        public UsuarioListForm()
        {
            InitializeComponent();

            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            var usuarios = repo.GetAll();

            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarios;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.CurrentRow?.DataBoundItem is Usuario usuarioSeleccionado)
                {
                    UsuarioForm formEditar = new UsuarioForm(usuarioSeleccionado);
                    formEditar.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un usuario de la lista.");
                }

                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

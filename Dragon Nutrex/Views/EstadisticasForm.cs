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
    public partial class EstadisticasForm : Form
    {
        private readonly UsuarioController _usuarioController = new UsuarioController();
        public EstadisticasForm()
        {
            InitializeComponent();
            this.Load += EstadisticasForm_Load;
        }

        private void EstadisticasForm_Load(object? sender, EventArgs e)
        {
            ConfigurarComboUsuarios();
            MostrarFormEnTab(tpResumen, new ConsumoVsMetaForm());
            MostrarFormEnTab(tpHistorico, new EstadisticasRangoForm());
            MostrarFormEnTab(tpImc, new IMCForm());
            MostrarFormEnTab(tpRequerimientos, new RequerimientosForm());
        }

        private void ConfigurarComboUsuarios()
        {
            var usuarios = _usuarioController.GetUsuarios();
            cmbUsuarios.DataSource = usuarios;
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "Id";       
            cmbUsuarios.SelectedIndex = -1;       
        }

        private static void MostrarFormEnTab(TabPage pagina, Form formularioHijo)
        {
            pagina.Controls.Clear();

            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;

            pagina.Controls.Add(formularioHijo);
            pagina.Tag = formularioHijo;
            formularioHijo.Show();
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedValue is Guid usuarioId)
            {
                foreach (TabPage tab in tcEstadisticas.TabPages)
                {
                    foreach (Control control in tab.Controls)
                    {
                        if (control is IEstadisticaFiltrable formEstadistica)
                        {
                            formEstadistica.FiltrarPorUsuario(usuarioId);
                        }
                    }
                }
            }
        }
    }
}

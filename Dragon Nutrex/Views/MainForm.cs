using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            var form = new UsuarioForm();
            form.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            var form = new ProductosForm();
            form.ShowDialog();
        }

        private void btnMenús_Click(object sender, EventArgs e)
        {
            var form = new MenusForm();
            form.ShowDialog();
        }
        
        private void btnIMC_Click(object sender, EventArgs e)
        {
            var form = new IMCForm();
            form.ShowDialog();
        }

        private void btnRequerimientos_Click(object sender, EventArgs e)
        {
            var form = new RequerimientosForm();
            form.ShowDialog();
        }

        private void btnConsumoVsMeta_Click(object sender, EventArgs e)
        {
            var form = new ConsumoVsMetaForm();
            form.ShowDialog();
        }

        private void btnEstadisticasPorRango_Click(object sender, EventArgs e)
        {
            var form = new EstadisticasRangoForm();
            form.ShowDialog();
        }
    }
}

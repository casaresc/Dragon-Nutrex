using Dragon_Nutrex.Controllers;
using Dragon_Nutrex.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class ProductosForm : Form
    {
        private readonly ProductoController _controller = new ProductoController();
        private Producto? _productoSeleccionado = null;

        public ProductosForm()
        {
            InitializeComponent();
            ConfigurarGrid();
            this.Load += ProductosForm_Load;
        }

        private void ProductosForm_Load(object? sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            CargarCategorias();
            CargarProductos();
        }

        private void ConfigurarGrid()
        {

            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
        }

        private void CargarCategorias()
        {
            cmbCategoria.DataSource = Enum.GetValues(typeof(CategoriaProducto));
            cmbCategoria.SelectedIndex = -1;
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = _controller.ObtenerTodos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var producto = ValidarYObtenerDeFormulario();
            if (producto == null) return;

            _controller.Crear(producto);
            MessageBox.Show("Producto guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarProductos();
            LimpiarFormulario();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_productoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un producto de la lista.");
                return;
            }

            var editado = ValidarYObtenerDeFormulario();
            if (editado == null) return;

            editado.Id = _productoSeleccionado.Id;
            _controller.Actualizar(editado);

            CargarProductos();
            LimpiarFormulario();

            MessageBox.Show(
                "Producto Editado correctamente",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_productoSeleccionado == null) return;

            var confirm = MessageBox.Show($"¿Desea eliminar {_productoSeleccionado.Nombre}?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _controller.Eliminar(_productoSeleccionado.Id);
                CargarProductos();
                LimpiarFormulario();
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow?.DataBoundItem is Producto p)
            {
                _productoSeleccionado = p;
                txtNombre.Text = p.Nombre;
                cmbCategoria.SelectedItem = p.Categoria;
                nudCalorias.Value = p.Calorias;
                nudProteina.Value = p.Proteina;
                nudCarbohidratos.Value = p.Carbohidratos;
                nudGrasas.Value = p.Grasas;
                nudPorcion.Value = p.PorcionGramos;
            }
        }

        private Producto? ValidarYObtenerDeFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.");
                return null;
            }

            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categoría.");
                return null;
            }

            return new Producto
            {
                Id = Guid.NewGuid(),
                Nombre = txtNombre.Text.Trim(),
                Categoria = (CategoriaProducto)(cmbCategoria.SelectedItem ?? CategoriaProducto.Proteina),
                Calorias = nudCalorias.Value,
                Proteina = nudProteina.Value,
                Carbohidratos = nudCarbohidratos.Value,
                Grasas = nudGrasas.Value,
                PorcionGramos = nudPorcion.Value,
                Activo = true
            };
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            cmbCategoria.SelectedIndex = -1;
            nudCalorias.Value = 0;
            nudProteina.Value = 0;
            nudCarbohidratos.Value = 0;
            nudGrasas.Value = 0;
            nudPorcion.Value = 0;
            _productoSeleccionado = null;
            dgvProductos.ClearSelection();
        }

        private void btnLimpiar_Click(object sender, EventArgs e) => LimpiarFormulario();
    }
}
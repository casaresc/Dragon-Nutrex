namespace Dragon_Nutrex.Views
{
    partial class MenuDetalleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblMenuNombre = new Label();
            cmbProductos = new ComboBox();
            txtPorcion = new TextBox();
            btnAgregarProducto = new Button();
            dgvProductosMenu = new DataGridView();
            lblPorcion = new Label();
            lblProductos = new Label();
            btnEliminarProducto = new Button();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProductosMenu).BeginInit();
            SuspendLayout();
            // 
            // lblMenuNombre
            // 
            lblMenuNombre.AutoSize = true;
            lblMenuNombre.Location = new Point(0, 253);
            lblMenuNombre.Name = "lblMenuNombre";
            lblMenuNombre.Size = new Size(82, 15);
            lblMenuNombre.TabIndex = 0;
            lblMenuNombre.Text = "MenuNombre";
            // 
            // cmbProductos
            // 
            cmbProductos.FormattingEnabled = true;
            cmbProductos.Location = new Point(112, 298);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(121, 23);
            cmbProductos.TabIndex = 1;
            // 
            // txtPorcion
            // 
            txtPorcion.Location = new Point(275, 298);
            txtPorcion.Name = "txtPorcion";
            txtPorcion.Size = new Size(100, 23);
            txtPorcion.TabIndex = 2;
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.Location = new Point(112, 371);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(75, 23);
            btnAgregarProducto.TabIndex = 3;
            btnAgregarProducto.Text = "Agregar Producto";
            btnAgregarProducto.UseVisualStyleBackColor = true;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // dgvProductosMenu
            // 
            dgvProductosMenu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductosMenu.Dock = DockStyle.Top;
            dgvProductosMenu.Location = new Point(0, 0);
            dgvProductosMenu.MultiSelect = false;
            dgvProductosMenu.Name = "dgvProductosMenu";
            dgvProductosMenu.ReadOnly = true;
            dgvProductosMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductosMenu.Size = new Size(800, 250);
            dgvProductosMenu.TabIndex = 4;
            // 
            // lblPorcion
            // 
            lblPorcion.AutoSize = true;
            lblPorcion.Location = new Point(275, 280);
            lblPorcion.Name = "lblPorcion";
            lblPorcion.Size = new Size(48, 15);
            lblPorcion.TabIndex = 5;
            lblPorcion.Text = "Porcion";
            // 
            // lblProductos
            // 
            lblProductos.AutoSize = true;
            lblProductos.Location = new Point(112, 280);
            lblProductos.Name = "lblProductos";
            lblProductos.Size = new Size(61, 15);
            lblProductos.TabIndex = 6;
            lblProductos.Text = "Productos";
            // 
            // btnEliminarProducto
            // 
            btnEliminarProducto.Location = new Point(248, 371);
            btnEliminarProducto.Name = "btnEliminarProducto";
            btnEliminarProducto.Size = new Size(75, 23);
            btnEliminarProducto.TabIndex = 7;
            btnEliminarProducto.Text = "Eliminar";
            btnEliminarProducto.UseVisualStyleBackColor = true;
            btnEliminarProducto.Click += btnEliminarProducto_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(355, 371);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 8;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // MenuDetalleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCerrar);
            Controls.Add(btnEliminarProducto);
            Controls.Add(lblProductos);
            Controls.Add(lblPorcion);
            Controls.Add(dgvProductosMenu);
            Controls.Add(btnAgregarProducto);
            Controls.Add(txtPorcion);
            Controls.Add(cmbProductos);
            Controls.Add(lblMenuNombre);
            Name = "MenuDetalleForm";
            Text = "MenuDetalleForm";
            ((System.ComponentModel.ISupportInitialize)dgvProductosMenu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMenuNombre;
        private ComboBox cmbProductos;
        private TextBox txtPorcion;
        private Button btnAgregarProducto;
        private DataGridView dgvProductosMenu;
        private Label lblPorcion;
        private Label lblProductos;
        private Button btnEliminarProducto;
        private Button btnCerrar;
    }
}
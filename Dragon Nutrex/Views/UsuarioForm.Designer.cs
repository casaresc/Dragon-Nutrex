namespace Dragon_Nutrex.Views
{
    partial class UsuarioForm
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
            txtNombre = new TextBox();
            txtPeso = new TextBox();
            txtAltura = new TextBox();
            cmbObjetivo = new ComboBox();
            cmbActividad = new ComboBox();
            cmbDieta = new ComboBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            btnLimpiar = new Button();
            lblNombre = new Label();
            lblPeso = new Label();
            lblAltura = new Label();
            lblObjetivo = new Label();
            lblNivelDeActividad = new Label();
            lblTipodeDieta = new Label();
            label1 = new Label();
            label2 = new Label();
            lblTitulo = new Label();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtNombre.Location = new Point(258, 106);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(100, 23);
            txtNombre.TabIndex = 0;
            // 
            // txtPeso
            // 
            txtPeso.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtPeso.Location = new Point(258, 150);
            txtPeso.Name = "txtPeso";
            txtPeso.Size = new Size(100, 23);
            txtPeso.TabIndex = 1;
            // 
            // txtAltura
            // 
            txtAltura.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtAltura.Location = new Point(258, 194);
            txtAltura.Name = "txtAltura";
            txtAltura.Size = new Size(100, 23);
            txtAltura.TabIndex = 2;
            // 
            // cmbObjetivo
            // 
            cmbObjetivo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cmbObjetivo.FormattingEnabled = true;
            cmbObjetivo.Location = new Point(396, 101);
            cmbObjetivo.Name = "cmbObjetivo";
            cmbObjetivo.Size = new Size(121, 23);
            cmbObjetivo.TabIndex = 3;
            // 
            // cmbActividad
            // 
            cmbActividad.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cmbActividad.FormattingEnabled = true;
            cmbActividad.Location = new Point(396, 150);
            cmbActividad.Name = "cmbActividad";
            cmbActividad.Size = new Size(121, 23);
            cmbActividad.TabIndex = 4;
            // 
            // cmbDieta
            // 
            cmbDieta.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cmbDieta.FormattingEnabled = true;
            cmbDieta.Location = new Point(396, 198);
            cmbDieta.Name = "cmbDieta";
            cmbDieta.Size = new Size(121, 23);
            cmbDieta.TabIndex = 5;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnGuardar.Location = new Point(258, 279);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCancelar.Location = new Point(352, 279);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnLimpiar.Location = new Point(445, 279);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(75, 23);
            btnLimpiar.TabIndex = 8;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // lblNombre
            // 
            lblNombre.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(258, 88);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 9;
            lblNombre.Text = "Nombre";
            // 
            // lblPeso
            // 
            lblPeso.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblPeso.AutoSize = true;
            lblPeso.Location = new Point(258, 132);
            lblPeso.Name = "lblPeso";
            lblPeso.Size = new Size(32, 15);
            lblPeso.TabIndex = 10;
            lblPeso.Text = "Peso";
            // 
            // lblAltura
            // 
            lblAltura.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblAltura.AutoSize = true;
            lblAltura.Location = new Point(258, 176);
            lblAltura.Name = "lblAltura";
            lblAltura.Size = new Size(39, 15);
            lblAltura.TabIndex = 11;
            lblAltura.Text = "Altura";
            // 
            // lblObjetivo
            // 
            lblObjetivo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblObjetivo.AutoSize = true;
            lblObjetivo.Location = new Point(399, 83);
            lblObjetivo.Name = "lblObjetivo";
            lblObjetivo.Size = new Size(52, 15);
            lblObjetivo.TabIndex = 12;
            lblObjetivo.Text = "Objetivo";
            // 
            // lblNivelDeActividad
            // 
            lblNivelDeActividad.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblNivelDeActividad.AutoSize = true;
            lblNivelDeActividad.Location = new Point(396, 132);
            lblNivelDeActividad.Name = "lblNivelDeActividad";
            lblNivelDeActividad.Size = new Size(103, 15);
            lblNivelDeActividad.TabIndex = 13;
            lblNivelDeActividad.Text = "Nivel de Actividad";
            // 
            // lblTipodeDieta
            // 
            lblTipodeDieta.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTipodeDieta.AutoSize = true;
            lblTipodeDieta.Location = new Point(396, 180);
            lblTipodeDieta.Name = "lblTipodeDieta";
            lblTipodeDieta.Size = new Size(76, 15);
            lblTipodeDieta.TabIndex = 14;
            lblTipodeDieta.Text = "Tipo de Dieta";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(399, 132);
            label1.Name = "label1";
            label1.Size = new Size(103, 15);
            label1.TabIndex = 13;
            label1.Text = "Nivel de Actividad";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(399, 180);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 14;
            label2.Text = "Tipo de Dieta";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(318, 51);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(109, 15);
            lblTitulo.TabIndex = 15;
            lblTitulo.Text = "Registro de Usuario";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UsuarioForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTitulo);
            Controls.Add(label2);
            Controls.Add(lblTipodeDieta);
            Controls.Add(label1);
            Controls.Add(lblNivelDeActividad);
            Controls.Add(lblObjetivo);
            Controls.Add(lblAltura);
            Controls.Add(lblPeso);
            Controls.Add(lblNombre);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(cmbDieta);
            Controls.Add(cmbActividad);
            Controls.Add(cmbObjetivo);
            Controls.Add(txtAltura);
            Controls.Add(txtPeso);
            Controls.Add(txtNombre);
            Name = "UsuarioForm";
            Text = "UsuarioForm";
            Load += UsuarioForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtPeso;
        private TextBox txtAltura;
        private ComboBox cmbObjetivo;
        private ComboBox cmbActividad;
        private ComboBox cmbDieta;
        private Button btnGuardar;
        private Button btnCancelar;
        private Button btnLimpiar;
        private Label lblNombre;
        private Label lblPeso;
        private Label lblAltura;
        private Label lblObjetivo;
        private Label lblNivelDeActividad;
        private Label lblTipodeDieta;
        private Label label1;
        private Label label2;
        private Label lblTitulo;
    }
}
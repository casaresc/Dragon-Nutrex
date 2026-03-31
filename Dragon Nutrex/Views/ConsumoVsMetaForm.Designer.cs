namespace Dragon_Nutrex.Views
{
    partial class ConsumoVsMetaForm
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
            dtpFecha = new DateTimePicker();
            lblCaloriasConsumidas = new Label();
            label2 = new Label();
            label3 = new Label();
            lblCarbosConsumidos = new Label();
            lblMetaCalorias = new Label();
            label6 = new Label();
            lblDiferencia = new Label();
            label8 = new Label();
            label9 = new Label();
            btnActualizar = new Button();
            SuspendLayout();
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(275, 109);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(200, 23);
            dtpFecha.TabIndex = 0;
            // 
            // lblCaloriasConsumidas
            // 
            lblCaloriasConsumidas.AutoSize = true;
            lblCaloriasConsumidas.Location = new Point(275, 215);
            lblCaloriasConsumidas.Name = "lblCaloriasConsumidas";
            lblCaloriasConsumidas.Size = new Size(0, 15);
            lblCaloriasConsumidas.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(275, 200);
            label2.Name = "label2";
            label2.Size = new Size(116, 15);
            label2.TabIndex = 2;
            label2.Text = "Calorias consumidas";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(456, 200);
            label3.Name = "label3";
            label3.Size = new Size(114, 15);
            label3.TabIndex = 3;
            label3.Text = "Carbos Consumidos";
            // 
            // lblCarbosConsumidos
            // 
            lblCarbosConsumidos.AutoSize = true;
            lblCarbosConsumidos.Location = new Point(456, 215);
            lblCarbosConsumidos.Name = "lblCarbosConsumidos";
            lblCarbosConsumidos.Size = new Size(0, 15);
            lblCarbosConsumidos.TabIndex = 4;
            // 
            // lblMetaCalorias
            // 
            lblMetaCalorias.AutoSize = true;
            lblMetaCalorias.Location = new Point(275, 271);
            lblMetaCalorias.Name = "lblMetaCalorias";
            lblMetaCalorias.Size = new Size(0, 15);
            lblMetaCalorias.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(275, 256);
            label6.Name = "label6";
            label6.Size = new Size(79, 15);
            label6.TabIndex = 5;
            label6.Text = "Meta Calorias";
            // 
            // lblDiferencia
            // 
            lblDiferencia.AutoSize = true;
            lblDiferencia.Location = new Point(456, 271);
            lblDiferencia.Name = "lblDiferencia";
            lblDiferencia.Size = new Size(0, 15);
            lblDiferencia.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(456, 256);
            label8.Name = "label8";
            label8.Size = new Size(60, 15);
            label8.TabIndex = 7;
            label8.Text = "Diferencia";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(275, 91);
            label9.Name = "label9";
            label9.Size = new Size(38, 15);
            label9.TabIndex = 9;
            label9.Text = "Fecha";
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(275, 315);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(75, 23);
            btnActualizar.TabIndex = 10;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Visible = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // ConsumoVsMetaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnActualizar);
            Controls.Add(label9);
            Controls.Add(lblDiferencia);
            Controls.Add(label8);
            Controls.Add(lblMetaCalorias);
            Controls.Add(label6);
            Controls.Add(lblCarbosConsumidos);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblCaloriasConsumidas);
            Controls.Add(dtpFecha);
            Name = "ConsumoVsMetaForm";
            Text = "ConsumoVsMetaForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpFecha;
        private Label lblCaloriasConsumidas;
        private Label label2;
        private Label label3;
        private Label lblCarbosConsumidos;
        private Label lblMetaCalorias;
        private Label label6;
        private Label lblDiferencia;
        private Label label8;
        private Label label9;
        private Button btnActualizar;
    }
}
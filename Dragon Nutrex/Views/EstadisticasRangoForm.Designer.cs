namespace Dragon_Nutrex.Views
{
    partial class EstadisticasRangoForm
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
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            btnCalcular = new Button();
            label1 = new Label();
            lblTotalCalorias = new Label();
            lblTotalCarbos = new Label();
            label3 = new Label();
            lblPromedioCalorias = new Label();
            label5 = new Label();
            lblPromedioCarbos = new Label();
            label7 = new Label();
            lblDias = new Label();
            label9 = new Label();
            label2 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(164, 145);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(200, 23);
            dtpFechaInicio.TabIndex = 0;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(441, 145);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(200, 23);
            dtpFechaFin.TabIndex = 1;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(566, 225);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(75, 23);
            btnCalcular.TabIndex = 2;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(109, 287);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // lblTotalCalorias
            // 
            lblTotalCalorias.AutoSize = true;
            lblTotalCalorias.Location = new Point(109, 312);
            lblTotalCalorias.Name = "lblTotalCalorias";
            lblTotalCalorias.Size = new Size(87, 15);
            lblTotalCalorias.TabIndex = 5;
            lblTotalCalorias.Text = "lblTotalCalorias";
            // 
            // lblTotalCarbos
            // 
            lblTotalCarbos.AutoSize = true;
            lblTotalCarbos.Location = new Point(233, 312);
            lblTotalCarbos.Name = "lblTotalCarbos";
            lblTotalCarbos.Size = new Size(82, 15);
            lblTotalCarbos.TabIndex = 7;
            lblTotalCarbos.Text = "lblTotalCarbos";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(233, 287);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 6;
            label3.Text = "label3";
            // 
            // lblPromedioCalorias
            // 
            lblPromedioCalorias.AutoSize = true;
            lblPromedioCalorias.Location = new Point(352, 312);
            lblPromedioCalorias.Name = "lblPromedioCalorias";
            lblPromedioCalorias.Size = new Size(114, 15);
            lblPromedioCalorias.TabIndex = 9;
            lblPromedioCalorias.Text = "lblPromedioCalorias";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(352, 287);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 8;
            label5.Text = "label5";
            // 
            // lblPromedioCarbos
            // 
            lblPromedioCarbos.AutoSize = true;
            lblPromedioCarbos.Location = new Point(499, 312);
            lblPromedioCarbos.Name = "lblPromedioCarbos";
            lblPromedioCarbos.Size = new Size(109, 15);
            lblPromedioCarbos.TabIndex = 11;
            lblPromedioCarbos.Text = "lblPromedioCarbos";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(499, 287);
            label7.Name = "label7";
            label7.Size = new Size(38, 15);
            label7.TabIndex = 10;
            label7.Text = "label7";
            // 
            // lblDias
            // 
            lblDias.AutoSize = true;
            lblDias.Location = new Point(625, 312);
            lblDias.Name = "lblDias";
            lblDias.Size = new Size(42, 15);
            lblDias.TabIndex = 13;
            lblDias.Text = "lblDias";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(625, 287);
            label9.Name = "label9";
            label9.Size = new Size(38, 15);
            label9.TabIndex = 12;
            label9.Text = "label9";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(164, 127);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 14;
            label2.Text = "label2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(441, 127);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 15;
            label4.Text = "label4";
            // 
            // EstadisticasRangoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(lblDias);
            Controls.Add(label9);
            Controls.Add(lblPromedioCarbos);
            Controls.Add(label7);
            Controls.Add(lblPromedioCalorias);
            Controls.Add(label5);
            Controls.Add(lblTotalCarbos);
            Controls.Add(label3);
            Controls.Add(lblTotalCalorias);
            Controls.Add(label1);
            Controls.Add(btnCalcular);
            Controls.Add(dtpFechaFin);
            Controls.Add(dtpFechaInicio);
            Name = "EstadisticasRangoForm";
            Text = "EstadisticasRangoForm";
            ResumeLayout(false);
            PerformLayout();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private Button btnCalcular;
        private Label label1;
        private Label lblTotalCalorias;
        private Label lblTotalCarbos;
        private Label label3;
        private Label lblPromedioCalorias;
        private Label label5;
        private Label lblPromedioCarbos;
        private Label label7;
        private Label lblDias;
        private Label label9;
        private Label label2;
        private Label label4;
    }
}
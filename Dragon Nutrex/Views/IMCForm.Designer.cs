namespace Dragon_Nutrex.Views
{
    partial class IMCForm
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
            lblPeso = new Label();
            txtPeso = new TextBox();
            lblAltura = new Label();
            txtAltura = new TextBox();
            lblResultado = new Label();
            lblIMC = new Label();
            btnCalcular = new Button();
            SuspendLayout();
            // 
            // lblPeso
            // 
            lblPeso.AutoSize = true;
            lblPeso.Location = new Point(337, 152);
            lblPeso.Name = "lblPeso";
            lblPeso.Size = new Size(56, 15);
            lblPeso.TabIndex = 0;
            lblPeso.Text = "Peso (kg)";
            // 
            // txtPeso
            // 
            txtPeso.Location = new Point(337, 170);
            txtPeso.Name = "txtPeso";
            txtPeso.Size = new Size(100, 23);
            txtPeso.TabIndex = 1;
            // 
            // lblAltura
            // 
            lblAltura.AutoSize = true;
            lblAltura.Location = new Point(337, 206);
            lblAltura.Name = "lblAltura";
            lblAltura.Size = new Size(61, 15);
            lblAltura.TabIndex = 2;
            lblAltura.Text = "Altura (m)";
            // 
            // txtAltura
            // 
            txtAltura.Location = new Point(337, 224);
            txtAltura.Name = "txtAltura";
            txtAltura.Size = new Size(100, 23);
            txtAltura.TabIndex = 3;
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.Location = new Point(337, 260);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(59, 15);
            lblResultado.TabIndex = 4;
            lblResultado.Text = "Resultado";
            // 
            // lblIMC
            // 
            lblIMC.AutoSize = true;
            lblIMC.Location = new Point(337, 289);
            lblIMC.Name = "lblIMC";
            lblIMC.Size = new Size(29, 15);
            lblIMC.TabIndex = 5;
            lblIMC.Text = "IMC";
            lblIMC.Visible = false;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(337, 329);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(75, 23);
            btnCalcular.TabIndex = 6;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // IMCForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCalcular);
            Controls.Add(lblIMC);
            Controls.Add(lblResultado);
            Controls.Add(txtAltura);
            Controls.Add(lblAltura);
            Controls.Add(txtPeso);
            Controls.Add(lblPeso);
            Name = "IMCForm";
            Text = "IMCForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPeso;
        private TextBox txtPeso;
        private Label lblAltura;
        private TextBox txtAltura;
        private Label lblResultado;
        private Label lblIMC;
        private Button btnCalcular;
    }
}
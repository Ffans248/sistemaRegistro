namespace sistemaRegistro
{
    partial class CambiarPass
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
            this.btnRegresar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContraActual = new System.Windows.Forms.TextBox();
            this.txtContraNueva = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRegresar
            // 
            this.btnRegresar.Location = new System.Drawing.Point(502, 13);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(85, 40);
            this.btnRegresar.TabIndex = 67;
            this.btnRegresar.Text = "Menú";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(114, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(302, 41);
            this.label7.TabIndex = 68;
            this.label7.Text = "Cambiar Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 69;
            this.label1.Text = "Contraseña Actual:";
            // 
            // txtContraActual
            // 
            this.txtContraActual.Location = new System.Drawing.Point(230, 118);
            this.txtContraActual.Name = "txtContraActual";
            this.txtContraActual.Size = new System.Drawing.Size(167, 22);
            this.txtContraActual.TabIndex = 70;
            this.txtContraActual.TextChanged += new System.EventHandler(this.txtContraActual_TextChanged);
            // 
            // txtContraNueva
            // 
            this.txtContraNueva.Location = new System.Drawing.Point(230, 169);
            this.txtContraNueva.Name = "txtContraNueva";
            this.txtContraNueva.Size = new System.Drawing.Size(167, 22);
            this.txtContraNueva.TabIndex = 72;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 71;
            this.label2.Text = "Contraseña Nueva:";
            // 
            // btnCambiar
            // 
            this.btnCambiar.Location = new System.Drawing.Point(253, 229);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(85, 35);
            this.btnCambiar.TabIndex = 73;
            this.btnCambiar.Text = "Cambiar";
            this.btnCambiar.UseVisualStyleBackColor = true;
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // CambiarPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 285);
            this.Controls.Add(this.btnCambiar);
            this.Controls.Add(this.txtContraNueva);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtContraActual);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnRegresar);
            this.Name = "CambiarPass";
            this.Text = "CambiarPass";
            this.Load += new System.EventHandler(this.CambiarPass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtContraActual;
        private System.Windows.Forms.TextBox txtContraNueva;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCambiar;
    }
}
namespace sistemaRegistro
{
    partial class Menu
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
            this.btnGestionarUsuarios = new FontAwesome.Sharp.IconButton();
            this.btnGestionarPermisos = new FontAwesome.Sharp.IconButton();
            this.btnchangePass = new FontAwesome.Sharp.IconButton();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGestionarUsuarios
            // 
            this.btnGestionarUsuarios.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnGestionarUsuarios.FlatAppearance.BorderSize = 0;
            this.btnGestionarUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarUsuarios.IconChar = FontAwesome.Sharp.IconChar.User;
            this.btnGestionarUsuarios.IconColor = System.Drawing.Color.Black;
            this.btnGestionarUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGestionarUsuarios.Location = new System.Drawing.Point(54, 63);
            this.btnGestionarUsuarios.Name = "btnGestionarUsuarios";
            this.btnGestionarUsuarios.Size = new System.Drawing.Size(256, 132);
            this.btnGestionarUsuarios.TabIndex = 3;
            this.btnGestionarUsuarios.Text = "Gestionar Usuarios";
            this.btnGestionarUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnGestionarUsuarios.UseVisualStyleBackColor = false;
            this.btnGestionarUsuarios.Click += new System.EventHandler(this.btnGestionarUsuarios_Click);
            // 
            // btnGestionarPermisos
            // 
            this.btnGestionarPermisos.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnGestionarPermisos.FlatAppearance.BorderSize = 0;
            this.btnGestionarPermisos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarPermisos.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            this.btnGestionarPermisos.IconColor = System.Drawing.Color.Black;
            this.btnGestionarPermisos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGestionarPermisos.Location = new System.Drawing.Point(438, 63);
            this.btnGestionarPermisos.Name = "btnGestionarPermisos";
            this.btnGestionarPermisos.Size = new System.Drawing.Size(256, 132);
            this.btnGestionarPermisos.TabIndex = 4;
            this.btnGestionarPermisos.Text = "Gestionar Permisos";
            this.btnGestionarPermisos.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnGestionarPermisos.UseVisualStyleBackColor = false;
            this.btnGestionarPermisos.Click += new System.EventHandler(this.btnGestionarPermisos_Click);
            // 
            // btnchangePass
            // 
            this.btnchangePass.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnchangePass.FlatAppearance.BorderSize = 0;
            this.btnchangePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnchangePass.IconChar = FontAwesome.Sharp.IconChar.Key;
            this.btnchangePass.IconColor = System.Drawing.Color.Black;
            this.btnchangePass.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnchangePass.Location = new System.Drawing.Point(268, 223);
            this.btnchangePass.Name = "btnchangePass";
            this.btnchangePass.Size = new System.Drawing.Size(256, 132);
            this.btnchangePass.TabIndex = 5;
            this.btnchangePass.Text = "Cambiar Contraseña";
            this.btnchangePass.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnchangePass.UseVisualStyleBackColor = false;
            this.btnchangePass.Click += new System.EventHandler(this.btnchangePass_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(365, 24);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(44, 16);
            this.lblUsuario.TabIndex = 6;
            this.lblUsuario.Text = "label1";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.btnchangePass);
            this.Controls.Add(this.btnGestionarPermisos);
            this.Controls.Add(this.btnGestionarUsuarios);
            this.Name = "Menu";
            this.Text = "Menu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Menu_FormClosed);
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private FontAwesome.Sharp.IconButton btnGestionarUsuarios;
        private FontAwesome.Sharp.IconButton btnGestionarPermisos;
        private FontAwesome.Sharp.IconButton btnchangePass;
        private System.Windows.Forms.Label lblUsuario;
    }
}
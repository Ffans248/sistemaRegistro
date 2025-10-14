namespace sistemaRegistro
{
    partial class Formularios
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
            this.btnPermitir = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.dgvFormularios = new System.Windows.Forms.DataGridView();
            this.btnDenegar = new FontAwesome.Sharp.IconButton();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormularios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPermitir
            // 
            this.btnPermitir.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnPermitir.FlatAppearance.BorderSize = 0;
            this.btnPermitir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPermitir.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnPermitir.IconColor = System.Drawing.Color.Black;
            this.btnPermitir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPermitir.IconSize = 20;
            this.btnPermitir.Location = new System.Drawing.Point(226, 457);
            this.btnPermitir.Name = "btnPermitir";
            this.btnPermitir.Size = new System.Drawing.Size(121, 55);
            this.btnPermitir.TabIndex = 34;
            this.btnPermitir.Text = "Permitir Acceso";
            this.btnPermitir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPermitir.UseVisualStyleBackColor = false;
            this.btnPermitir.Click += new System.EventHandler(this.btnPermitir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Correo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "Nombre:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "ID:";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(180, 87);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.ReadOnly = true;
            this.txtCorreo.Size = new System.Drawing.Size(100, 22);
            this.txtCorreo.TabIndex = 25;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(180, 46);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(100, 22);
            this.txtNombre.TabIndex = 24;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(180, 7);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(100, 22);
            this.txtId.TabIndex = 23;
            // 
            // dgvFormularios
            // 
            this.dgvFormularios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormularios.Location = new System.Drawing.Point(54, 518);
            this.dgvFormularios.Name = "dgvFormularios";
            this.dgvFormularios.RowHeadersWidth = 51;
            this.dgvFormularios.RowTemplate.Height = 24;
            this.dgvFormularios.Size = new System.Drawing.Size(672, 251);
            this.dgvFormularios.TabIndex = 22;
            this.dgvFormularios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFormularios_CellClick);
            // 
            // btnDenegar
            // 
            this.btnDenegar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnDenegar.FlatAppearance.BorderSize = 0;
            this.btnDenegar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDenegar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnDenegar.IconColor = System.Drawing.Color.Black;
            this.btnDenegar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDenegar.IconSize = 20;
            this.btnDenegar.Location = new System.Drawing.Point(446, 457);
            this.btnDenegar.Name = "btnDenegar";
            this.btnDenegar.Size = new System.Drawing.Size(121, 55);
            this.btnDenegar.TabIndex = 35;
            this.btnDenegar.Text = "Negar Acceso";
            this.btnDenegar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDenegar.UseVisualStyleBackColor = false;
            this.btnDenegar.Click += new System.EventHandler(this.btnDenegar_Click);
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Location = new System.Drawing.Point(54, 139);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.RowHeadersWidth = 51;
            this.dgvUsuarios.RowTemplate.Height = 24;
            this.dgvUsuarios.Size = new System.Drawing.Size(672, 251);
            this.dgvUsuarios.TabIndex = 36;
            this.dgvUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);
            // 
            // Formularios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 813);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.btnDenegar);
            this.Controls.Add(this.btnPermitir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.dgvFormularios);
            this.Name = "Formularios";
            this.Text = "Formularios";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormularios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnPermitir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.DataGridView dgvFormularios;
        private FontAwesome.Sharp.IconButton btnDenegar;
        private System.Windows.Forms.DataGridView dgvUsuarios;
    }
}
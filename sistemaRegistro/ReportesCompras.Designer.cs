namespace sistemaRegistro
{
    partial class ReportesCompras
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
            this.cmbProveedorReport = new System.Windows.Forms.ComboBox();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.btnBuscarCompras = new FontAwesome.Sharp.IconButton();
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.btnStockBajo = new FontAwesome.Sharp.IconButton();
            this.btnSinStock = new FontAwesome.Sharp.IconButton();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.btnRegresar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbProveedorReport
            // 
            this.cmbProveedorReport.FormattingEnabled = true;
            this.cmbProveedorReport.Location = new System.Drawing.Point(95, 56);
            this.cmbProveedorReport.Name = "cmbProveedorReport";
            this.cmbProveedorReport.Size = new System.Drawing.Size(121, 24);
            this.cmbProveedorReport.TabIndex = 0;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(40, 99);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(268, 22);
            this.dtpDesde.TabIndex = 1;
            // 
            // btnBuscarCompras
            // 
            this.btnBuscarCompras.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnBuscarCompras.FlatAppearance.BorderSize = 0;
            this.btnBuscarCompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCompras.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnBuscarCompras.IconColor = System.Drawing.Color.Black;
            this.btnBuscarCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarCompras.IconSize = 20;
            this.btnBuscarCompras.Location = new System.Drawing.Point(95, 127);
            this.btnBuscarCompras.Name = "btnBuscarCompras";
            this.btnBuscarCompras.Size = new System.Drawing.Size(121, 55);
            this.btnBuscarCompras.TabIndex = 35;
            this.btnBuscarCompras.Text = "Ver Proveedores";
            this.btnBuscarCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBuscarCompras.UseVisualStyleBackColor = false;
            this.btnBuscarCompras.Click += new System.EventHandler(this.btnBuscarCompras_Click);
            // 
            // dgvReporte
            // 
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Location = new System.Drawing.Point(40, 189);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.RowHeadersWidth = 51;
            this.dgvReporte.RowTemplate.Height = 24;
            this.dgvReporte.Size = new System.Drawing.Size(675, 188);
            this.dgvReporte.TabIndex = 36;
            // 
            // btnStockBajo
            // 
            this.btnStockBajo.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnStockBajo.FlatAppearance.BorderSize = 0;
            this.btnStockBajo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockBajo.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnStockBajo.IconColor = System.Drawing.Color.Black;
            this.btnStockBajo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStockBajo.IconSize = 20;
            this.btnStockBajo.Location = new System.Drawing.Point(268, 128);
            this.btnStockBajo.Name = "btnStockBajo";
            this.btnStockBajo.Size = new System.Drawing.Size(121, 55);
            this.btnStockBajo.TabIndex = 37;
            this.btnStockBajo.Text = "Ver Stock Bajo";
            this.btnStockBajo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStockBajo.UseVisualStyleBackColor = false;
            this.btnStockBajo.Click += new System.EventHandler(this.btnStockBajo_Click);
            // 
            // btnSinStock
            // 
            this.btnSinStock.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnSinStock.FlatAppearance.BorderSize = 0;
            this.btnSinStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSinStock.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnSinStock.IconColor = System.Drawing.Color.Black;
            this.btnSinStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSinStock.IconSize = 20;
            this.btnSinStock.Location = new System.Drawing.Point(437, 128);
            this.btnSinStock.Name = "btnSinStock";
            this.btnSinStock.Size = new System.Drawing.Size(121, 55);
            this.btnSinStock.TabIndex = 38;
            this.btnSinStock.Text = "Ver sin Stock";
            this.btnSinStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSinStock.UseVisualStyleBackColor = false;
            this.btnSinStock.Click += new System.EventHandler(this.btnSinStock_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(474, 99);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(268, 22);
            this.dtpHasta.TabIndex = 39;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.iconButton1.IconColor = System.Drawing.Color.Black;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 20;
            this.iconButton1.Location = new System.Drawing.Point(95, 383);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(121, 55);
            this.iconButton1.TabIndex = 40;
            this.iconButton1.Text = "Ver Stock Bajo";
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.iconButton2.IconColor = System.Drawing.Color.Black;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.IconSize = 20;
            this.iconButton2.Location = new System.Drawing.Point(283, 383);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Size = new System.Drawing.Size(121, 55);
            this.iconButton2.TabIndex = 41;
            this.iconButton2.Text = "Ver sin Stock";
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconButton2.UseVisualStyleBackColor = false;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // iconButton3
            // 
            this.iconButton3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.iconButton3.FlatAppearance.BorderSize = 0;
            this.iconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.iconButton3.IconColor = System.Drawing.Color.Black;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.IconSize = 20;
            this.iconButton3.Location = new System.Drawing.Point(465, 383);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Size = new System.Drawing.Size(121, 55);
            this.iconButton3.TabIndex = 42;
            this.iconButton3.Text = "Ver sin Stock";
            this.iconButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconButton3.UseVisualStyleBackColor = false;
            this.iconButton3.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // btnRegresar
            // 
            this.btnRegresar.Location = new System.Drawing.Point(642, 12);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(85, 40);
            this.btnRegresar.TabIndex = 67;
            this.btnRegresar.Text = "Menú";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // ReportesCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.iconButton3);
            this.Controls.Add(this.iconButton2);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.btnSinStock);
            this.Controls.Add(this.btnStockBajo);
            this.Controls.Add(this.dgvReporte);
            this.Controls.Add(this.btnBuscarCompras);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.cmbProveedorReport);
            this.Name = "ReportesCompras";
            this.Text = "Reportes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProveedorReport;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private FontAwesome.Sharp.IconButton btnBuscarCompras;
        private System.Windows.Forms.DataGridView dgvReporte;
        private FontAwesome.Sharp.IconButton btnStockBajo;
        private FontAwesome.Sharp.IconButton btnSinStock;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private System.Windows.Forms.Button btnRegresar;
    }
}
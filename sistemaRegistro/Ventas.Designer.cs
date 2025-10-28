namespace sistemaRegistro
{
    partial class Ventas
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
            this.label7 = new System.Windows.Forms.Label();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumeroFactura = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombreProducto = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtPrecioVenta = new System.Windows.Forms.TextBox();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDescuentoGeneral = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTotalVenta = new System.Windows.Forms.TextBox();
            this.btnConfirmarVenta = new System.Windows.Forms.Button();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.txtIdProducto = new System.Windows.Forms.TextBox();
            this.btnRegresar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(329, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 41);
            this.label7.TabIndex = 15;
            this.label7.Text = "Ventas";
            // 
            // cmbClientes
            // 
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(163, 105);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(121, 24);
            this.cmbClientes.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Cliente:";
            // 
            // txtNumeroFactura
            // 
            this.txtNumeroFactura.Location = new System.Drawing.Point(163, 142);
            this.txtNumeroFactura.Name = "txtNumeroFactura";
            this.txtNumeroFactura.ReadOnly = true;
            this.txtNumeroFactura.Size = new System.Drawing.Size(121, 22);
            this.txtNumeroFactura.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "N° Factura:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(37, 68);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(247, 22);
            this.dtpFecha.TabIndex = 20;
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.FormattingEnabled = true;
            this.cmbFormaPago.Location = new System.Drawing.Point(163, 179);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(121, 24);
            this.cmbFormaPago.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "Forma de Pago:";
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.Location = new System.Drawing.Point(618, 102);
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.ReadOnly = true;
            this.txtNombreProducto.Size = new System.Drawing.Size(101, 22);
            this.txtNombreProducto.TabIndex = 24;
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(618, 178);
            this.txtStock.Name = "txtStock";
            this.txtStock.ReadOnly = true;
            this.txtStock.Size = new System.Drawing.Size(101, 22);
            this.txtStock.TabIndex = 26;
            // 
            // txtPrecioVenta
            // 
            this.txtPrecioVenta.Location = new System.Drawing.Point(618, 140);
            this.txtPrecioVenta.Name = "txtPrecioVenta";
            this.txtPrecioVenta.ReadOnly = true;
            this.txtPrecioVenta.Size = new System.Drawing.Size(101, 22);
            this.txtPrecioVenta.TabIndex = 25;
            // 
            // numCantidad
            // 
            this.numCantidad.Location = new System.Drawing.Point(618, 215);
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(101, 22);
            this.numCantidad.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(451, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 16);
            this.label5.TabIndex = 29;
            this.label5.Text = "Nombre Producto:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(480, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 31;
            this.label6.Text = "Stock Actual:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(480, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 16);
            this.label8.TabIndex = 30;
            this.label8.Text = "Precio Venta:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(458, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 16);
            this.label9.TabIndex = 32;
            this.label9.Text = "Buscar Producto:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Nirmala UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(144, 265);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 25);
            this.label10.TabIndex = 33;
            this.label10.Text = "Carrito";
            // 
            // dgvDetalleVenta
            // 
            this.dgvDetalleVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(15, 293);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.RowHeadersWidth = 51;
            this.dgvDetalleVenta.RowTemplate.Height = 24;
            this.dgvDetalleVenta.Size = new System.Drawing.Size(642, 192);
            this.dgvDetalleVenta.TabIndex = 34;
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Location = new System.Drawing.Point(517, 249);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(140, 38);
            this.btnAgregarProducto.TabIndex = 35;
            this.btnAgregarProducto.Text = "Agregar Producto";
            this.btnAgregarProducto.UseVisualStyleBackColor = true;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(691, 384);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 16);
            this.label11.TabIndex = 37;
            this.label11.Text = "Descuento General:";
            // 
            // txtDescuentoGeneral
            // 
            this.txtDescuentoGeneral.Location = new System.Drawing.Point(876, 381);
            this.txtDescuentoGeneral.Name = "txtDescuentoGeneral";
            this.txtDescuentoGeneral.Size = new System.Drawing.Size(101, 22);
            this.txtDescuentoGeneral.TabIndex = 36;
            this.txtDescuentoGeneral.TextChanged += new System.EventHandler(this.txtDescuentoGeneral_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(691, 423);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 16);
            this.label12.TabIndex = 39;
            this.label12.Text = "Total Venta:";
            // 
            // txtTotalVenta
            // 
            this.txtTotalVenta.Location = new System.Drawing.Point(876, 420);
            this.txtTotalVenta.Name = "txtTotalVenta";
            this.txtTotalVenta.ReadOnly = true;
            this.txtTotalVenta.Size = new System.Drawing.Size(101, 22);
            this.txtTotalVenta.TabIndex = 38;
            // 
            // btnConfirmarVenta
            // 
            this.btnConfirmarVenta.Location = new System.Drawing.Point(754, 457);
            this.btnConfirmarVenta.Name = "btnConfirmarVenta";
            this.btnConfirmarVenta.Size = new System.Drawing.Size(140, 39);
            this.btnConfirmarVenta.TabIndex = 40;
            this.btnConfirmarVenta.Text = "Confirmar Venta";
            this.btnConfirmarVenta.UseVisualStyleBackColor = true;
            this.btnConfirmarVenta.Click += new System.EventHandler(this.btnConfirmarVenta_Click);
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.Location = new System.Drawing.Point(618, 65);
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(101, 22);
            this.txtBuscarProducto.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(458, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "Buscar Producto:";
            // 
            // dgvProductos
            // 
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(754, 21);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.RowHeadersWidth = 51;
            this.dgvProductos.RowTemplate.Height = 24;
            this.dgvProductos.Size = new System.Drawing.Size(394, 250);
            this.dgvProductos.TabIndex = 41;
            this.dgvProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellClick);
            // 
            // txtIdProducto
            // 
            this.txtIdProducto.Location = new System.Drawing.Point(618, 28);
            this.txtIdProducto.Name = "txtIdProducto";
            this.txtIdProducto.Size = new System.Drawing.Size(101, 22);
            this.txtIdProducto.TabIndex = 42;
            this.txtIdProducto.Visible = false;
            // 
            // btnRegresar
            // 
            this.btnRegresar.Location = new System.Drawing.Point(15, 9);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(85, 40);
            this.btnRegresar.TabIndex = 71;
            this.btnRegresar.Text = "Menú";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // Ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 508);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.txtIdProducto);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.btnConfirmarVenta);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtTotalVenta);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDescuentoGeneral);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.dgvDetalleVenta);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.txtPrecioVenta);
            this.Controls.Add(this.txtNombreProducto);
            this.Controls.Add(this.txtBuscarProducto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbFormaPago);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumeroFactura);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbClientes);
            this.Controls.Add(this.label7);
            this.Name = "Ventas";
            this.Text = "Ventas";
            this.Load += new System.EventHandler(this.Ventas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumeroFactura;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombreProducto;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtPrecioVenta;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDescuentoGeneral;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTotalVenta;
        private System.Windows.Forms.Button btnConfirmarVenta;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.TextBox txtIdProducto;
        private System.Windows.Forms.Button btnRegresar;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static sistemaRegistro.Login;

namespace sistemaRegistro
{
    public partial class Ventas : Form
    {
        DataTable dtDetalleVenta;
        public Ventas()
        {
            InitializeComponent();
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            cargarClientesCombo();
            cargarFormasPago();
            generarSiguienteFactura();
            cargarProductosDGV();
            configurarDetalleVentaDGV();
        }

        private void cargarClientesCombo()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT idCliente, nombre FROM tbCliente ORDER BY nombre", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DataRow fila = dt.NewRow();
                fila["idCliente"] = 0;
                fila["nombre"] = "[Seleccionar un Cliente]";
                dt.Rows.InsertAt(fila, 0);

                cmbClientes.DataSource = dt;
                cmbClientes.DisplayMember = "nombre";
                cmbClientes.ValueMember = "idCliente";
            }
        }

        private void cargarFormasPago()
        {
            cmbFormaPago.Items.Add("efectivo");
            cmbFormaPago.Items.Add("tarjeta");
            cmbFormaPago.Items.Add("transferencia");
            cmbFormaPago.SelectedIndex = 0;
        }

        private void generarSiguienteFactura()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = "SELECT ISNULL(MAX(CAST(numeroFactura AS INT)), 0) + 1 FROM tbVenta";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        txtNumeroFactura.Text = Convert.ToInt32(result).ToString("D4");
                    }
                    else
                    {
                        txtNumeroFactura.Text = "0001";
                    }
                }
            }
        }


        private void cargarProductosDGV()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = "SELECT idProducto, nombreProducto, precioVenta, stockActual FROM tbProducto WHERE stockActual > 0";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvProductos.DataSource = dt;
            }
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {

            try
            {
                (dgvProductos.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("nombre LIKE '%{0}%'", txtBuscarProducto.Text);
            }
            catch (Exception)
            {

            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                txtIdProducto.Text = fila.Cells["idProducto"].Value.ToString();
                txtNombreProducto.Text = fila.Cells["nombreProducto"].Value.ToString();
                txtPrecioVenta.Text = fila.Cells["precioVenta"].Value.ToString();
                txtStock.Text = fila.Cells["stockActual"].Value.ToString();

                numCantidad.Value = 1;
                numCantidad.Focus();
            }
        }

        private void configurarDetalleVentaDGV()
        {
            dtDetalleVenta = new DataTable();
            dtDetalleVenta.Columns.Add("idProducto", typeof(int));
            dtDetalleVenta.Columns.Add("Nombre", typeof(string));
            dtDetalleVenta.Columns.Add("Precio", typeof(decimal));
            dtDetalleVenta.Columns.Add("Cantidad", typeof(int));
            dtDetalleVenta.Columns.Add("Descuento", typeof(decimal)); 
            dtDetalleVenta.Columns.Add("Subtotal", typeof(decimal));

            dgvDetalleVenta.DataSource = dtDetalleVenta;

            // Opcional: Ocultar la columna de ID
            dgvDetalleVenta.Columns["idProducto"].Visible = false;
            // Opcional: Poner columnas como ReadOnly
            dgvDetalleVenta.Columns["Nombre"].ReadOnly = true;
            dgvDetalleVenta.Columns["Precio"].ReadOnly = true;
            dgvDetalleVenta.Columns["Subtotal"].ReadOnly = true;
        }
        private void LimpiarFormularioVenta()
        {
            cmbClientes.SelectedIndex = 0;
            cmbFormaPago.SelectedIndex = 0;

            dtDetalleVenta.Clear();

            txtDescuentoGeneral.Text = "0.00";
            txtTotalVenta.Text = "0.00";
            LimpiarSeccionProducto(); 

            generarSiguienteFactura();
            cargarProductosDGV(); 
        }
        private void LimpiarSeccionProducto()
        {
            txtIdProducto.Text = "";
            txtNombreProducto.Text = "";
            txtPrecioVenta.Text = "";
            txtStock.Text = "";
            numCantidad.Value = 1;
        }

        private void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedIndex <= 0) 
            {
                MessageBox.Show("Por favor, seleccione un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtDetalleVenta.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos en el carrito. Agregue al menos un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCliente = Convert.ToInt32(cmbClientes.SelectedValue);
            string numFactura = txtNumeroFactura.Text;
            string formaPago = cmbFormaPago.SelectedItem.ToString();
            DateTime fechaVenta = dtpFecha.Value; 
            decimal descGeneral = 0;
            Decimal.TryParse(txtDescuentoGeneral.Text, out descGeneral);
            decimal totalVenta = Convert.ToDecimal(txtTotalVenta.Text);

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlTransaction transaction = null;
                try
                {
                    transaction = con.BeginTransaction();
                    string sqlVenta = @"INSERT INTO tbVenta 
                                (idCliente, numeroFactura, fechaVenta, formaPago, descuentoGeneral, totalVenta)
                                VALUES 
                                (@IdCliente, @NumFactura, @Fecha, @FormaPago, @DescGeneral, @Total);
                                SELECT SCOPE_IDENTITY();";

                    int idVentaGenerada;
                    using (SqlCommand cmdVenta = new SqlCommand(sqlVenta, con, transaction))
                    {

                        cmdVenta.Parameters.AddWithValue("@IdCliente", idCliente);
                        cmdVenta.Parameters.AddWithValue("@NumFactura", numFactura);
                        cmdVenta.Parameters.AddWithValue("@Fecha", fechaVenta);
                        cmdVenta.Parameters.AddWithValue("@FormaPago", formaPago);
                        cmdVenta.Parameters.AddWithValue("@DescGeneral", descGeneral);
                        cmdVenta.Parameters.AddWithValue("@Total", totalVenta);

                        idVentaGenerada = Convert.ToInt32(cmdVenta.ExecuteScalar());
                    }

                    string sqlDetalle = @"INSERT INTO tbDetalleVenta 
                                  (idVenta, idProducto, cantidad, precioUnitario, descuento, subtotal)
                                  VALUES
                                  (@IdVenta, @IdProducto, @Cantidad, @Precio, @Descuento, @Subtotal)";


                    string sqlUpdateStock = @"UPDATE tbProducto SET 
                                      stockActual = stockActual - @Cantidad 
                                      WHERE idProducto = @IdProducto";

                    using (SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, con, transaction))
                    using (SqlCommand cmdStock = new SqlCommand(sqlUpdateStock, con, transaction))
                    {
                        foreach (DataRow fila in dtDetalleVenta.Rows)
                        {
                            cmdDetalle.Parameters.Clear();
                            cmdDetalle.Parameters.AddWithValue("@IdVenta", idVentaGenerada);
                            cmdDetalle.Parameters.AddWithValue("@IdProducto", Convert.ToInt32(fila["idProducto"]));
                            cmdDetalle.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(fila["Cantidad"]));
                            cmdDetalle.Parameters.AddWithValue("@Precio", Convert.ToDecimal(fila["Precio"]));
                            cmdDetalle.Parameters.AddWithValue("@Descuento", Convert.ToDecimal(fila["Descuento"]));
                            cmdDetalle.Parameters.AddWithValue("@Subtotal", Convert.ToDecimal(fila["Subtotal"]));
                            cmdDetalle.ExecuteNonQuery();

                            cmdStock.Parameters.Clear();
                            cmdStock.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(fila["Cantidad"]));
                            cmdStock.Parameters.AddWithValue("@IdProducto", Convert.ToInt32(fila["idProducto"]));
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show($"Venta {numFactura} registrada exitosamente.", "Venta Confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormularioVenta();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error al registrar la venta. No se guardó ningún dato: " + ex.Message, "Error en Transacción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception exRollback)
                    {
                        MessageBox.Show("Error crítico: Falló el registro Y el rollback. " + exRollback.Message);
                    }
                }
            }
        }
        private void actualizarTotales()
        {
            decimal totalVenta = 0;

            foreach (DataRow fila in dtDetalleVenta.Rows)
            {
                totalVenta += Convert.ToDecimal(fila["Subtotal"]);
            }

            decimal descuentoGeneral = 0;
            Decimal.TryParse(txtDescuentoGeneral.Text, out descuentoGeneral);

            totalVenta = totalVenta - descuentoGeneral;

            txtTotalVenta.Text = totalVenta.ToString("F2"); 
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdProducto.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProductoSel = Convert.ToInt32(txtIdProducto.Text);
            int cantidadPedida = (int)numCantidad.Value;
            int stockEnDB = Convert.ToInt32(txtStock.Text);
            decimal precioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
            string nombreProducto = txtNombreProducto.Text;

       
            int cantidadYaEnCarrito = 0;
            DataRow filaExistente = null;

            foreach (DataRow fila in dtDetalleVenta.Rows)
            {
                if (Convert.ToInt32(fila["idProducto"]) == idProductoSel)
                {
                    cantidadYaEnCarrito = Convert.ToInt32(fila["Cantidad"]);
                    filaExistente = fila;
                    break;
                }
            }

            int cantidadTotal = cantidadYaEnCarrito + cantidadPedida;

            if (cantidadTotal > stockEnDB)
            {
                MessageBox.Show($"Stock insuficiente. Solo quedan {stockEnDB} unidades de este producto.", "Stock Agotado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (filaExistente != null)
            {
                filaExistente["Cantidad"] = cantidadTotal;
                decimal descuento = Convert.ToDecimal(filaExistente["Descuento"]);
                filaExistente["Subtotal"] = (cantidadTotal * precioVenta) - descuento;
            }
            else
            {
                decimal subtotal = (cantidadPedida * precioVenta);
                dtDetalleVenta.Rows.Add(idProductoSel, nombreProducto, precioVenta, cantidadPedida, 0.00, subtotal);
            }

            actualizarTotales();
            LimpiarSeccionProducto();
            txtBuscarProducto.Focus();
        }

        private void txtDescuentoGeneral_TextChanged(object sender, EventArgs e)
        {
            actualizarTotales();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.ShowDialog();
        }
    }
}
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
    public partial class Compras : Form
    {
        public Compras()
        {
            InitializeComponent();
            CargarProductos();
            CargarProveedores();
        }

        private void CargarProveedores()
{
    using (SqlConnection con = new Conexion().AbrirConexion())
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT idProveedor, nombre FROM tbProveedor", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        cmbProveedor.DataSource = dt;
        cmbProveedor.DisplayMember = "nombre";
        cmbProveedor.ValueMember = "idProveedor";
    }
}


        private void CargarProductos()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT idProducto, nombreProducto, precioCosto, stockActual FROM tbProducto", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvProductos.DataSource = dt;

                // Agregamos columnas editables
                if (!dgvProductos.Columns.Contains("cantidad"))
                    dgvProductos.Columns.Add("cantidad", "Cantidad");

                if (!dgvProductos.Columns.Contains("descuento"))
                    dgvProductos.Columns.Add("descuento", "Descuento");

                if (!dgvProductos.Columns.Contains("subtotal"))
                    dgvProductos.Columns.Add("subtotal", "Subtotal");
            }
        }

        private void dgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Recalcular subtotal de la fila y total de la compra
            if (e.RowIndex >= 0)
            {
                var row = dgvProductos.Rows[e.RowIndex];
                decimal precio = Convert.ToDecimal(row.Cells["precioCosto"].Value);
                int cantidad = 0;
                decimal descuento = 0;

                if (row.Cells["cantidad"].Value != null)
                    cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);

                if (row.Cells["descuento"].Value != null)
                    descuento = Convert.ToDecimal(row.Cells["descuento"].Value);

                decimal subtotal = (precio * cantidad) - descuento;
                row.Cells["subtotal"].Value = subtotal;

                // Recalcular total
                decimal total = 0;
                foreach (DataGridViewRow r in dgvProductos.Rows)
                {
                    if (r.Cells["subtotal"].Value != null)
                        total += Convert.ToDecimal(r.Cells["subtotal"].Value);
                }
                txtTotal.Text = total.ToString("0.00");
            }
        }

       

        private void btnAgregarCompra_Click_1(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null || string.IsNullOrWhiteSpace(txtNumeroFactura.Text))
            {
                MessageBox.Show("Seleccione un proveedor y escriba el número de factura.");
                return;
            }

            int idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            string numeroFactura = txtNumeroFactura.Text;
            DateTime fecha = DateTime.Now; // <<--- Se asigna automáticamente la fecha actual

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    // Insertar compra
                    SqlCommand cmdCompra = new SqlCommand(
                        "INSERT INTO tbCompra (idProveedor, numeroFactura, fechaCompra, totalCompra) OUTPUT INSERTED.idCompra VALUES (@prov, @fact, @fecha, 0)",
                        con, transaction);
                    cmdCompra.Parameters.AddWithValue("@prov", idProveedor);
                    cmdCompra.Parameters.AddWithValue("@fact", numeroFactura);
                    cmdCompra.Parameters.AddWithValue("@fecha", fecha);
                    int idCompra = (int)cmdCompra.ExecuteScalar();

                    decimal totalCompra = 0;

                    // Recorrer DataGridView de productos
                    foreach (DataGridViewRow row in dgvProductos.Rows)
                    {
                        if (row.Cells["Cantidad"].Value == null) continue;

                        int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value);
                        int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                        decimal precioUnitario = Convert.ToDecimal(row.Cells["precioCosto"].Value);
                        decimal descuento = Convert.ToDecimal(row.Cells["Descuento"].Value);
                        decimal subtotal = (precioUnitario * cantidad) - ((precioUnitario * cantidad) * descuento / 100);
                        totalCompra += subtotal;

                        // Insertar detalle compra
                        SqlCommand cmdDetalle = new SqlCommand(
                            "INSERT INTO tbDetalleCompra (idCompra, idProducto, cantidad, precioUnitario, descuento, subtotal) VALUES (@idCompra, @idProd, @cant, @precio, @desc, @subt)",
                            con, transaction);
                        cmdDetalle.Parameters.AddWithValue("@idCompra", idCompra);
                        cmdDetalle.Parameters.AddWithValue("@idProd", idProducto);
                        cmdDetalle.Parameters.AddWithValue("@cant", cantidad);
                        cmdDetalle.Parameters.AddWithValue("@precio", precioUnitario);
                        cmdDetalle.Parameters.AddWithValue("@desc", descuento);
                        cmdDetalle.Parameters.AddWithValue("@subt", subtotal);
                        cmdDetalle.ExecuteNonQuery();

                        // Actualizar stock
                        SqlCommand cmdStock = new SqlCommand(
                            "UPDATE tbProducto SET stockActual = stockActual + @cant WHERE idProducto = @idProd",
                            con, transaction);
                        cmdStock.Parameters.AddWithValue("@cant", cantidad);
                        cmdStock.Parameters.AddWithValue("@idProd", idProducto);
                        cmdStock.ExecuteNonQuery();
                    }

                    // Actualizar totalCompra
                    SqlCommand cmdTotal = new SqlCommand(
                        "UPDATE tbCompra SET totalCompra = @total WHERE idCompra = @idCompra", con, transaction);
                    cmdTotal.Parameters.AddWithValue("@total", totalCompra);
                    cmdTotal.Parameters.AddWithValue("@idCompra", idCompra);
                    cmdTotal.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Compra registrada exitosamente.");
                }
                catch
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al registrar la compra.");
                }
            }
        }
    }
}
    


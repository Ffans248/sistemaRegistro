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
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
            cargarProductos();
        }
        private void limpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecioCosto.Clear();
            txtPrecioVenta.Clear();
            txtDescuento.Clear();
            txtStockActual.Clear();
            txtStockMinimo.Clear();
        }
        private void cargarProductos()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT idProducto, nombreProducto, descripcion, precioCosto, precioVenta, descuento, stockActual, stockMinimo FROM tbProducto", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvProductos.DataSource = dt;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioCosto.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {
                MessageBox.Show("Por favor completa los campos obligatorios: Nombre, Precio Costo y Precio Venta.");
                return;
            }

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                // Verificar si el producto ya existe por nombre
                string checkSql = @"SELECT 1 FROM tbProducto WHERE nombreProducto = @Nombre";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                {
                    checkCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    object existe = checkCmd.ExecuteScalar();
                    if (existe != null)
                    {
                        MessageBox.Show("El producto con ese nombre ya existe.");
                        return;
                    }
                }

                string insertSql = @"INSERT INTO tbProducto
                        (nombreProducto, descripcion, precioCosto, precioVenta, descuento, stockActual, stockMinimo)
                        VALUES (@Nombre, @Descripcion, @PrecioCosto, @PrecioVenta, @Descuento, @StockActual, @StockMinimo);
                        SELECT SCOPE_IDENTITY();";
                using (SqlCommand insertCmd = new SqlCommand(insertSql, con))
                {
                    insertCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    insertCmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                    insertCmd.Parameters.AddWithValue("@PrecioCosto", decimal.Parse(txtPrecioCosto.Text));
                    insertCmd.Parameters.AddWithValue("@PrecioVenta", decimal.Parse(txtPrecioVenta.Text));
                    insertCmd.Parameters.AddWithValue("@Descuento", decimal.Parse(txtDescuento.Text == "" ? "0" : txtDescuento.Text));
                    insertCmd.Parameters.AddWithValue("@StockActual", int.Parse(txtStockActual.Text == "" ? "0" : txtStockActual.Text));
                    insertCmd.Parameters.AddWithValue("@StockMinimo", int.Parse(txtStockMinimo.Text == "" ? "0" : txtStockMinimo.Text));

                    object idgenerado = insertCmd.ExecuteScalar();
                    int nuevoId = Convert.ToInt32(idgenerado);
                }

                MessageBox.Show("Producto registrado correctamente.");
            }

            cargarProductos();
            limpiarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecciona un producto de la lista para editar.");
                return;
            }

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = @"UPDATE tbProducto 
                                 SET nombreProducto = @Nombre, 
                                     descripcion = @Descripcion,
                                     precioCosto = @PrecioCosto, 
                                     precioVenta = @PrecioVenta, 
                                     descuento = @Descuento, 
                                     stockActual = @StockActual, 
                                     stockMinimo = @StockMinimo
                                 WHERE idProducto = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@PrecioCosto", decimal.Parse(txtPrecioCosto.Text));
                cmd.Parameters.AddWithValue("@PrecioVenta", decimal.Parse(txtPrecioVenta.Text));
                cmd.Parameters.AddWithValue("@Descuento", decimal.Parse(txtDescuento.Text == "" ? "0" : txtDescuento.Text));
                cmd.Parameters.AddWithValue("@StockActual", int.Parse(txtStockActual.Text == "" ? "0" : txtStockActual.Text));
                cmd.Parameters.AddWithValue("@StockMinimo", int.Parse(txtStockMinimo.Text == "" ? "0" : txtStockMinimo.Text));
                cmd.Parameters.AddWithValue("@Id", txtId.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Producto actualizado correctamente.");
            }
            cargarProductos();
            limpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
                return;
            }

            DialogResult result = MessageBox.Show("¿Seguro que deseas eliminar este producto?", "Confirmar eliminación",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection con = new Conexion().AbrirConexion())
                {
                    string query = "DELETE FROM tbProducto WHERE idProducto = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", txtId.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producto eliminado correctamente.");
                }
                cargarProductos();
                limpiarCampos();
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];
                txtId.Text = fila.Cells["idProducto"].Value.ToString();
                txtNombre.Text = fila.Cells["nombreProducto"].Value.ToString();
                txtDescripcion.Text = fila.Cells["descripcion"].Value.ToString();
                txtPrecioCosto.Text = fila.Cells["precioCosto"].Value.ToString();
                txtPrecioVenta.Text = fila.Cells["precioVenta"].Value.ToString();
                txtDescuento.Text = fila.Cells["descuento"].Value.ToString();
                txtStockActual.Text = fila.Cells["stockActual"].Value.ToString();
                txtStockMinimo.Text = fila.Cells["stockMinimo"].Value.ToString();
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.ShowDialog();
        }

        private void Productos_Load(object sender, EventArgs e)
        {

        }
    }
}

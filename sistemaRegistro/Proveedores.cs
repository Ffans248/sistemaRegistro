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
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
        }
        private void cargarProveedor()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT idProveedor, " +
                    "nombre, nit, direccion, telefono, correo FROM tbProveedor", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvProveedores.DataSource = dt;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {

                string checkSql = @"SELECT 1 FROM tbProveedor
                            WHERE nit = @Nit OR correo = @Correo OR telefono = @Telefono";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                {
                    checkCmd.Parameters.AddWithValue("@Nit", txtNit.Text);
                    checkCmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    checkCmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);

                    object existe = checkCmd.ExecuteScalar();
                    if (existe != null)
                    {
                        MessageBox.Show("El NIT de Proveedor, teléfono o correo ya existe.");
                        return;
                    }
                }

                string insertSql = @"INSERT INTO tbProveedor
                             (nombre, nit, direccion, telefono, correo)
                             VALUES (@Nombre, @Nit, @Direccion, @Telefono, @Correo);
                             SELECT SCOPE_IDENTITY();"; //OBTENIENDO EL ÚLTIMO ID
                using (SqlCommand insertCmd = new SqlCommand(insertSql, con))
                {
                    insertCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    insertCmd.Parameters.AddWithValue("@Nit", txtNit.Text);
                    insertCmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    insertCmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    insertCmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    object idgenerado = insertCmd.ExecuteScalar();
                    int nuevoId = Convert.ToInt32(idgenerado);


                }
            }
            cargarProveedor();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = "UPDATE tbProveedor SET nombre= @Nombre, nit = @Nit, direccion = @Direccion," +
                    " telefono = @Telefono, correo = @Correo WHERE idProveedor = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Nit", txtNit.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Proveedor actualizado correctamente");
            }
            cargarProveedor();
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];

                txtId.Text = fila.Cells["idProveedor"].Value.ToString();
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtCorreo.Text = fila.Cells["correo"].Value.ToString();
                txtDireccion.Text = fila.Cells["direccion"].Value.ToString();
                txtNit.Text = fila.Cells["nit"].Value.ToString();
                txtTelefono.Text = fila.Cells["telefono"].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = "DELETE FROM tbProveedor WHERE idProveedor = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Proveedor Eliminado Correctamente");
            }
            cargarProveedor();
        }
    }
}

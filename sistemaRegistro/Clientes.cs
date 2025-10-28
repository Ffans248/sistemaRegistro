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
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
            cargarClientes();
        }
        private void cargarClientes()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT idCliente, nombre, nit, direccion, telefono, correo FROM tbCliente", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvClientes.DataSource = dt;
            }
        }

        private void LimpiarCampos()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtNIT.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtNombre.Focus();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtNIT.Text))
            {
                MessageBox.Show("El Nombre y el NIT son obligatorios.");
                return;
            }

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string checkSql = "SELECT 1 FROM tbCliente WHERE nit = @NIT";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                {
                    checkCmd.Parameters.AddWithValue("@NIT", txtNIT.Text);
                    object existe = checkCmd.ExecuteScalar();
                    if (existe != null)
                    {
                        MessageBox.Show("El NIT ingresado ya existe en la base de datos.");
                        return;
                    }
                }

                // Si no existe, insertamos
                string insertSql = @"INSERT INTO tbCliente (nombre, nit, direccion, telefono, correo) 
                                     VALUES (@Nombre, @NIT, @Direccion, @Telefono, @Correo)";

                using (SqlCommand insertCmd = new SqlCommand(insertSql, con))
                {
                    insertCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    insertCmd.Parameters.AddWithValue("@NIT", txtNIT.Text);
                    insertCmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    insertCmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    insertCmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);

                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente guardado correctamente.");
                }
            }
            cargarClientes();
            LimpiarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccione un cliente para editar.");
                return;
            }

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string checkSql = "SELECT 1 FROM tbCliente WHERE nit = @NIT AND idCliente != @Id";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                {
                    checkCmd.Parameters.AddWithValue("@NIT", txtNIT.Text);
                    checkCmd.Parameters.AddWithValue("@Id", txtId.Text);
                    object existe = checkCmd.ExecuteScalar();
                    if (existe != null)
                    {
                        MessageBox.Show("El NIT ingresado ya pertenece a otro cliente.");
                        return;
                    }
                }


                string query = @"UPDATE tbCliente SET 
                                 nombre = @Nombre, nit = @NIT, direccion = @Direccion, 
                                 telefono = @Telefono, correo = @Correo 
                                 WHERE idCliente = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@NIT", txtNIT.Text);
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Id", txtId.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente actualizado correctamente.");
                }
            }
            cargarClientes();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Por favor, seleccione un cliente de la lista.");
                return;
            }

            if (MessageBox.Show("¿Está seguro de que desea eliminar este cliente?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                try
                {
                    string query = "DELETE FROM tbCliente WHERE idCliente = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", txtId.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cliente eliminado correctamente.");
                    }
                }
                catch (SqlException ex)
                {

                    if (ex.Number == 547)
                    {
                        MessageBox.Show("No se puede eliminar el cliente porque tiene ventas asociadas. " +
                                        "Considere desactivarlo (si tuviera un campo 'estado').");
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message);
                    }
                }
            }
            cargarClientes();
            LimpiarCampos();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];

                txtId.Text = fila.Cells["idCliente"].Value.ToString();
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtNIT.Text = fila.Cells["nit"].Value.ToString();
                txtDireccion.Text = fila.Cells["direccion"].Value.ToString();
                txtTelefono.Text = fila.Cells["telefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["correo"].Value.ToString();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dgvClientes.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("nombre LIKE '%{0}%' OR nit LIKE '%{0}%'", txtBuscar.Text);
            }
            catch (Exception)
            {

            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.ShowDialog();
        }
    }
}
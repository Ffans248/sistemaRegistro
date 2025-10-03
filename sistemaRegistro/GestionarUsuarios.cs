using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using static sistemaRegistro.Login;

namespace sistemaRegistro
{
    public partial class GestionarUsuarios : Form
    {
        public GestionarUsuarios()
        {
            InitializeComponent();
            cargarUsuario();
            cargarCombos();
        }
        private void cargarUsuario()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT idUsuario, " +
                    "nombreUsuario, correo, rol, estado FROM tbUsuario", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;
            }
        }

        private void cargarCombos()
        {
            cmbRol.Items.Add("Admin");
            cmbRol.Items.Add("User");

            var estados = new[]
            {
            new { Text = "Activo",   Value = 1 },
            new { Text = "Inactivo", Value = 0 }
            };

            cmbEstado.DataSource = estados;
            cmbEstado.DisplayMember = "Text";
            cmbEstado.ValueMember = "Value";
        }


        private void GestionarUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                
                string checkSql = @"SELECT 1 FROM tbUsuario
                            WHERE nombreUsuario = @Nombre OR correo = @Correo";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                {
                    checkCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    checkCmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);

                    object existe = checkCmd.ExecuteScalar();
                    if (existe != null)
                    {
                        MessageBox.Show("El nombre de usuario o correo ya existe.");
                        return;
                    }
                }

                string insertSql = @"INSERT INTO tbUsuario
                             (nombreUsuario, correo, pass, rol, estado)
                             VALUES (@Nombre, @Correo, @Pass, @Rol, @Estado)";
                using (SqlCommand insertCmd = new SqlCommand(insertSql, con))
                {
                    insertCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    insertCmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    insertCmd.Parameters.AddWithValue("@Pass",
                        BCrypt.Net.BCrypt.HashPassword(txtPass.Text));
                    insertCmd.Parameters.AddWithValue("@Rol",
                        cmbRol.SelectedItem.ToString());
                    insertCmd.Parameters.AddWithValue("@Estado",
                        cmbEstado.SelectedValue);

                    insertCmd.ExecuteNonQuery();
                }
            }

            cargarUsuario();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = "UPDATE tbUsuario SET nombreUsuario = @Nombre, correo = @Correo, pass = @Pass," +
                    " rol = @Rol, estado = @Estado WHERE idUsuario = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Pass", txtPass.Text);
                cmd.Parameters.AddWithValue("@Rol",
                cmbRol.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Estado",
                cmbEstado.SelectedValue);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario actualizado correctamente");
            }
            cargarUsuario();
        }
       
        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                txtId.Text = fila.Cells["idUsuario"].Value.ToString();
                txtNombre.Text = fila.Cells["nombreUsuario"].Value.ToString();
                txtCorreo.Text = fila.Cells["correo"].Value.ToString();
                cmbRol.SelectedItem = fila.Cells["rol"].Value.ToString();
                cmbEstado.SelectedValue = Convert.ToInt32(fila.Cells["estado"].Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = "DELETE FROM tbUsuario WHERE idUsuario = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario Eliminado Correctamente");
            }
            cargarUsuario();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using FontAwesome.Sharp;
using static sistemaRegistro.Login;

namespace sistemaRegistro
{
    public partial class GestionarUsuarios : Form
    {
        int idActual = Session.UsuarioID;
        public GestionarUsuarios()
        {
            InitializeComponent();
            cargarUsuario();
            cargarCombos();
            permisos();
        }
        private void verificarPermisos()
        {

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
        private void crearPermisos()
        {
            if (cmbRol.SelectedIndex == 0)
            {


            }
        }
        private void crearAccesosForms()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {

                

                string insertSql = @"INSERT INTO tbFormulario
                             (nombreUsuario, descripcion, p, rol, estado)
                             VALUES (@Nombre, @Correo, @Pass, @Rol, @Estado);
                             SELECT SCOPE_IDENTITY();"; //OBTENIENDO EL ÚLTIMO ID
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
                    object idgenerado = insertCmd.ExecuteScalar();
                    int nuevoId = Convert.ToInt32(idgenerado);

                }
            }
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
                             VALUES (@Nombre, @Correo, @Pass, @Rol, @Estado);
                             SELECT SCOPE_IDENTITY();"; //OBTENIENDO EL ÚLTIMO ID
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
                    object idgenerado = insertCmd.ExecuteScalar();
                    int nuevoId = Convert.ToInt32(idgenerado);

                    if (cmbRol.SelectedIndex == 0)
                    {
                        accesoFormularioAdmin(nuevoId);

                    }
                    else if (cmbRol.SelectedIndex == 1){
                        accesoFormularioUsuario(nuevoId);
                    }

                    

                }
            }

            cargarUsuario();
        }
        private void accesoFormularioAdmin(int id)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {

                string insertSql = @"INSERT INTO tbAccesoFormulario (idUsuario, idFormulario)
                             VALUES (@idUsuario, 1), (@idUsuario, 2);";
                using (SqlCommand insertCmd = new SqlCommand(insertSql, con))
                {
                    insertCmd.Parameters.AddWithValue("@idUsuario", id);

                    insertCmd.ExecuteNonQuery();

                }
                //Dar permisos de formularios
                string insertSql2 = @"INSERT INTO tbPermisoFormulario (idUsuario, idFormulario, lectura, escritura, eliminacion)
                             VALUES (@idUsuario, 1, 1, 1, 1), (@idUsuario, 2, 1, 1, 1);";
                using (SqlCommand insertCmd2 = new SqlCommand(insertSql2, con))
                {
                    insertCmd2.Parameters.AddWithValue("@idUsuario", id);

                    insertCmd2.ExecuteNonQuery();

                }
            }
        }
        private void accesoFormularioUsuario(int id)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {

                string insertSql = @"INSERT INTO tbAccesoFormulario (idUsuario, idFormulario)
                             VALUES (@idUsuario, 1), (@idUsuario, 2);";
                using (SqlCommand insertCmd = new SqlCommand(insertSql, con))
                {
                    insertCmd.Parameters.AddWithValue("@idUsuario", id);

                    insertCmd.ExecuteNonQuery();

                }
                //Dar permisos de formularios
                string insertSql2 = @"INSERT INTO tbPermisoFormulario (idUsuario, idFormulario, lectura, escritura, eliminacion)
                             VALUES (@idUsuario, 1, 0, 0, 0), (@idUsuario, 2, 0, 0, 0);";
                using (SqlCommand insertCmd2 = new SqlCommand(insertSql2, con))
                {
                    insertCmd2.Parameters.AddWithValue("@idUsuario", id);

                    insertCmd2.ExecuteNonQuery();

                }
            }
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

        private void permisos()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                // Lectura
                SqlCommand cmd = new SqlCommand("SELECT lectura FROM tbPermisoFormulario WHERE idUsuario = @idUsuario", con);
                cmd.Parameters.AddWithValue("@idUsuario", idActual);
                object resultLectura = cmd.ExecuteScalar();

                // Escritura
                SqlCommand cmd2 = new SqlCommand("SELECT escritura FROM tbPermisoFormulario WHERE idUsuario = @idUsuario", con);
                cmd2.Parameters.AddWithValue("@idUsuario", idActual);
                object resultEscritura = cmd2.ExecuteScalar();

                // Eliminación
                SqlCommand cmd3 = new SqlCommand("SELECT eliminacion FROM tbPermisoFormulario WHERE idUsuario = @idUsuario", con);
                cmd3.Parameters.AddWithValue("@idUsuario", idActual);
                object resultEliminacion = cmd3.ExecuteScalar();

                // Bloquear si no tiene escritura
                if (resultEscritura != null && Convert.ToInt32(resultEscritura) == 0)
                {
                    textBox();
                }

                // Bloquear si no tiene eliminación
                if (resultEliminacion != null && Convert.ToInt32(resultEliminacion) == 0)
                {
                    permisoEliminar();
                }
            }
        }
        private void permisoEliminar()
        {
            btnEliminar.Enabled = false;
        }
        private void textBox()
        {
            txtId.ReadOnly= true;
            txtNombre.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            txtPass.ReadOnly = true;
            cmbRol.Enabled = false;
            cmbEstado.Enabled = false;
            btnGuardar.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.ShowDialog();
        }
    }
}

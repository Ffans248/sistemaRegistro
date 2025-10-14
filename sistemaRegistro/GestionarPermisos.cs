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
    public partial class GestionarPermisos : Form
    {
        public GestionarPermisos()
        {
            InitializeComponent();
            CargarUsuarios();
            rolesCmb();
        }

        private void CargarUsuarios()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbUsuario", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPermisos.DataSource = dt;
            }
        }

        private void dgvPermisos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            chbEditar.Checked = false;
           // chbLeer.Checked = false;
            chbEliminar.Checked = false;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvPermisos.Rows[e.RowIndex];
                txtId.Text = fila.Cells["idUsuario"].Value.ToString();
                txtNombre.Text = fila.Cells["nombreUsuario"].Value.ToString();
                txtCorreo.Text = fila.Cells["correo"].Value.ToString();
                if (fila.Cells["rol"].Value.ToString() == "Admin")
                {
                    cmbRol.SelectedItem = "Admin"; //Seleccionando Admin
                }
                else if (fila.Cells["rol"].Value.ToString() == "User")
                {
                    cmbRol.SelectedItem = "User"; // Seleccionando User


                }
                using (SqlConnection con = new Conexion().AbrirConexion())
                {
                    //obtener id
                    int id = int.Parse(txtId.Text);
                    //obteniendo el valor de lectura
                    SqlCommand cmd = new SqlCommand("SELECT lectura FROM tbPermisoFormulario WHERE idUsuario = @idUsuario", con);
                    cmd.Parameters.AddWithValue("@idUsuario", id);
                    object resultLectura = cmd.ExecuteScalar();
                    //obteniendo el valor de Escritura
                    SqlCommand cmd2 = new SqlCommand("SELECT escritura FROM tbPermisoFormulario WHERE idUsuario = @idUsuario", con);
                    cmd2.Parameters.AddWithValue("@idUsuario", id);
                    object resultEscritura = cmd2.ExecuteScalar();
                    //obteniendo el valor de eliminacion
                    SqlCommand cmd3 = new SqlCommand("SELECT eliminacion FROM tbPermisoFormulario WHERE idUsuario = @idUsuario", con);
                    cmd3.Parameters.AddWithValue("@idUsuario", id);
                    object resultEliminacion = cmd3.ExecuteScalar();

                    if (resultLectura != null && resultLectura != DBNull.Value)
                    {
                        bool lectura = Convert.ToBoolean(resultLectura);
                        //marcando el checkbox
                      //  chbLeer.Checked = lectura;
                        
                    }
                    if (resultEscritura != null && resultEscritura != DBNull.Value)
                    {
                        bool escritura = Convert.ToBoolean(resultEscritura);
                        //marcando el checkbox
                        
                        chbEditar.Checked = escritura;
                        

                    }
                    if (resultEliminacion != null && resultEliminacion != DBNull.Value)
                    {

                        bool eliminacion = Convert.ToBoolean(resultEliminacion);
                        //marcando el checkbox
                        
                        chbEliminar.Checked = eliminacion;

                    }
                    }
            }
        }
        private void rolesCmb()
        {
            cmbRol.Items.Add("Admin");
            cmbRol.Items.Add("User");

        }

        private void GestionarPermisos_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idUsuario = Convert.ToInt32(txtId.Text);

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                // valores por defecto false
               // bool leer = chbLeer.Checked;
                bool editar = chbEditar.Checked;
                bool eliminar = chbEliminar.Checked;

                string query = @"UPDATE tbPermisoFormulario 
                         SET 
                             escritura = @editar, 
                             eliminacion = @eliminar 
                         WHERE idUsuario = @idUsuario 
                         AND idFormulario = 1;";

                SqlCommand cmd = new SqlCommand(query, con);

                // Agregar SIEMPRE los parámetros
              //  cmd.Parameters.AddWithValue("@leer", leer);
                cmd.Parameters.AddWithValue("@editar", editar);
                cmd.Parameters.AddWithValue("@eliminar", eliminar);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario actualizado correctamente");
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

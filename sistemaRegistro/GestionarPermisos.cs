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
                        chbLeer.Checked = lectura;
                        chbEditar.Checked = false;
                        chbEliminar.Checked = false;
                    }
                    else if (resultEscritura != null && resultEscritura != DBNull.Value)
                    {
                        bool escritura = Convert.ToBoolean(resultEscritura);
                        //marcando el checkbox
                        chbLeer.Checked = false;
                        chbEditar.Checked = escritura;
                        chbEliminar.Checked = false;

                    }
                    else if (resultEliminacion != null && resultEliminacion != DBNull.Value)
                    {

                        bool eliminacion = Convert.ToBoolean(resultEliminacion);
                        //marcando el checkbox
                        chbLeer.Checked = false;
                        chbEditar.Checked = false;
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
    }
}

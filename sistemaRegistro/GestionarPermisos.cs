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
                    SqlDataAdapter da = new SqlDataAdapter("SELECT lectura FROM tbPermisoFormulario where idUsuario=@idUsuario", con);
                    .Parameters.addWithValue("@idUsuario")
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

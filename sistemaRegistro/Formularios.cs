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
    public partial class Formularios : Form
    {
        public Formularios()
        {
            InitializeComponent();
            cargarUsuario();

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
        private void cargarForms()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                int id = Convert.ToInt32(txtId.Text);

                string query = @"SELECT 
                            tbFormulario.idFormulario, 
                            tbFormulario.nombreFormulario, 
                            tbFormulario.descripcion, 
                            tbFormulario.idUsuario, 
                            tbUsuario.nombreUsuario, 
                            tbFormulario.permiso
                         FROM tbFormulario
                         INNER JOIN tbUsuario 
                         ON tbFormulario.idUsuario = tbUsuario.idUsuario
                         WHERE tbFormulario.idUsuario = @idUsuario";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@idUsuario", id);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFormularios.DataSource = dt;
            }
        }


        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                txtId.Text = fila.Cells["idUsuario"].Value.ToString();
                txtNombre.Text = fila.Cells["nombreUsuario"].Value.ToString();
                txtCorreo.Text = fila.Cells["correo"].Value.ToString();

            }
            cargarForms();
        }
        private int idFormularioSeleccionado = -1;
        private void dgvFormularios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvFormularios.Rows[e.RowIndex];
                idFormularioSeleccionado = Convert.ToInt32(fila.Cells["idFormulario"].Value);
            }
        }

        private void btnPermitir_Click(object sender, EventArgs e)
        {
            if (idFormularioSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un formulario primero.");
                return;
            }

            CambiarPermisoFormulario(idFormularioSeleccionado, true);
        }

        private void btnDenegar_Click(object sender, EventArgs e)
        {
            if (idFormularioSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un formulario primero.");
                return;
            }

            CambiarPermisoFormulario(idFormularioSeleccionado, false);
        }
        private void CambiarPermisoFormulario(int idFormulario, bool permiso)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string updateSql = "UPDATE tbFormulario SET permiso = @permiso WHERE idFormulario = @idFormulario";
                using (SqlCommand cmd = new SqlCommand(updateSql, con))
                {
                    cmd.Parameters.AddWithValue("@permiso", permiso);
                    cmd.Parameters.AddWithValue("@idFormulario", idFormulario);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show($"Permiso {(permiso ? "activado" : "denegado")} correctamente.");
            cargarForms(); // refrescar la tabla
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.ShowDialog();
        }
    }
}

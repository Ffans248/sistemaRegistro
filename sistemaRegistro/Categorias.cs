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
    public partial class Categorias : Form
    {
        public Categorias()
        {
            InitializeComponent();
            cargarCategoria();
        }
        private void cargarCategoria()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT idCategoria, nombreCategoria, descripcion FROM tbCategoria", con);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDescripcion.DataSource = dt;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {

                string checkSql = @"SELECT 1 FROM tbCategoria
                            WHERE nombreCategoria = @Nombre";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                {
                    checkCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);

                    object existe = checkCmd.ExecuteScalar();
                    if (existe != null)
                    {
                        MessageBox.Show("Nombre de categoria repetida.");
                        return;
                    }
                }

                string insertSql = @"INSERT INTO tbCategoria
                             (nombreCategoria, descripcion)
                             VALUES (@Nombre, @Descripcion);
                             SELECT SCOPE_IDENTITY();"; //OBTENIENDO EL ÚLTIMO ID
                using (SqlCommand insertCmd = new SqlCommand(insertSql, con))
                {
                    insertCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    insertCmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                    object idgenerado = insertCmd.ExecuteScalar();
                    int nuevoId = Convert.ToInt32(idgenerado);


                }
            }
            cargarCategoria();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = "UPDATE tbCategoria SET nombreCategoria = @Nombre, descripcion = @Descripcion" +
                    " WHERE idCategoria = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Categoria actualizada correctamente");
            }
            cargarCategoria();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = "DELETE FROM tbCategoria WHERE idCategoria = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Categoria Eliminado Correctamente");
            }
            cargarCategoria();
        }

        private void dgvDescripcion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvDescripcion.Rows[e.RowIndex];

                txtId.Text = fila.Cells["idCategoria"].Value.ToString();
                txtNombre.Text = fila.Cells["nombreCategoria"].Value.ToString();
                txtDescripcion.Text = fila.Cells["descripcion"].Value.ToString();
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.ShowDialog();
        }

        private void Categorias_Load(object sender, EventArgs e)
        {

        }
    }
}

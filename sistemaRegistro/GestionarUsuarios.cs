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
using BCrypt.Net;
using static sistemaRegistro.Login;

namespace sistemaRegistro
{
    public partial class GestionarUsuarios : Form
    {
        public GestionarUsuarios()
        {
            InitializeComponent();
        }
        private void cargarUsuario()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbUsuario", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;
            }
        }


        private void GestionarUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbUsuario (Nombre) VALUES (@Nombre, @Correo, @Pass, @Rol, @Estado)", con);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Pass", txtPass.Text);
                cmd.Parameters.AddWithValue("@Rol", cmbRol.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Estado", cmbEstado.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
            }
            cargarUsuario();
        }
    }
}

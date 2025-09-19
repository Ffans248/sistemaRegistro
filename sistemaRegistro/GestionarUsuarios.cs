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
            cargarUsuario();
            cargarCombos();
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
                SqlCommand cmd = new SqlCommand("INSERT INTO tbUsuario (nombreUsuario, correo, pass, rol, estado) VALUES (@Nombre, @Correo, @Pass, @Rol, @Estado)", con);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Pass", BCrypt.Net.BCrypt.HashPassword(txtPass.Text));
                cmd.Parameters.AddWithValue("@Rol", cmbRol.SelectedItem.ToString());
                var estado = ((dynamic)cmbEstado.SelectedItem).Value;
                cmd.Parameters.AddWithValue("@Estado", cmbEstado.SelectedValue);

                cmd.ExecuteNonQuery();
            }
            cargarUsuario();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

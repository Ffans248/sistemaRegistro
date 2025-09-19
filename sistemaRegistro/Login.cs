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
using System.Data.SqlClient;
using System.Configuration;

namespace sistemaRegistro
{
    public partial class Login : Form
    {
        
    public class Conexion
    {
        private SqlConnection con;
        public SqlConnection AbrirConexion()
        {
            con = new
            SqlConnection(ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            return con;
        }
        public void CerrarConexion()
        {
            if (con != null && con.State == System.Data.ConnectionState.Open)
                con.Close();
        }
}

        public Login()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {

        }
        private void encriptarPass(){

            }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnAcceder_Click_1(object sender, EventArgs e)
        {
            
            {
                string user = txtUsuario.Text;
                string pass = txtPass.Text;

                if (AutenticarUsuario(user, pass))
                {
                    MessageBox.Show("Inicio de sesión correcto");
                    // Abrir formulario principal, etc.
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }

        }

        private void btnSaltar_Click(object sender, EventArgs e)
        {
            GestionarUsuarios gestionarUsuarios = new GestionarUsuarios();
            gestionarUsuarios.ShowDialog();
        }
       
        private bool AutenticarUsuario(string nombreUsuario, string passwordIngresada)
        {
            string hashBD = null;

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string sql = "SELECT pass FROM tbUsuario WHERE nombreUsuario = @Nombre";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        hashBD = result.ToString();
                    }
                    else
                    {
                        // Usuario no encontrado
                        return false;
                    }
                }
            }

            // Verificar la contraseña ingresada contra el hash guardado
            bool esCorrecta = BCrypt.Net.BCrypt.Verify(passwordIngresada, hashBD);
            return esCorrecta;
        }
    }
}

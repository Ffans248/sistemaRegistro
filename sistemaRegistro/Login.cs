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
using System.Configuration;
using System.Collections;

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
                    Menu menu = new Menu();
                    this.Hide();
                    menu.ShowDialog();
                    
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
                    string queryId = "SELECT idUsuario, nombreUsuario, correo, rol, estado FROM tbUsuario WHERE nombreUsuario = @Nombre";
                    using (SqlCommand cmd2 = new SqlCommand(queryId, con))
                    {
                        cmd2.Parameters.AddWithValue("@Nombre", nombreUsuario);
                        using (SqlDataReader reader = cmd2.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Guardamos los datos de la sesion
                                Session.UsuarioID = reader.GetInt32(0); 
                                Session.NombreUsuario = reader.GetString(1);
                                Session.Correo = reader.GetString(2);
                                Session.Rol = reader.GetString(3);
                                Session.Estado = reader.GetBoolean(4);
                            }


                        }
                    }
                }

                // Verificar la contraseña ingresada contra el hash guardado
                bool esCorrecta = BCrypt.Net.BCrypt.Verify(passwordIngresada, hashBD);
                return esCorrecta;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            crearUsuario crearUsuario = new crearUsuario();
            crearUsuario.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recuperar recuperar = new recuperar();
            recuperar.ShowDialog();
        }
    }
}

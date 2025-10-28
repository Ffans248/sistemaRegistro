using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistemaRegistro
{
    public partial class CambiarPass : Form
    {
        public CambiarPass()
        {
            InitializeComponent();
        }

        int idActual = Session.UsuarioID;
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.ShowDialog();
        }

        private void CambiarPass_Load(object sender, EventArgs e)
        {

        }

        private void txtContraActual_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            string oldPass = txtContraActual.Text; // Contraseña actual ingresada
            string newPass = txtContraNueva.Text;    // Nueva contraseña

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Por favor, ingrese todos los campos.");
                return;
            }

            // Verificar la contraseña actual
            using (SqlConnection con = new Login.Conexion().AbrirConexion())
            {
                string sql = "SELECT pass FROM tbUsuario WHERE idUsuario = @id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", Session.UsuarioID);
                    object result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("Usuario no encontrado.");
                        return;
                    }

                    string hashBD = result.ToString();

                    // Verificamos si la contraseña actual coincide
                    if (!BCrypt.Net.BCrypt.Verify(oldPass, hashBD))
                    {
                        MessageBox.Show("La contraseña actual es incorrecta.");
                        return;
                    }

                    // Encriptar la nueva contraseña
                    string newHash = BCrypt.Net.BCrypt.HashPassword(newPass);

                    // Actualizar la contraseña en la base de datos
                    string updateSql = "UPDATE tbUsuario SET pass = @pass WHERE idUsuario = @id";
                    using (SqlCommand updateCmd = new SqlCommand(updateSql, con))
                    {
                        updateCmd.Parameters.AddWithValue("@pass", newHash);
                        updateCmd.Parameters.AddWithValue("@id", Session.UsuarioID);
                        int rows = updateCmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Contraseña cambiada correctamente. La aplicación se cerrará.");
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar la contraseña.");
                        }
                    }
                }
            }

        }
    }
}

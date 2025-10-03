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
    public partial class recuperar : Form
    {
        public recuperar()
        {
            InitializeComponent();
        }

        private int usuarioIdRecuperar = -1; // guardamos el idUsuario temporalmente

        private void recuperar_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = @"SELECT u.idUsuario, 
                                p1.pregunta AS Pregunta1,
                                p2.pregunta AS Pregunta2,
                                ps.respuesta1,
                                ps.respuesta2
                         FROM tbUsuario u
                         INNER JOIN tbPreguntaSeguridad ps ON u.idUsuario = ps.idUsuario
                         INNER JOIN tbCatPreguntas p1 ON ps.idCatPregunta1 = p1.idCatPregunta
                         INNER JOIN tbCatPreguntas p2 ON ps.idCatPregunta2 = p2.idCatPregunta
                         WHERE u.nombreUsuario = @User OR u.correo = @Correo";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@User", txtUsuario.Text.Trim());
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuarioIdRecuperar = (int)reader["idUsuario"];
                            lblPregunta1.Text = reader["Pregunta1"].ToString();
                            lblPregunta2.Text = reader["Pregunta2"].ToString();

                            // Guardamos las respuestas reales en el Tag para validarlas
                            lblPregunta1.Tag = reader["respuesta1"].ToString();
                            lblPregunta2.Tag = reader["respuesta2"].ToString();

                            // Habilitar campos de respuestas
                            txtRespuesta1.Enabled = true;
                            txtRespuesta2.Enabled = true;
                            btnValidarRespuestas.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("No se encontró ningún usuario con esos datos.");
                        }
                    }
                }
            }
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            if (usuarioIdRecuperar == -1) return;

            string resp1BD = lblPregunta1.Tag.ToString();
            string resp2BD = lblPregunta2.Tag.ToString();

            if (resp1BD.Equals(txtRespuesta1.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
                resp2BD.Equals(txtRespuesta2.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Respuestas correctas. Ahora ingresa tu nueva contraseña.");
                txtNewPass.Enabled = true;
                btnCambiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Las respuestas no coinciden. Intenta de nuevo.");
            }
        }

        private void btnNewPass_Click(object sender, EventArgs e)
        {
            string nuevaPass = txtNewPass.Text.Trim();

            // Validar nueva contraseña
            if (!System.Text.RegularExpressions.Regex.IsMatch(nuevaPass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$"))
            {
                MessageBox.Show("La contraseña debe contener al menos una mayúscula, una minúscula y un número.");
                return;
            }

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string updateSql = "UPDATE tbUsuario SET pass = @Pass WHERE idUsuario = @IdUsuario";
                using (SqlCommand cmd = new SqlCommand(updateSql, con))
                {
                    cmd.Parameters.AddWithValue("@Pass", BCrypt.Net.BCrypt.HashPassword(nuevaPass));
                    cmd.Parameters.AddWithValue("@IdUsuario", usuarioIdRecuperar);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contraseña cambiada correctamente.");

                    // Opcional: limpiar campos
                    txtUsuario.Clear();
                    txtCorreo.Clear();
                    txtRespuesta1.Clear();
                    txtRespuesta2.Clear();
                    txtNewPass.Clear();
                    lblPregunta1.Text = "";
                    lblPregunta2.Text = "";
                    txtNewPass.Enabled = false;
                    btnCambiar.Enabled = false;
                }
            }
        }
    }
}

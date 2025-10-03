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
    public partial class crearUsuario : Form
    {
        public crearUsuario()
        {
            InitializeComponent();
            preguntasDAO.CargarPreguntas(cmbPregunta1);
            preguntasDAO.CargarPreguntas(cmbPregunta2);
        }
        public class PreguntasDAO
        {
            private Conexion conexion = new Conexion();

            public void CargarPreguntas(ComboBox combo)
            {
                string query = "SELECT idCatPregunta, pregunta FROM tbCatPreguntas";

                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    combo.DataSource = dt;
                    combo.DisplayMember = "pregunta";    // Lo que verá el usuario
                    combo.ValueMember = "idCatPregunta"; // Lo que se usará en BD
                }

                conexion.CerrarConexion();
            }
        }

        private PreguntasDAO preguntasDAO = new PreguntasDAO();
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validar que no se repitan las preguntas
            if ((int)cmbPregunta1.SelectedValue == (int)cmbPregunta2.SelectedValue)
            {
                MessageBox.Show("No puedes seleccionar la misma pregunta dos veces.");
                return;
            }

            // 2. Validar correo con Regex
            string correo = txtCorreo.Text.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("El correo no tiene un formato válido.");
                return;
            }

            // 3. Validar contraseña
            string pass = txtPass.Text;
            if (!System.Text.RegularExpressions.Regex.IsMatch(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$"))
            {
                MessageBox.Show("La contraseña debe contener al menos una mayúscula, una minúscula y un número.");
                return;
            }

            using (SqlConnection con = new Conexion().AbrirConexion())
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    // 4. Verificar que no exista usuario o correo
                    string checkSql = @"SELECT 1 FROM tbUsuario
                                WHERE nombreUsuario = @Nombre OR correo = @Correo";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, con, trans))
                    {
                        checkCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                        checkCmd.Parameters.AddWithValue("@Correo", correo);

                        object existe = checkCmd.ExecuteScalar();
                        if (existe != null)
                        {
                            MessageBox.Show("El nombre de usuario o correo ya existe.");
                            trans.Rollback();
                            return;
                        }
                    }

                    // 5. Insertar usuario
                    string insertUsuario = @"INSERT INTO tbUsuario (nombreUsuario, correo, pass)
                                     OUTPUT INSERTED.idUsuario
                                     VALUES (@Nombre, @Correo, @Pass)";
                    int idUsuario;
                    using (SqlCommand insertCmd = new SqlCommand(insertUsuario, con, trans))
                    {
                        insertCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                        insertCmd.Parameters.AddWithValue("@Correo", correo);
                        insertCmd.Parameters.AddWithValue("@Pass", BCrypt.Net.BCrypt.HashPassword(pass));

                        idUsuario = (int)insertCmd.ExecuteScalar();
                    }

                    // 6. Insertar preguntas de seguridad
                    string insertPreguntas = @"INSERT INTO tbPreguntaSeguridad 
                        (idUsuario, idCatPregunta1, respuesta1, idCatPregunta2, respuesta2)
                        VALUES (@IdUsuario, @Pregunta1, @Respuesta1, @Pregunta2, @Respuesta2)";
                    using (SqlCommand pregCmd = new SqlCommand(insertPreguntas, con, trans))
                    {
                        pregCmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        pregCmd.Parameters.AddWithValue("@Pregunta1", (int)cmbPregunta1.SelectedValue);
                        pregCmd.Parameters.AddWithValue("@Respuesta1", txtRespuesta1.Text.Trim());
                        pregCmd.Parameters.AddWithValue("@Pregunta2", (int)cmbPregunta2.SelectedValue);
                        pregCmd.Parameters.AddWithValue("@Respuesta2", txtRespuesta2.Text.Trim());

                        pregCmd.ExecuteNonQuery();
                    }

                    // Confirmamos la transacción
                    trans.Commit();
                    MessageBox.Show("Cuenta creada correctamente");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error al crear el usuario: " + ex.Message);
                }
            }
        }

        private void crearUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}

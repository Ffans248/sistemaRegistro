using FontAwesome.Sharp;
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
    public partial class Menu : Form
    {
        int idActual = Session.UsuarioID;
        public Menu()
        {
            InitializeComponent();
        }
        private void tetxbox()
        {
        
        } 
       
        private void btnGestionarUsuarios_Click(object sender, EventArgs e)
        {
            GestionarUsuarios frmgestionarUsuarios = new GestionarUsuarios();
            this.Hide();
            frmgestionarUsuarios.ShowDialog();
        }

        private void btnGestionarPermisos_Click(object sender, EventArgs e)
        {
            GestionarPermisos frmgestionarPermisos = new GestionarPermisos();
            this.Hide();
            frmgestionarPermisos.ShowDialog();
        }

        private void btnchangePass_Click(object sender, EventArgs e)
        {
            CambiarPass cambiarPass = new CambiarPass();
            this.Hide();
            cambiarPass.ShowDialog();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "Bienvenido: " + Session.NombreUsuario;
            validarPermisos();
        }
        private void validarPermisos()
        {
            int idUsuario = Session.UsuarioID;
            Boolean Estado = Session.Estado;
            if (Estado == false)
            {
                btnchangePass.Enabled = false;
                btnGestionarPermisos.Enabled = false;
                btnGestionarUsuarios.Enabled = false;


            }
            using (SqlConnection con = new Conexion().AbrirConexion())
            {

                string checkSql = @"SELECT 1 FROM tbAccesoFormulario
                            WHERE idUsuario = @id AND idFormulario = 1";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                {
                    checkCmd.Parameters.AddWithValue("@id", idUsuario);


                    object existe = checkCmd.ExecuteScalar();
                    if (existe == null)
                    {
                        btnGestionarPermisos.Enabled = false;
                    }
                    string checkSql2 = @"SELECT 1 FROM tbAccesoFormulario
                            WHERE idUsuario = @id2 AND idFormulario = 2";
                    using (SqlCommand checkCmd2 = new SqlCommand(checkSql2, con))
                    {
                        checkCmd.Parameters.AddWithValue("@id2", idUsuario);


                        object existe2 = checkCmd.ExecuteScalar();
                        if (existe2 == null)
                        {
                            btnGestionarUsuarios.Enabled = false;
                        }
                    }
                }
            }
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            Proveedores proveedores = new Proveedores();
            this.Hide();
            proveedores.ShowDialog();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            Categorias categorias = new Categorias();
            this.Hide();
            categorias.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            Productos productos = new Productos();
            this.Hide();
            productos.ShowDialog();
        }

        private void btnAsignaciones_Click(object sender, EventArgs e)
        {
            productosCategorias asignaciones = new productosCategorias();
            this.Hide();
            asignaciones.ShowDialog();
        }
    }
}

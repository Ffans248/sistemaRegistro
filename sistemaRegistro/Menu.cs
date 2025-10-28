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
            VerificarTodosLosPermisos(idActual);

        }
        private void VerificarTodosLosPermisos(int idUsuario)
        {
            // Lista con los nombres de formulario y los botones asociados
            var botones = new List<(string Formulario, Button Boton)>
    {
        ("Gestionar Permisos", btnGestionarPermisos),
        ("Gestionar Usuarios", btnGestionarUsuarios),
        ("Formularios", btnFormularios),
        ("Categorías", btnCategorias),
        ("Productos", btnProductos),
        ("Proveedores", btnProveedores),
        ("Productos Categorías", btnAsignaciones),
        ("Compras", btnCompras),
        ("Reportes", btnReportes)
    };

            // Recorre la lista y verifica el permiso de cada uno
            foreach (var item in botones)
            {
                VerificarPermisoBoton(idUsuario, item.Formulario, item.Boton);
            }
        }
        private void VerificarPermisoBoton(int idUsuario, string nombreFormulario, Button boton)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = @"SELECT permiso
                         FROM tbFormulario 
                         WHERE idUsuario = @idUsuario AND nombreFormulario = @nombreFormulario";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@nombreFormulario", nombreFormulario);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        bool tienePermiso = Convert.ToBoolean(resultado);
                        boton.Enabled = tienePermiso;
                    }
                    else
                    {
                        boton.Enabled = false; // Si no hay registro, por seguridad se desactiva
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

        private void btnFormularios_Click(object sender, EventArgs e)
        {
            Formularios forms = new Formularios();
            this.Hide();
            forms.ShowDialog();

        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compras compras = new Compras();
            this.Hide();
            compras.ShowDialog();

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
             Reportes reportes = new Reportes();
            this.Hide();
            reportes.ShowDialog();

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            this.Hide();
            clientes.ShowDialog();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Ventas ventas = new Ventas();
            this.Hide();
            ventas.ShowDialog();
        }
    }
}
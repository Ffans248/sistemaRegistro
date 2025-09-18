using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistemaRegistro
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
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
    }
}

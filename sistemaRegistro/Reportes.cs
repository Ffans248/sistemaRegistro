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
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ReportesCompras reportesCompras = new ReportesCompras();
            this.Hide();
            reportesCompras.Show();
            
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ReportesVentas reportesVentas = new ReportesVentas();
            this.Hide();
            reportesVentas.Show();
        }
    }
}

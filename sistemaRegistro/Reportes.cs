using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static sistemaRegistro.Login;

namespace sistemaRegistro
{
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
            CargarProveedores();
        }
        private void GenerarPDF(string nombreArchivo, DataTable dt, string titulo)
        {
            try
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(doc, new FileStream(nombreArchivo, FileMode.Create));
                doc.Open();

                // Título
                Paragraph p = new Paragraph(titulo, iTextSharp.text.FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD));
                p.Alignment = Element.ALIGN_CENTER;
                doc.Add(p);
                doc.Add(new Paragraph("\n"));

                // Tabla
                PdfPTable table = new PdfPTable(dt.Columns.Count) { WidthPercentage = 100 };

                // Encabezados
                foreach (DataColumn c in dt.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(c.ColumnName, iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)));

                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }

                // Filas
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        table.AddCell(item.ToString());
                    }
                }

                doc.Add(table);
                doc.Close();
                MessageBox.Show("PDF generado: " + nombreArchivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando PDF: " + ex.Message);
            }
        }

        //Reporte de Proveedores
        private void GenerarPDFCompras()
        {
            if (cmbProveedorReport.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un proveedor para generar el reporte.");
                return;
            }

            int idProveedor = Convert.ToInt32(cmbProveedorReport.SelectedValue);
            DataTable dt = new DataTable();
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT c.numeroFactura, c.fechaCompra, p.nombreProducto, dc.cantidad, dc.precioUnitario, dc.descuento, dc.subtotal
                  FROM tbCompra c
                  INNER JOIN tbDetalleCompra dc ON c.idCompra = dc.idCompra
                  INNER JOIN tbProducto p ON dc.idProducto = p.idProducto
                  WHERE c.idProveedor = @prov AND c.fechaCompra >= DATEADD(month, -1, GETDATE())", con);
                cmd.Parameters.AddWithValue("@prov", idProveedor);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            GenerarPDF("ComprasProveedor.pdf", dt, "Reporte de Compras por Proveedor");
        }
        //Reporte Stock Bajo

        private void GenerarPDFStockBajo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT nombreProducto, stockActual, stockMinimo FROM tbProducto WHERE stockActual <= stockMinimo", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            GenerarPDF("ProductosStockBajo.pdf", dt, "Reporte de Productos con Stock Bajo");
        }
        //Reporte Sin Stock

        private void GenerarPDFSinExistencia()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT nombreProducto, stockActual FROM tbProducto WHERE stockActual = 0", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            GenerarPDF("ProductosSinExistencia.pdf", dt, "Reporte de Productos Sin Existencia");
        }















        //------------------------------------------------------------- 
        private void CargarProveedores()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT idProveedor, nombre FROM tbProveedor", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbProveedorReport.DataSource = dt;
                cmbProveedorReport.DisplayMember = "nombre";
                cmbProveedorReport.ValueMember = "idProveedor";
            }
        }

        private void btnBuscarCompras_Click(object sender, EventArgs e)
        {
            int idProveedor = Convert.ToInt32(cmbProveedorReport.SelectedValue);

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string query = @"SELECT c.numeroFactura, p.nombre AS Proveedor, c.fechaCompra, c.totalCompra
                         FROM tbCompra c
                         INNER JOIN tbProveedor p ON c.idProveedor = p.idProveedor
                         WHERE c.idProveedor = @idProveedor
                         AND c.fechaCompra BETWEEN @desde AND @hasta";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@idProveedor", idProveedor);
                da.SelectCommand.Parameters.AddWithValue("@desde", dtpDesde.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@hasta", dtpHasta.Value.Date);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvReporte.DataSource = dt;
            }
        }

        private void btnStockBajo_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT nombreProducto, stockActual, stockMinimo FROM tbProducto WHERE stockActual < stockMinimo", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvReporte.DataSource = dt;
            }
        }

        private void btnSinStock_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT nombreProducto, stockActual FROM tbProducto WHERE stockActual = 0", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvReporte.DataSource = dt;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            GenerarPDFCompras();

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            GenerarPDFStockBajo();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            GenerarPDFSinExistencia();
        }
    }
}
   


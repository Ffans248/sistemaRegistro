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
using System.Windows.Forms.DataVisualization.Charting;

using static sistemaRegistro.Login;


namespace sistemaRegistro
{
    public partial class ReportesVentas : Form
    {
        public ReportesVentas()
        {
            InitializeComponent();
        }

        private void ReportesVentas_Load(object sender, EventArgs e)
        {

            // Llenar opciones de reporte
            cmbTipoReporte.Items.AddRange(new object[] {
                "Ventas (lista)",
                "Por Cliente",
                "Productos más vendidos",
                "Ingresos por Día",
                "Ingresos por Semana",
                "Ingresos por Mes"
            });
            cmbTipoReporte.SelectedIndex = 0;

            // Default fechas
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;

            CargarClientesCombo();
            cmbClientes.Visible = false;

            cmbTipoReporte.SelectedIndexChanged += (s, ev) =>
            {
                cmbClientes.Visible = cmbTipoReporte.SelectedItem.ToString() == "Por Cliente";
            };

            
        }
        private void CargarClientesCombo()
        {
            try
            {
                using (SqlConnection con = new Conexion().AbrirConexion())
                {
                    string q = "SELECT idCliente, nombre FROM dbo.tbCliente ORDER BY nombre";
                    SqlDataAdapter da = new SqlDataAdapter(q, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataRow fila = dt.NewRow();
                    fila["idCliente"] = 0;
                    fila["nombre"] = "[Todos los Clientes]";
                    dt.Rows.InsertAt(fila, 0);

                    cmbClientes.DataSource = dt;
                    cmbClientes.DisplayMember = "nombre";
                    cmbClientes.ValueMember = "idCliente";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando clientes: " + ex.Message);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1); // incluir todo el día
            string tipo = cmbTipoReporte.SelectedItem.ToString();

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new Conexion().AbrirConexion())
                {
                    SqlDataAdapter da = null;
                    switch (tipo)
                    {
                        case "Ventas (lista)":
                            da = new SqlDataAdapter(
                                "SELECT v.idVenta, v.numeroFactura, v.fechaVenta, c.nombre AS Cliente, v.formaPago, v.descuentoGeneral, v.totalVenta " +
                                "FROM dbo.tbVenta v LEFT JOIN dbo.tbCliente c ON v.idCliente = c.idCliente " +
                                "WHERE v.fechaVenta BETWEEN @desde AND @hasta ORDER BY v.fechaVenta", con);
                            da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                            da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                            break;

                        case "Por Cliente":
                            da = new SqlDataAdapter(
                                "SELECT v.idVenta, v.numeroFactura, v.fechaVenta, v.formaPago, v.totalVenta " +
                                "FROM dbo.tbVenta v WHERE v.idCliente = @idCliente AND v.fechaVenta BETWEEN @desde AND @hasta ORDER BY v.fechaVenta", con);
                            int idCliente = Convert.ToInt32(cmbClientes.SelectedValue);
                            da.SelectCommand.Parameters.AddWithValue("@idCliente", idCliente);
                            da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                            da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                            break;

                        case "Productos más vendidos":
                            da = new SqlDataAdapter(
                                "SELECT p.idProducto, p.nombreProducto, SUM(d.cantidad) AS TotalCantidad, SUM(d.subtotal) AS TotalMonto " +
                                "FROM dbo.tbDetalleVenta d JOIN dbo.tbVenta v ON d.idVenta = v.idVenta JOIN dbo.tbProducto p ON d.idProducto = p.idProducto " +
                                "WHERE v.fechaVenta BETWEEN @desde AND @hasta " +
                                "GROUP BY p.idProducto, p.nombreProducto ORDER BY TotalCantidad DESC", con);
                            da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                            da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                            break;

                        case "Ingresos por Día":
                            da = new SqlDataAdapter(
                                "SELECT CAST(v.fechaVenta AS DATE) AS Fecha, SUM(v.totalVenta) AS TotalIngresos " +
                                "FROM dbo.tbVenta v WHERE v.fechaVenta BETWEEN @desde AND @hasta " +
                                "GROUP BY CAST(v.fechaVenta AS DATE) ORDER BY Fecha", con);
                            da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                            da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                            break;

                        case "Ingresos por Semana":
                            da = new SqlDataAdapter(
                                "SELECT DATEPART(YEAR, v.fechaVenta) AS Año, DATEPART(WEEK, v.fechaVenta) AS Semana, SUM(v.totalVenta) AS TotalIngresos " +
                                "FROM dbo.tbVenta v WHERE v.fechaVenta BETWEEN @desde AND @hasta " +
                                "GROUP BY DATEPART(YEAR, v.fechaVenta), DATEPART(WEEK, v.fechaVenta) ORDER BY Año, Semana", con);
                            da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                            da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                            break;

                        case "Ingresos por Mes":
                            da = new SqlDataAdapter(
                                "SELECT DATEPART(YEAR, v.fechaVenta) AS Año, DATEPART(MONTH, v.fechaVenta) AS Mes, SUM(v.totalVenta) AS TotalIngresos " +
                                "FROM dbo.tbVenta v WHERE v.fechaVenta BETWEEN @desde AND @hasta " +
                                "GROUP BY DATEPART(YEAR, v.fechaVenta), DATEPART(MONTH, v.fechaVenta) ORDER BY Año, Mes", con);
                            da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                            da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                            break;
                    }

                    if (da != null)
                    {
                        da.Fill(dt);
                    }
                }

                dgvReporte.DataSource = dt;
                GenerarGraficaSegunReporte(tipo, dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando reporte: " + ex.Message);
            }
        }

        private void GenerarGraficaSegunReporte(string tipo, DataTable dt)
        {
            chartReporte.Series.Clear();
            chartReporte.Titles.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                chartReporte.Titles.Add("No hay datos para graficar");
                return;
            }

            if (tipo == "Productos más vendidos")
            {
                chartReporte.Titles.Add("Productos más vendidos");
                Series s = new Series("Cantidad");
                s.ChartType = SeriesChartType.Bar;
                s.XValueMember = "nombreProducto";
                s.YValueMembers = "TotalCantidad";
                chartReporte.Series.Add(s);
                chartReporte.DataSource = dt;
                chartReporte.DataBind();
                // Ajustes: mostrar etiquetas
                foreach (var ser in chartReporte.Series) ser.IsValueShownAsLabel = true;
            }
            else if (tipo == "Ingresos por Día" || tipo == "Ingresos por Semana" || tipo == "Ingresos por Mes")
            {
                chartReporte.Titles.Add("Ingresos");
                Series s = new Series("Ingresos");
                s.ChartType = SeriesChartType.Column;

                // Elegir columnas que contienen los valores según el query
                if (dt.Columns.Contains("Fecha"))
                {
                    s.XValueMember = "Fecha";
                    s.YValueMembers = "TotalIngresos";
                }
                else if (dt.Columns.Contains("Semana"))
                {
                    // crear una columna compuesta año+semana para eje X
                    if (!dt.Columns.Contains("Periodo"))
                        dt.Columns.Add("Periodo", typeof(string));

                    foreach (DataRow r in dt.Rows)
                    {
                        r["Periodo"] = $"{r["Año"]}-S{r["Semana"]}";
                    }

                    s.XValueMember = "Periodo";
                    s.YValueMembers = "TotalIngresos";
                }
                else if (dt.Columns.Contains("Mes"))
                {
                    if (!dt.Columns.Contains("Periodo"))
                        dt.Columns.Add("Periodo", typeof(string));

                    foreach (DataRow r in dt.Rows)
                    {
                        r["Periodo"] = $"{r["Año"]}-{r["Mes"]}";
                    }

                    s.XValueMember = "Periodo";
                    s.YValueMembers = "TotalIngresos";
                }
                chartReporte.Series.Add(s);
                chartReporte.DataSource = dt;
                chartReporte.DataBind();
                foreach (var ser in chartReporte.Series) ser.IsValueShownAsLabel = true;
            }
            else // Ventas lista o Por Cliente -> mostrar totales por factura (ejemplo)
            {
                chartReporte.Titles.Add("Total por Factura (ejemplo)");
                if (dt.Columns.Contains("numeroFactura") && dt.Columns.Contains("totalVenta"))
                {
                    Series s = new Series("Total");
                    s.ChartType = SeriesChartType.Column;
                    s.XValueMember = "numeroFactura";
                    s.YValueMembers = "totalVenta";
                    chartReporte.Series.Add(s);
                    chartReporte.DataSource = dt;
                    chartReporte.DataBind();
                    foreach (var ser in chartReporte.Series) ser.IsValueShownAsLabel = true;
                }
            }
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            if (dgvReporte.DataSource == null)
            {
                MessageBox.Show("Genere un reporte antes de exportar.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = "reporte.pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    // 1) Guardar imagen del chart en un MemoryStream
                    MemoryStream chartStream = new MemoryStream();
                    chartReporte.SaveImage(chartStream, ChartImageFormat.Png);
                    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(chartStream.ToArray());
                    chartImage.Alignment = Element.ALIGN_CENTER;
                    chartImage.ScaleToFit(520f, 300f);

                    // 2) Crear documento PDF
                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // Título
                        var titulo = new Paragraph("Reporte generado: " + DateTime.Now.ToString("g"))
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 12f
                        };
                        doc.Add(titulo);

                        // Añadir imagen de la gráfica
                        doc.Add(chartImage);

                        // Añadir una tabla con los datos (DataGridView)
                        PdfPTable pdfTable = new PdfPTable(dgvReporte.Columns.Count);
                        pdfTable.WidthPercentage = 100;

                        // Encabezados
                        foreach (DataGridViewColumn column in dgvReporte.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                            cell.BackgroundColor = new BaseColor(240, 240, 240);
                            pdfTable.AddCell(cell);
                        }

                        // Filas
                        foreach (DataGridViewRow row in dgvReporte.Rows)
                        {
                            if (row.IsNewRow) continue;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                var texto = cell.Value == null ? "" : cell.Value.ToString();
                                pdfTable.AddCell(new Phrase(texto));
                            }
                        }

                        doc.Add(new Paragraph("\n")); // espacio
                        doc.Add(pdfTable);

                        doc.Close();
                    }

                    MessageBox.Show("PDF generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exportando PDF: " + ex.Message);
                }
            }
        }
    }
    }

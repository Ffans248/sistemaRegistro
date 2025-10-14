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
    public partial class productosCategorias : Form
    {
        public productosCategorias()
        {
            InitializeComponent();
            CargarProductos();
            CargarCategorias();
            CargarAsignaciones();
        }

        private void CargarProductos()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT idProducto, nombreProducto FROM tbProducto", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbProducto.DataSource = dt;
                cbProducto.DisplayMember = "nombreProducto";
                cbProducto.ValueMember = "idProducto";
            }
        }

        private void CargarCategorias()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT idCategoria, nombreCategoria FROM tbCategoria", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                clbCategorias.Items.Clear();
                foreach (DataRow fila in dt.Rows)
                {
                    clbCategorias.Items.Add(new
                    {
                        Id = fila["idCategoria"],
                        Nombre = fila["nombreCategoria"]
                    }, false);
                }
            }
        }

        private void CargarAsignaciones()
        {
            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                string sql = @"
                    SELECT 
                        p.idProducto,
                        p.nombreProducto AS Producto,
                        c.nombreCategoria AS Categoria
                    FROM tbProductoCategoria pc
                    INNER JOIN tbProducto p ON pc.idProducto = p.idProducto
                    INNER JOIN tbCategoria c ON pc.idCategoria = c.idCategoria
                    ORDER BY p.nombreProducto, c.nombreCategoria";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAsignaciones.DataSource = dt;
            }
        }

        private void productosCategoria_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cbProducto.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un producto válido.");
                return;
            }

            int idProducto = Convert.ToInt32(cbProducto.SelectedValue);

            using (SqlConnection con = new Conexion().AbrirConexion())
            {
                // Borrar asignaciones previas
                SqlCommand del = new SqlCommand("DELETE FROM tbProductoCategoria WHERE idProducto = @IdProd", con);
                del.Parameters.AddWithValue("@IdProd", idProducto);
                del.ExecuteNonQuery();

                // Insertar nuevas asignaciones
                foreach (var item in clbCategorias.CheckedItems)
                {
                    dynamic cat = item;
                    int idCat = Convert.ToInt32(cat.Id);

                    SqlCommand ins = new SqlCommand("INSERT INTO tbProductoCategoria (idProducto, idCategoria) VALUES (@Prod, @Cat)", con);
                    ins.Parameters.AddWithValue("@Prod", idProducto);
                    ins.Parameters.AddWithValue("@Cat", idCat);
                    ins.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Categorías asignadas correctamente al producto.");
            CargarAsignaciones();
        }
    }
}

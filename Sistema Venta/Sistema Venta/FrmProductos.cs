using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Sistema_Venta
{
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            string conexion = "server=localhost; database=pos; user=root; password=Yesicaespinal2003;";

            if (txtNombre.Text.Trim() == "" ||
                cmbCategoria.Text.Trim() == "" ||
                txtPrecio.Text.Trim() == "" ||
                txtStock.Text.Trim() == "")
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text.Trim(), out precio))
            {
                MessageBox.Show("El precio debe ser un número válido. Ejemplo: 150.50",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

          
            int stock;
            if (!int.TryParse(txtStock.Text.Trim(), out stock))
            {
                MessageBox.Show("El stock debe ser un número entero.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexion))
                {
                    conn.Open();

                    string consulta = @"INSERT INTO productos (nombre, categoria, precio, stock) VALUES (@nombre, @categoria, @precio, @stock)";

                    using (MySqlCommand cmd = new MySqlCommand(consulta, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@categoria", cmbCategoria.Text.Trim());
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@stock", stock);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Producto guardado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el producto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            
            txtNombre.Clear();
            cmbCategoria.SelectedIndex = -1;
            txtPrecio.Clear();
            txtStock.Clear();

            txtNombre.Focus();
        }

        private void CargarProductos(string buscar = "")
        {

            try
            {
                CONEXIONBASE conexion = new CONEXIONBASE();

                using (MySqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();
                    string query = @"SELECT id,
                                   nombre,
                                   categoria,
                                   precio,
                                   stock
                                   FROM productos WHERE nombre LIKE @buscar OR categoria LIKE @buscar";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@buscar", "%" + buscar + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);
                    dgvProductos.DataSource = tabla;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }


        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        

        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {


            CargarProductos(txtBuscar.Text);


        }
    }

}
    

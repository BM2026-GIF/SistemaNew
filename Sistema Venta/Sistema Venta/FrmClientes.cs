using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Venta
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }

       

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            string conexion = "server=localhost; database=pos; user=root; password=Yesicaespinal2003;";

            try
            {
                using (MySqlConnection con = new MySqlConnection(conexion))
                {
                    con.Open();

                    string consulta = "INSERT INTO clientes (nombre, telefono, correo, direccion) VALUES (@nombre, @telefono, @correo, @direccion)";

                    using (MySqlCommand cmd = new MySqlCommand(consulta, con))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                        cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("GUARDADO EXITOSAMENTE");

                        txtNombre.Clear();
                        txtTelefono.Clear();
                        txtCorreo.Clear();
                        txtDireccion.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void CargarClientes(string buscar = "")
        {

            try
            {
                CONEXIONBASE conexion = new CONEXIONBASE();

                using (MySqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();
                    string query = @"SELECT id,
                                   nombre, 
                                   correo, 
                                   telefono, 
                                   direccion FROM clientes WHERE nombre LIKE @buscar OR correo LIKE @buscar OR telefono LIKE @buscar";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@buscar", "%" + buscar + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);
                    dgvClientes.DataSource = tabla;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
         }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarClientes();


        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

            CargarClientes(txtBuscar.Text);
        }
    }
}

           
           
        
    
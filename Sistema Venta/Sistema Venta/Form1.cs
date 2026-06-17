using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Venta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Form formularioActivo = null;

        private void AbrirFormulario(Form form)
        {
            if (formularioActivo != null)
                formularioActivo.Close();


            formularioActivo = form;
            form.TopLevel = false;


            PanelContenedor.Controls.Clear();
            PanelContenedor.Controls.Add(form);

            form.Show();
        }

        private void cLIENTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmClientes());        }

        private void pRODUCTOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProductos());
        }

        private void eMPLEADOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmEmpleados());
        }
    }
}

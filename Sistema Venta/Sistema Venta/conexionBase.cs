using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Sistema_Venta
{
    class CONEXIONBASE
    {

        private string cadenaConexion = "server=localhost;database=pos;uid=root;password=Yesicaespinal2003";


       public MySqlConnection ObtenerConexion()
        {


            MySqlConnection conexion = new MySqlConnection(cadenaConexion);
            return conexion;


        }

    }
}

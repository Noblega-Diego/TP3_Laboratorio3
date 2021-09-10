using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3_Lab3_ProductosBusqueda
{
    class Conexion
    {
        public MySqlConnection Conectar()
        {
            try
            {
                MySqlConnection conexionSql;
                MySqlConnectionStringBuilder configBuilder = new MySqlConnectionStringBuilder();
                configBuilder.Database = "tienda";
                configBuilder.Server = "localhost";
                configBuilder.Port = 3306;
                configBuilder.UserID = "root";
                configBuilder.Password = "mysql";
                configBuilder.SslMode = MySqlSslMode.None;
            
                conexionSql = new MySqlConnection(configBuilder.ConnectionString);
                return conexionSql;
            }catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}

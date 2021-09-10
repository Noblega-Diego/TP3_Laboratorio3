using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppConexionMysql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            String servidor, puerto, usuario, contrasenia, bd;

            servidor = this.tbServidor.Text;
            puerto = this.tbPuerto.Text;
            usuario = this.tbUsuario.Text;
            contrasenia = this.tbContrasenia.Text;
            bd = this.tbBD.Text;
            MySqlConnectionStringBuilder configConecion = new MySqlConnectionStringBuilder();
            configConecion.Database = bd;
            configConecion.Server = servidor;
            configConecion.Port = Convert.ToUInt16(puerto);
            configConecion.UserID = usuario;
            configConecion.Password = contrasenia;
            configConecion.SslMode = MySqlSslMode.None;
            Console.WriteLine(configConecion.ConnectionString);
            MySqlConnection connectionDB = new MySqlConnection(configConecion.ConnectionString);
            MySqlDataReader reader;
            String data = "";
            try { 
                MySqlCommand command = new MySqlCommand("SHOW databases");
                command.Connection = connectionDB;
                connectionDB.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data += reader.GetString(0);
                }
                MessageBox.Show(data);
            }
            catch (MySqlException ex) { 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connectionDB.Close();
            }
        }
    }
}

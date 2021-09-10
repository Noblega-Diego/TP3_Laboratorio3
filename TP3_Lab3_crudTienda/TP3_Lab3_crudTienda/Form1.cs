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

namespace TP3_Lab3_crudTienda
{
    public partial class Form1 : Form
    {
        private int idSeleccionado = -1;

        private int IDNONSELECT = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            String nombre, codigo, descripcion;
            decimal precioPublico;
            int existencias;
            try
            {
                codigo = txtCodigo.Text;
                nombre = txtNombre.Text;
                descripcion = txtDescripcion.Text;
                precioPublico = Convert.ToDecimal(txtPrecioPublico.Text);
                existencias = Convert.ToInt32(txtExistencias.Text);
            }catch(System.FormatException ex)
            {
                MessageBox.Show("Los campos ingresados no son validos");
                return;
            }

  
            MySqlConnection connectionDB = conectar();
            try
            {
                String sql = "INSERT INTO productos(codigo, nombre, descripcion, precio_publico, existencias) " +
                "VALUES(@codigo,@nombre,@descripcion,@precioPublico,@existencias)";
                MySqlCommand command = new MySqlCommand(sql);
                command.Connection = connectionDB;
                command.Parameters.AddWithValue("@codigo",codigo);
                command.Parameters.AddWithValue("@nombre",nombre);
                command.Parameters.AddWithValue("@descripcion",descripcion);
                command.Parameters.AddWithValue("@precioPublico",precioPublico);
                command.Parameters.AddWithValue("@existencias",existencias);
                connectionDB.Open();
                int result = command.ExecuteNonQuery();
                Limpiar();
                MessageBox.Show("Guardado con exito");
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Error al guardar:" + ex.Message);
            }
            finally
            {
                connectionDB.Close();
            }
            
        }

        private MySqlConnection conectar()
        {
            MySqlConnectionStringBuilder configurationBuilder = new MySqlConnectionStringBuilder
            {
                Database = "tienda",
                Server = "localhost",
                Port = 3306,
                UserID = "root",
                Password = "mysql",
                SslMode = MySqlSslMode.None
            };
            return new MySqlConnection(configurationBuilder.ConnectionString);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            String nombre, codigo, descripcion;
            decimal precioPublico;
            int existencias;
            if (idSeleccionado == IDNONSELECT)
            {
                MessageBox.Show("No se ha selecionado ningun producto para actualizar");
                return;
            }

            try
            {
                codigo = txtCodigo.Text;
                nombre = txtNombre.Text;
                descripcion = txtDescripcion.Text;
                precioPublico = Convert.ToDecimal(txtPrecioPublico.Text);
                existencias = Convert.ToInt32(txtExistencias.Text);
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Los campos ingresados no son validos");
                return;
            }


            MySqlConnection connectionDB = conectar();
            try
            {
                String sql = "UPDATE productos SET codigo= @codigo, nombre= @nombre, descripcion= @descripcion, precio_publico= @precioPublico, existencias= @existencias WHERE id= @id";
                MySqlCommand command = new MySqlCommand(sql);
                command.Connection = connectionDB;
                command.Parameters.AddWithValue("@id", idSeleccionado);
                command.Parameters.AddWithValue("@codigo", codigo);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@descripcion", descripcion);
                command.Parameters.AddWithValue("@precioPublico", precioPublico);
                command.Parameters.AddWithValue("@existencias", existencias);
                connectionDB.Open();
                int result = command.ExecuteNonQuery();
                Limpiar();
                MessageBox.Show("Actualizado con exito");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:" + ex.Message);
            }
            finally
            {
                connectionDB.Close();
            }
        }

        private void Limpiar()
        {
            idSeleccionado = IDNONSELECT;
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioPublico.Text = "";
            txtExistencias.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            String nombre = "", codigo = "", descripcion = "";
            decimal precioPublico = 0;
            int existencias = 0, id = -1;

            MySqlConnection connectionDB = conectar();
            try
            {
                String sql = "SELECT id, codigo, nombre, descripcion, precio_publico, existencias FROM productos WHERE codigo= @codigo";
                MySqlCommand command = new MySqlCommand(sql);
                command.Connection = connectionDB;
                command.Parameters.AddWithValue("@codigo",txtCodigo.Text);
                connectionDB.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    
                    id = reader.GetInt32("id");
                    codigo = reader.GetString("codigo");
                    nombre = reader.GetString("nombre");
                    descripcion = reader.GetString("descripcion");
                    precioPublico = reader.GetDecimal("precio_publico");
                    existencias = reader.GetInt32("existencias");
                }
                if(id != -1)
                {
                    idSeleccionado = id;
                    txtCodigo.Text = codigo;
                    txtNombre.Text = nombre;
                    txtDescripcion.Text = descripcion;
                    txtPrecioPublico.Text = Convert.ToString(precioPublico);
                    txtExistencias.Text = Convert.ToString(existencias);

                    MessageBox.Show("Se ha encontrado el producto");
                }
                else
                    MessageBox.Show("No se ha encontrado");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:" + ex.Message);
            }
            finally
            {
                connectionDB.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String nombre, codigo, descripcion;
            decimal precioPublico;
            int existencias;
            if (idSeleccionado == IDNONSELECT)
            {
                MessageBox.Show("No se ha selecionado ningun producto para eliminar");
                return;
            }


            MySqlConnection connectionDB = conectar();
            try
            {
                String sql = "DELETE FROM productos WHERE id= @id";
                MySqlCommand command = new MySqlCommand(sql);
                command.Connection = connectionDB;
                command.Parameters.AddWithValue("@id", idSeleccionado);
                connectionDB.Open();
                int result = command.ExecuteNonQuery();
                Limpiar();
                MessageBox.Show("Se ha eliminado con exito");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar:" + ex.Message);
            }
            finally
            {
                connectionDB.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3_Lab3_ProductosBusqueda.modelo;

namespace TP3_Lab3_ProductosBusqueda.controlador
{
    class CtrlProductos: Conexion
    {
        public List<Object> Consultar(String dato)
        {
            List<Object> lista = new List<Object>();
            MySqlConnection conexionDB =  base.Conectar();
            String sql;

            if(dato == "" || dato == null)
            {
                sql = "SELECT id, codigo, nombre, descripcion, precio_Publico, existencias FROM productos" +
                    " ORDER BY nombre ASC";
            }
            else
            {
                sql = "SELECT id, codigo, nombre, descripcion, precio_Publico, existencias FROM productos " +
                    "WHERE nombre LIKE '%@dato%' OR descripcion LIKE '%@dato%' ORDER BY nombre ASC";
                sql = sql.Replace("@dato", dato);
            }   
            try {
                MySqlCommand command = new MySqlCommand(sql);
                command.Connection = conexionDB;
                conexionDB.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(reader.GetString("id"));
                    producto.Codigo = reader.GetString("codigo");
                    producto.Nombre = reader.GetString("nombre");
                    producto.Descripcion = reader.GetString("descripcion");
                    producto.PrecioPublico = reader.GetDouble("precio_publico");
                    producto.Existencias = reader.GetInt32("existencias");
                    lista.Add(producto);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conexionDB.Close();
            }
            
            return lista;
        }

        public void Guardar(Producto producto)
        {
            String sql = "INSERT INTO productos(codigo, nombre, descripcion, precio_publico, existencias)" +
                "VALUES(@codigo, @nombre, @descripcion, @precioPublico, @existencias)";
            MySqlConnection conexionDB =  base.Conectar();
            try
            {
                MySqlCommand command = new MySqlCommand(sql) { Connection = conexionDB };
                command.Parameters.AddWithValue("@codigo", producto.Codigo);
                command.Parameters.AddWithValue("@nombre", producto.Nombre);
                command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@precioPublico", producto.PrecioPublico);
                command.Parameters.AddWithValue("@existencias", producto.Existencias);
                conexionDB.Open();
                command.ExecuteNonQuery();

            }catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }
        }

        public void Actualizar(Producto producto)
        {
            String sql = "UPDATE productos SET codigo= @codigo, nombre= @nombre, " +
                "descripcion= @descripcion, precio_publico= @precioPublico, existencias= @existencias " +
                "WHERE id = @id";
            MySqlConnection conexionDB = base.Conectar();
            try
            {
                MySqlCommand command = new MySqlCommand(sql) { Connection = conexionDB };
                command.Parameters.AddWithValue("@id", producto.Id);
                command.Parameters.AddWithValue("@codigo", producto.Codigo);
                command.Parameters.AddWithValue("@nombre", producto.Nombre);
                command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@precioPublico", producto.PrecioPublico);
                command.Parameters.AddWithValue("@existencias", producto.Existencias);
                conexionDB.Open();
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }
        }

        public void ELiminar(Producto producto)
        {
            String sql = "DELETE FROM productos WHERE id = "+producto.Id;
            MySqlConnection conexionDB = base.Conectar();
            try
            {
                MySqlCommand command = new MySqlCommand(sql) { Connection = conexionDB };
                conexionDB.Open();
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }
        }
         
    }
}

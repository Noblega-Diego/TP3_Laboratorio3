using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3_Lab3_ProductosBusqueda.modelo
{
    class Producto
    {
        private int id;
        private string codigo;
        private string nombre;
        private string descripcion;
        private double precioPublico;
        private int existencias;

        public int Id { get => id; set => id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public double PrecioPublico { get => precioPublico; set => precioPublico = value; }
        public int Existencias { get => existencias; set => existencias = value; }


    }
}

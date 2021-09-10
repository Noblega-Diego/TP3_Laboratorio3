using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP3_Lab3_ProductosBusqueda.controlador;
using TP3_Lab3_ProductosBusqueda.modelo;

namespace TP3_Lab3_ProductosBusqueda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            String abuscar = txtBuscar.Text;
            cargarTabla(abuscar);
        }

        private void cargarTabla(string abuscar)
        {
            CtrlProductos ctrProductos = new CtrlProductos();
            List<Object> productos = ctrProductos.Consultar(abuscar);
            tblProductos.Rows.Clear();

            foreach (Object o in productos)
            {
                Producto producto = (Producto) o;
                tblProductos.Rows.Add(producto.Codigo,producto.Nombre,producto.Descripcion,producto.PrecioPublico,producto.Existencias, producto.Id);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();
            producto.Codigo = txtCodigo.Text;
            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.PrecioPublico = Convert.ToDouble(txtPrecioPublico.Text);
            producto.Existencias = Convert.ToInt32(txtExistencias.Text);
            CtrlProductos ctrl = new CtrlProductos();
            if(txtId.Text != "")
            {
                producto.Id = Convert.ToInt32(txtId.Text);
                ctrl.Actualizar(producto);
            }
            else
            {
                ctrl.Guardar(producto);
            }
            Limpiar(); 
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioPublico.Text = "";
            txtExistencias.Text = "";
            txtId.Text = "";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = tblProductos.CurrentRow;
            
            txtCodigo.Text = currentRow.Cells[0].Value.ToString();
            txtNombre.Text = currentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = currentRow.Cells[2].Value.ToString();
            txtPrecioPublico.Text = currentRow.Cells[3].Value.ToString();
            txtExistencias.Text = currentRow.Cells[4].Value.ToString();
            txtId.Text = currentRow.Cells[5].Value.ToString();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = tblProductos.CurrentRow;
            Producto producto = new Producto();
            producto.Codigo = currentRow.Cells[0].Value.ToString();
            producto.Nombre = currentRow.Cells[1].Value.ToString();
            producto.Descripcion = currentRow.Cells[2].Value.ToString();
            producto.PrecioPublico = Convert.ToDouble(currentRow.Cells[3].Value.ToString());
            producto.Existencias = Convert.ToInt32(currentRow.Cells[4].Value.ToString());
            producto.Id = Convert.ToInt32(currentRow.Cells[5].Value.ToString());
            CtrlProductos ctrl = new CtrlProductos();
            ctrl.ELiminar(producto);
        }
    }
}

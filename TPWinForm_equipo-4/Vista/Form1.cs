using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Vista
{
    public partial class Form1 : Form
    {
        private List<Articulo> listArticulo;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargaDatos();
        }
        
        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.imagenURL);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://sferaone.es/wp-content/uploads/2023/02/placeholder-1.png");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo aux = new Articulo();
            aux = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            Detalle fr = new Detalle(aux);
            fr.ShowDialog();

        }
        //Carga todos los datos de la base de datos en la datagrid
        private void CargaDatos()
        {
            ArticuloNegocio art = new ArticuloNegocio();
            listArticulo = art.listar();
            dgvArticulo.DataSource = listArticulo;
            dgvArticulo.Columns["imagenURL"].Visible = false;   // OCULTA LA COLUMNA.
            cargarImagen(listArticulo[0].imagenURL);
        }

        //Elimina el articulo seleccionado (fila) en la Datagrid
        private void btnEliminar_Click(object sender, EventArgs e)
        {
             ArticuloNegocio datos = new ArticuloNegocio();
            Articulo aux = new Articulo();
            aux = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            DialogResult res;
            res = MessageBox.Show("Desea eliminar este articulo? \n \t" +  aux.descripcion.ToString(), "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(res == DialogResult.Yes)
            {
                datos.Eliminar(aux.ID);
                CargaDatos();
                
            }
            else
            {
                return;
            }
        }
    }
}

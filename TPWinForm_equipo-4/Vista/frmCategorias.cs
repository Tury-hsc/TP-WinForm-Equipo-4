using Negocio;
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

namespace Vista
{
    public partial class frmCategorias : Form
    {
        private List<Categoria> listaCategoria;
        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            CategoriaNegocio cat = new CategoriaNegocio();
            listaCategoria = cat.listar();
            dgvCategorias.DataSource = listaCategoria;

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Categoria aux = new Categoria();
            CategoriaNegocio negocioCat = new CategoriaNegocio();

            try
            {
                aux.descripcion = txtCategoria.Text;

                negocioCat.agregar(aux);
                MessageBox.Show(" AGREGAADO ");
                cargarDatos();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Categoria aux = new Categoria();
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                aux = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
                negocio.eliminar(aux.ID);
                MessageBox.Show("ELIMINADO ");
                cargarDatos();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}

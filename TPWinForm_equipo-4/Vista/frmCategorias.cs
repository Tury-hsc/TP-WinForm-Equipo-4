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
        private List<Marca> listaMarca;
        private bool marca = false;
        public frmCategorias(bool marca)
        {
            InitializeComponent();
            this.marca = marca;
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            if (marca == true)
            {
                MarcaNegocio marca = new MarcaNegocio();
                listaMarca = marca.listar();
                dgvCategorias.DataSource = listaMarca;

                lblCategoria.Text = " MARCA: ";
            }
            else
            {
                CategoriaNegocio cat = new CategoriaNegocio();
                listaCategoria = cat.listar();
                dgvCategorias.DataSource = listaCategoria;

            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoria.Text))
            {
                MessageBox.Show("Agregue la descripcion por favor");
                return;
            }
            else
            {
                if (marca == true)
                {
                    Marca auxMarca = new Marca();
                    MarcaNegocio negocioMarca = new MarcaNegocio();

                    try
                    {
                        auxMarca.descripcion = txtCategoria.Text;  // En este caso contiene una marca, cambiar el nombre de la LBL.

                        negocioMarca.agregar(auxMarca);
                        MessageBox.Show(" MARCA AGREGADO ");
                        cargarDatos();

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                else
                {

                    Categoria aux = new Categoria();
                    CategoriaNegocio negocioCat = new CategoriaNegocio();

                    try
                    {
                        aux.descripcion = txtCategoria.Text;

                        negocioCat.agregar(aux);
                        MessageBox.Show(" AGREGADO ");
                        cargarDatos();

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (marca == true)
            {
                Marca auxMarca = new Marca();
                MarcaNegocio negocioMarca = new MarcaNegocio();

                try
                {
                    auxMarca = (Marca)dgvCategorias.CurrentRow.DataBoundItem;
                    negocioMarca.eliminar(auxMarca.ID);
                    MessageBox.Show(" MARCA ELIMINADA ");
                    cargarDatos();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                Categoria aux = new Categoria();
                CategoriaNegocio negocio = new CategoriaNegocio();

                try
                {
                    aux = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
                    negocio.eliminar(aux.ID);
                    MessageBox.Show(" ELIMINADO ");
                    cargarDatos();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}

using System;
using System.Collections;
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
        private List<Imagen> listaImganes;
        private int img = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargaDatos();

            cboCampo.Items.Add("Número");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");
            cboCriterio.Items.Add("Mayor a");
            cboCriterio.Items.Add("Menor a");
        }


        }



        //Faltan botones para reccorer imagenes
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
            Detalle fr = new Detalle(aux, false);
            fr.ShowDialog();
            CargaDatos();

        }
        //Carga todos los datos de la base de datos en la datagrid
        private void CargaDatos()
        {

            dgvArticulo.DataSource = null;
            ImagenNegocio imgNeg = new ImagenNegocio();
            ArticuloNegocio art = new ArticuloNegocio();
            listArticulo = art.listar();
            listaImganes = imgNeg.listar(1);
            dgvArticulo.DataSource = listArticulo;
            dgvArticulo.Columns[7].Visible = false;   // OCULTA LA COLUMNA.
                                                      //  cargarImagen(listaImganes[0].url);
        }

        //Elimina el articulo seleccionado (fila) en la Datagrid
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio datos = new ArticuloNegocio();
            Articulo aux = new Articulo();
            aux = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            DialogResult res;
            res = MessageBox.Show("Desea eliminar este articulo? \n \t" + aux.descripcion.ToString(), "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                datos.Eliminar(aux.ID);
                CargaDatos();

            }
            else
            {
                return;
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = textBox1.Text;

            if (filtro.Length >= 3)
            {
                listaFiltrada = listArticulo.FindAll(x => x.nombre.ToUpper().Contains(filtro.ToUpper()) || x.marca.descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listArticulo;
            }

            dgvArticulo.DataSource = null;
            dgvArticulo.DataSource = listaFiltrada;

        }

        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Articulo nuevo = new Articulo();
            Detalle agregar = new Detalle(nuevo, true);
            agregar.ShowDialog();
            CargaDatos();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            frmCategorias cat = new frmCategorias(false);
            cat.ShowDialog();
            CargaDatos();
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            frmCategorias mar = new frmCategorias(true);
            mar.ShowDialog();
            CargaDatos();
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
        }

        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el campo para filtrar.");
                return true;
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el criterio para filtrar.");
                return true;
            }
            if (cboCampo.SelectedItem.ToString() == "Número")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Debes cargar el filtro para numéricos...");
                    return true;
                }
                if (!(soloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Solo nros para filtrar por un campo numérico...");
                    return true;
                }

            }

            return false;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }

        private void btnFiltro_Click_1(object sender, EventArgs e)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                    return;

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvArticulo.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {

            ImagenNegocio imgNeg = new ImagenNegocio();
            Articulo aux = new Articulo();
            aux = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            img = 0;
            listaImganes = imgNeg.listar(aux.ID);
            if (listaImganes.Count > 0)
            {
                cargarImagen(listaImganes[0].url);


            }
            else
            {
                cargarImagen("");
            }
            if (listaImganes.Count >= 2)
            {
                btnSiguiente.Visible = true;
            }
            else
            {
                btnSiguiente.Visible = false;

            }


        }

        private void btnAgregarImg_Click(object sender, EventArgs e)
        {
            Articulo aux = new Articulo();
            aux = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            img fr = new img(aux);
            fr.ShowDialog();
        }



        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            int largo = listaImganes.Count();
            
            img = img + 1;
            if (img < largo)
            {
                cargarImagen(listaImganes[img].url);
            }
            else
            {
                MessageBox.Show("EL ARTICULO NO POSEE MAS IMAGENES CARGADAS");
                return;
            }

        }
    }
}

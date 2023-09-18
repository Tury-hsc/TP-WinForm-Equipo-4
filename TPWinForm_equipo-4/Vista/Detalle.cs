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
    public partial class Detalle : Form
    {
        private MarcaNegocio MarNeg = new MarcaNegocio();
        private ArticuloNegocio ArtNeg =    new ArticuloNegocio();
        private CategoriaNegocio CatNeg = new CategoriaNegocio();
        public Detalle()
        {
            InitializeComponent();
        }

        public Detalle(Articulo aux, bool mod)
        {
            InitializeComponent();
            if (mod == false)
            {
                btnGuardar.Visible = false;
            CargaDatos(aux);
            }
            else
            {
                btnmodificar.Visible = false;
            }
        }

        private void Detalle_Load(object sender, EventArgs e)
        {
            cbxMarca.DataSource = MarNeg.listar();
            cbxMarca.ValueMember = "Id";
            cbxMarca.DisplayMember = "Descripcion";

            cbxCategoria.DataSource = CatNeg.listar();
            cbxCategoria.ValueMember = "ID";
            cbxCategoria.DisplayMember = "Descripcion";

        }

        private void CargaDatos(Articulo aux)
        {
            
            txtCodigo.Text = aux.codigo.ToString();
            txtNombre.Text = aux.nombre.ToString();
            txtprecio.Text = aux.precio.ToString();
            txtDescripcion.Text = aux.descripcion.ToString();             
            txtid.Text = aux.ID.ToString();
            cbxMarca.DataSource = MarNeg.listar();
            cbxMarca.ValueMember = "Id";
            cbxMarca.DisplayMember = "Descripcion";

            cbxCategoria.DataSource = CatNeg.listar();
            cbxCategoria.ValueMember = "ID";
            cbxCategoria.DisplayMember = "Descripcion";

        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {   
            
            
            Articulo aux = new Articulo();
            aux.codigo = txtCodigo.Text;
            aux.nombre = txtNombre.Text;
            aux.precio = Convert.ToDecimal(txtprecio.Text);
            aux.descripcion = txtDescripcion.Text;
            aux.ID = Convert.ToInt32(txtid.Text);
            aux.marca = (Marca)cbxMarca.SelectedItem;
            aux.marca.ID =Convert.ToInt32(cbxMarca.SelectedValue);
            aux.categoria = (Categoria)cbxCategoria.SelectedItem;
            aux.categoria.ID = Convert.ToInt32(cbxCategoria.SelectedValue);

            ArtNeg.Modificar(aux);
            MessageBox.Show("Modificado exitosamente");
            Close();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Articulo aux = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    MessageBox.Show("Ingrese Codigo");
                    txtCodigo.Select();
                    return;
                }
                else
                {
                aux.codigo = txtCodigo.Text;

                }
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese Nombre");
                    txtNombre.Select();
                    return;
                }
                else
                {
                    aux.nombre = txtCodigo.Text;

                }
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show("Ingrese Descripcion");
                    txtDescripcion.Select();
                    return;
                }
                else
                {
                    aux.descripcion = txtDescripcion.Text;

                }
                if (string.IsNullOrEmpty(txtprecio.Text))
                {
                    MessageBox.Show("Ingrese Precio");
                    txtprecio.Select();
                    return;
                }
                else
                {
                    aux.precio = decimal.Parse(txtprecio.Text);

                }


                aux.marca = (Marca)cbxMarca.SelectedItem;
                aux.categoria = (Categoria)cbxCategoria.SelectedItem;
                

                //falta imagen 
                //aux.imagenURL;

                negocio.agregar(aux);
                MessageBox.Show(" AGREGADO ");
                Close ();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}

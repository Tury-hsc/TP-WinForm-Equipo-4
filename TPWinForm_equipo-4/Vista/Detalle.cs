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
        public Detalle()
        {
            InitializeComponent();
        }

        public Detalle(Articulo aux)
        {
            InitializeComponent();
            CargaDatos(aux);
        }

        private void Detalle_Load(object sender, EventArgs e)
        {

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
           
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {   
            Articulo aux = new Articulo();
            aux.codigo = txtCodigo.Text;
            aux.nombre = txtNombre.Text;
            aux.precio = Convert.ToDecimal(txtprecio.Text);
            aux.descripcion = txtDescripcion.Text;
            aux.ID = Convert.ToInt32(txtid.Text);

            ArtNeg.Modificar(aux);
            MessageBox.Show("Modificado exitosamente");
            Close();

        }
    }
}

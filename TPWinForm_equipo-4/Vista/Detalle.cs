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
           txtMarca.Text = aux.marca.descripcion.ToString();
        }

    }
}

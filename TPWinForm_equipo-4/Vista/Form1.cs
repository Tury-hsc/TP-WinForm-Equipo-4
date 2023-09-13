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
            ArticuloNegocio art = new ArticuloNegocio();
            listArticulo = art.listar();
            dgvArticulo.DataSource = listArticulo;
            dgvArticulo.Columns["imagenURL"].Visible = false;   // OCULTA LA COLUMNA.
            cargarImagen(listArticulo[0].imagenURL);
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
        
    }
}

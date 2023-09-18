using Dominio;
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

namespace Vista
{
    public partial class img : Form
    {
        ImagenNegocio imgNeg = new ImagenNegocio();

        public img()
        {
            InitializeComponent();
        }

        public img(Articulo aux)
        {
            InitializeComponent();
            txtID.Text = aux.ID.ToString();
            txtNombre.Text = aux.nombre;
            txtID.ReadOnly = true;
            txtNombre.ReadOnly = true;
        }

        private void Imagen_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtimg.Text))
            {
                MessageBox.Show("AGREGUE URL");
                return;
            }
            else
            {
                imgNeg.AgregarImagen(txtimg.Text, Convert.ToInt32(txtID.Text));
                MessageBox.Show("IMAGEN AGREGADA");
                this.Close();
            }
        }
    }
}

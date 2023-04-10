using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winform_Disco
{
    public partial class frmSubirDisco : Form
    {
        public frmSubirDisco()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Disco disco= new Disco();
            DiscosNegocio negocio = new DiscosNegocio();
            try
            {
                disco.Titulo = txtbTitulo.Text;

                disco.FechaLanzamiento = dtpFechaLanzamiento.Value;
                disco.UrlImagen = txtUrlImagen.Text;
                disco.CantidadCanciones = int.Parse(txtbCantidadCanciones.Text);
                disco.Estilo = (Estilo)cboEstilo.SelectedItem;
                disco.Edicion = (Edicion)cboEdicion.SelectedItem;

                negocio.agregar(disco);
                MessageBox.Show("Agregado exitosamente");

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSubirDisco_Load(object sender, EventArgs e)
        {
            EdicionNegocio edicionNegocio = new EdicionNegocio();
            EstiloNegocio estiloNegocio = new EstiloNegocio();
            try
            {
                cboEdicion.DataSource = edicionNegocio.listar();
                cboEstilo.DataSource = estiloNegocio.listar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        public void cargarImagen(string Imagen)
        {
            try
            {
                pbxUrlImagen.Load(Imagen);

            }
            catch (Exception)
            {

                pbxUrlImagen.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }

        }
    }
}

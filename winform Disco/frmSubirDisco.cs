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
        private Disco disco = null;
        public frmSubirDisco()
        {
            InitializeComponent();
        }
        public frmSubirDisco(Disco disco)
        {
            InitializeComponent();
            this.disco = disco;
            Text = "Modificar Disco";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DiscosNegocio negocio = new DiscosNegocio();
            try
            {
                if(disco == null)
                    disco = new Disco();

                disco.Titulo = txtbTitulo.Text;
                disco.FechaLanzamiento = dtpFechaLanzamiento.Value;
                disco.UrlImagen = txtUrlImagen.Text;
                disco.CantidadCanciones = int.Parse(txtbCantidadCanciones.Text);
                disco.Estilo = (Estilo)cboEstilo.SelectedItem;
                disco.Edicion = (Edicion)cboEdicion.SelectedItem;

                if(disco.Id != 0)
                {
                    negocio.Modificar(disco);
                    MessageBox.Show("Modificado exitosamente");

                }
                else
                {
                    negocio.agregar(disco);
                    MessageBox.Show("Agregado exitosamente");

                }

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
                cboEdicion.ValueMember = "Id";
                cboEdicion.DisplayMember = "Descripcion";
                cboEstilo.DataSource = estiloNegocio.listar();
                cboEstilo.ValueMember = "Id";
                cboEstilo.DisplayMember = "Descripcion";

                if(disco != null)
                {
                    txtbTitulo.Text = disco.Titulo;
                    dtpFechaLanzamiento.Value = disco.FechaLanzamiento;
                    txtbCantidadCanciones.Text = disco.CantidadCanciones.ToString();
                    txtUrlImagen.Text = disco.UrlImagen;
                    cboEstilo.SelectedValue = disco.Estilo.Id;
                    cboEdicion.SelectedValue = disco.Edicion.Id;
                    cargarImagen(disco.UrlImagen);
                }

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

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
                disco.CantidadCanciones = int.Parse(txtbCantidadCanciones.Text);

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
    }
}

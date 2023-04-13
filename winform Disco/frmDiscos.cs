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
using dominio;
namespace winform_Disco
{
    public partial class FrmDiscos : Form
    {
        List<Disco> lista = new List<Disco>();
        public FrmDiscos()
        {
            InitializeComponent();
        }

        private void FrmDiscos_Load(object sender, EventArgs e)
        {
            cargar();

        }

        private void cargar()
        {
            DiscosNegocio disco = new DiscosNegocio();
            try
            {
                lista = disco.listar();
                dgvDiscos.DataSource = lista;
                dgvDiscos.Columns["UrlImagen"].Visible = false;
                dgvDiscos.Columns["Id"].Visible = false;
                cargarImagen(lista[0].UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Disco seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;

            cargarImagen(seleccionado.UrlImagen);
        }

        public void cargarImagen(string Imagen)
        {
            try
            {
                pbImagenDiscco.Load(Imagen);

            }
            catch (Exception)
            {

                pbImagenDiscco.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmSubirDisco subir = new frmSubirDisco();
            subir.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Disco seleccionado;
            seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;

            frmSubirDisco subir = new frmSubirDisco(seleccionado);
            subir.ShowDialog();
            cargar(); // Para Actualizar el dgvDiscos
        }
    }
}

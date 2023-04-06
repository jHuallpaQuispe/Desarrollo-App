using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimeraAppDiscos
{
    public partial class frmDiscos1 : Form
    {
        private List<Disco> listaDisco = new List<Disco>();
        public frmDiscos1()
        {
            InitializeComponent();
        }
        private void frmDiscos1_Load(object sender, EventArgs e)
        {
            DiscoServer server = new DiscoServer();
            listaDisco = server.listar();
            dgvDisco.DataSource = listaDisco;

            dgvDisco.Columns["UrlImagenTapa"].Visible= false;

            CargarImagen(listaDisco[0].UrlImagenTapa);
        }

        private void dgvDisco_SelectionChanged(object sender, EventArgs e)
        {

            Disco Seleccionado = (Disco)dgvDisco.CurrentRow.DataBoundItem;

            CargarImagen(Seleccionado.UrlImagenTapa);
        }

        private void CargarImagen(string imagen)
        {
            try
            {
                pbxDisco.Load(imagen);
                
            }
            catch (Exception ex)
            {
                pbxDisco.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }
        }
    }
}

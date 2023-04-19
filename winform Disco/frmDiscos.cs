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

            cboCampo.Items.Add("Título");
            cboCampo.Items.Add("Cantidad de Canciones");
            cboCampo.Items.Add("Estilo");
            cboCampo.Items.Add("Edición");


        }

        private void cargar()
        {
            DiscosNegocio disco = new DiscosNegocio();
            try
            {
                lista = disco.listar();
                dgvDiscos.DataSource = lista;
                ocultarColumnas();
                cargarImagen(lista[0].UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void ocultarColumnas()
        {
            dgvDiscos.Columns["UrlImagen"].Visible = false;
            dgvDiscos.Columns["Id"].Visible = false;
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            // Me da un error acá porque al momento de filtrar la "", el index no apunta a nada, osea un null
            if(dgvDiscos.CurrentRow != null)
            {
                Disco seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
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

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {

            eliminar();
        }

        private void btnEliminarLogica_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        private void eliminar (bool logica = false)
        {
            // para no repetir lo mismo en las dos eliminaciones
            DiscosNegocio negocio = new DiscosNegocio();
            Disco seleccionado;

            try
            {
                DialogResult resultado = MessageBox.Show("¿Seguro quieres eliminarlo?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;

                    if (logica)
                        negocio.eliminarLogico(seleccionado.Id);
                    else
                        negocio.eliminar(seleccionado.Id);

                    cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private bool validarFiltro()
        {
            
            if(cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un campo..");
                return true;
            }
            if(cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un criterio..");
                return true;
            }
            if(cboCampo.SelectedItem.ToString() == "Cantidad de Canciones")
            {
                if (! (soloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Ingrese solo números para Cantidad de Canciones en el filtro...");
                    return true;

                }
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Esta vacío el filtro...");
                    return true;
                }
            }
            return false;
        }
        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter))) // si no es letra, retorna false
                    return false;
            }
            return true;
        }
        private void btnFiltro_Click(object sender, EventArgs e)
        {
            DiscosNegocio negocio = new DiscosNegocio();

            try
            {
                if (validarFiltro())
                    return;

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

                dgvDiscos.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }



            //Por el momento esto no lo usaré

            /*List<Disco> listraFiltrada;
            string filtro = txtFiltro.Text;

            if(filtro != "")
            {
                // Filtrado por más que se encuentre un solo caractér
                // tambien, según Edicion y Estilo.
                listraFiltrada = lista.FindAll( x => x.Titulo.ToLower().Contains(filtro.ToLower()) || x.Edicion.Descripcion.ToLower().Contains(filtro.ToLower()) || x.Estilo.Descripcion.ToLower().Contains(filtro.ToLower()) );
            }
            else
            {
                listraFiltrada = lista;
            }

            dgvDiscos.DataSource = null; // Lo limpiamos
            dgvDiscos.DataSource = listraFiltrada;
            ocultarColumnas();*/
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Disco> listraFiltrada;
            string filtro = txtFiltro.Text;

            if(filtro.Length >= 2) // Filtras si hay más de 2 caracteres
            {
                listraFiltrada = lista.FindAll( x => x.Titulo.ToLower().Contains(filtro.ToLower()) || x.Edicion.Descripcion.ToLower().Contains(filtro.ToLower()) || x.Estilo.Descripcion.ToLower().Contains(filtro.ToLower()) );
            }
            else
            {
                listraFiltrada = lista;
            }

            dgvDiscos.DataSource = null; // Lo limpiamos
            dgvDiscos.DataSource = listraFiltrada;
            ocultarColumnas();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccionado = cboCampo.SelectedItem.ToString();

            if(seleccionado == "Cantidad de Canciones")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Contiene");
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
            }

        }
    }
}

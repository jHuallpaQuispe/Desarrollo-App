﻿using negocio;
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

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            List<Disco> listraFiltrada;
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
            ocultarColumnas();
        }
    }
}

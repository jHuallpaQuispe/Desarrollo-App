using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace PrimeraAppDiscos
{
    internal class DiscoServer
    {
		public List<Disco> lista = new List<Disco>();
		public List<Disco> listar()
		{
			SqlConnection conexion = new SqlConnection();
			SqlCommand comando = new SqlCommand();
			SqlDataReader lector;
			try
			{
				conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";
				comando.CommandType = System.Data.CommandType.Text;
				comando.CommandText = "Select Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, e.Descripcion Estilo, T.Descripcion Edicion From DISCOS D , ESTILOS E, TIPOSEDICION T Where E.Id = D.IdEstilo And T.Id = D.IdTipoEdicion";
				comando.Connection = conexion;

				conexion.Open();
				lector = comando.ExecuteReader();

				while (lector.Read())
				{
					Disco aux = new Disco();
					aux.Titulo = (string)lector["Titulo"];
					aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
					aux.CantidadCanciones = (int)lector["CantidadCanciones"];
					aux.UrlImagenTapa = (string)lector["urlImagenTapa"];
					aux.Estilo = new Estilo();
					aux.Estilo.Descripcion = (string)lector["Estilo"];
					aux.Edicion= new Edicion();
					aux.Edicion.Descripcion = (string)lector["Edicion"];

					lista.Add(aux);
				}

				conexion.Close();

                return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}

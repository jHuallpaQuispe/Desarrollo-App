using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
namespace negocio
{
    public class EdicionNegocio
    {
        public List<Edicion> listar()
        {
			AcessoDatos datos = new AcessoDatos();
			List<Edicion> lista = new List<Edicion>();
			try
			{
				datos.setearConsulta("Select Id, Descripcion From TIPOSEDICION");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
					Edicion aux = new Edicion();

					aux.Id = (int)datos.Lector["Id"];
					aux.Descripcion = (string)datos.Lector["Descripcion"];

					lista.Add(aux);
				}
				return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.cerrarConexion();
			}
        }
    }
}

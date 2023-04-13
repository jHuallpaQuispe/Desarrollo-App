using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
namespace negocio
{
    public class DiscosNegocio
    {
       public List<Disco> listar()
       {
            List<Disco> lista = new List<Disco>();
            AcessoDatos dato = new AcessoDatos();
            try
            {
                dato.setearConsulta("Select D.Titulo, D.FechaLanzamiento, D.CantidadCanciones, D.UrlImagenTapa, E.Descripcion Estilo, T.Descripcion Edicion, D.IdEstilo , D.IdTipoEdicion, D.Id From Discos D, ESTILOS E, TIPOSEDICION T where D.IdEstilo = E.Id and D.IdTipoEdicion = T.Id And Activo = 1");
                dato.ejecutarLectura();

                while (dato.Lector.Read())
                {
                    Disco aux = new Disco();

                    aux.Id = (int)dato.Lector["Id"];
                    aux.Titulo = (string)dato.Lector["Titulo"];

                    aux.FechaLanzamiento = (DateTime)dato.Lector["FechaLanzamiento"];

                    aux.CantidadCanciones = (int)dato.Lector["CantidadCanciones"];

                    //Validacion de una columna null
                    if (!(dato.Lector["UrlImagenTapa"] is DBNull))
                        aux.UrlImagen = (string)dato.Lector["UrlImagenTapa"];

                    aux.Estilo = new Estilo();
                    aux.Estilo.Id = (int)dato.Lector["IdEstilo"];
                    aux.Estilo.Descripcion = (string)dato.Lector["Estilo"];

                    aux.Edicion = new Edicion();
                    aux.Edicion.Id = (int)dato.Lector["IdTipoEdicion"];
                    aux.Edicion.Descripcion = (string)dato.Lector["Edicion"];
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
                dato.cerrarConexion();
            }

       }

         public void agregar(Disco nuevo)
        {
            AcessoDatos datos = new AcessoDatos();

            try
            {
                datos.setearConsulta("insert into DISCOS(Titulo, FechaLanzamiento, CantidadCanciones, IdEstilo, IdTipoEdicion, UrlImagenTapa) values('" + nuevo.Titulo+"',@fechaLanzamiento,"+nuevo.CantidadCanciones+", @idEstilo, @idEdicion,@urlImagen)");
                datos.seterarParametros("@fechaLanzamiento", nuevo.FechaLanzamiento);
                datos.seterarParametros("@idEstilo", nuevo.Estilo.Id);
                datos.seterarParametros("@idEdicion", nuevo.Edicion.Id);
                datos.seterarParametros("@UrlImagen", nuevo.UrlImagen);

                datos.ejecutarAccion();

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

        public void Modificar(Disco disc)
        {
            AcessoDatos datos = new AcessoDatos();
            try
            {
                datos.setearConsulta("update DISCOS set Titulo = @titulo, FechaLanzamiento = @fechaLan, CantidadCanciones = @cantCanciones, UrlImagenTapa = @urlImg, IdEstilo = @idEstilo, IdTipoEdicion = @idEdicion where Id = @id");
                datos.seterarParametros("@titulo",disc.Titulo );
                datos.seterarParametros("@fechaLan", disc.FechaLanzamiento);
                datos.seterarParametros("@cantCanciones", disc.CantidadCanciones);
                datos.seterarParametros("@urlImg", disc.UrlImagen);
                datos.seterarParametros("@idEstilo", disc.Estilo.Id);
                datos.seterarParametros("@idEdicion", disc.Edicion.Id);
                datos.seterarParametros("@id", disc.Id);

                datos.ejecutarAccion();

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

        public void eliminar(int id)
        {
            AcessoDatos datos = new AcessoDatos();
            try
            {
                datos.setearConsulta("delete from DISCOS where id = @id");
                datos.seterarParametros("@id",id);
                datos.ejecutarAccion();
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
        public void eliminarLogico(int id)
        {
            AcessoDatos datos = new AcessoDatos();
            try
            {
                datos.setearConsulta("update DISCOS set Activo = 0 where id = @id");
                datos.seterarParametros("@id", id);
                datos.ejecutarAccion();
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

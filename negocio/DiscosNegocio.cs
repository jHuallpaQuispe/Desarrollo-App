using System;
using System.Collections;
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

                lista = listarLista(dato);

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
        public List<Disco> listarLista(AcessoDatos dato)
        {
            List<Disco> lista = new List<Disco>();
            try
            {
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
        }

         public void agregar(Disco nuevo)
        {
            AcessoDatos datos = new AcessoDatos();

            try
            {
                datos.setearConsulta("insert into DISCOS(Titulo, FechaLanzamiento, CantidadCanciones, IdEstilo, IdTipoEdicion, UrlImagenTapa, Activo) values('" + nuevo.Titulo+"',@fechaLanzamiento,"+nuevo.CantidadCanciones+", @idEstilo, @idEdicion,@urlImagen,1)");
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

        public List<Disco> filtrar(string campo, string criterio, string filtro)
        {
            List<Disco> lista = new List<Disco>();
            AcessoDatos dato = new AcessoDatos();

            string consulta = "Select D.Titulo, D.FechaLanzamiento, D.CantidadCanciones, D.UrlImagenTapa, E.Descripcion Estilo, T.Descripcion Edicion, D.IdEstilo , D.IdTipoEdicion, D.Id From Discos D, ESTILOS E, TIPOSEDICION T where D.IdEstilo = E.Id and D.IdTipoEdicion = T.Id And Activo = 1 And ";

            try
            {
                if(campo == "Título")
                {
                    switch (criterio)
                    {
                        case "Contiene":
                            consulta += "D.Titulo like '%" + filtro + "%'";
                            break;
                        case "Comienza con":
                            consulta += "D.Titulo like '" + filtro + "%'";
                            break;
                        default:
                            consulta += "D.Titulo like '%" + filtro + "'";
                            break;
                    }
                }
                if (campo == "Cantidad de Canciones")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "D.CantidadCanciones > " + filtro ;
                            break;
                        case "Menor a":
                            consulta += "D.CantidadCanciones < " + filtro;
                            break;
                        default:
                            consulta += "D.CantidadCanciones = " + filtro;
                            break;
                    }
                }
                if (campo == "Estilo")
                {
                    switch (criterio)
                    {
                        case "Contiene":
                            consulta += "E.Descripcion like '%" + filtro + "%'";
                            break;
                        case "Comienza con":
                            consulta += "E.Descripcion like '" + filtro + "%'";
                            break;
                        default:
                            consulta += "E.Descripcion like '%" + filtro + "'";
                            break;
                    }
                }
                if (campo == "Edición")
                {
                    switch (criterio)
                    {
                        case "Contiene":
                            consulta += "T.Descripcion like '%" + filtro + "%'";
                            break;
                        case "Comienza con":
                            consulta += "T.Descripcion like '" + filtro + "%'";
                            break;
                        default:
                            consulta += "T.Descripcion like '%" + filtro + "'";
                            break;
                    }
                }


                dato.setearConsulta(consulta);
                dato.ejecutarLectura();

                lista = listarLista(dato);

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();

            datos.setConsulta("select id,Descripcion from MARCAS");
            datos.ejecutarLectura();
            while (datos.Lector.Read())
            {
                Marca aux = new Marca();
                aux.ID = (int)datos.Lector["Id"];
                aux.descripcion = (string)datos.Lector["Descripcion"];

                lista.Add(aux);
            }

            datos.cerrarConexion();
            return lista;
        }

        public void agregar(Marca marca)
        {
            try
            {
                datos.setConsulta("INSERT INTO MARCAS(Descripcion) VALUES(@MARCA)");
                datos.setParametro("@MARCA", marca.descripcion);
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
            try
            {
                datos.setConsulta("DELETE FROM MARCAS WHERE ID = @ID");
                datos.setParametro("@ID", id);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace Negocio
{
    public class ImagenNegocio
    {
        AccesoDatos datos = new AccesoDatos();

        public List<Imagen> listar(int idart)
        {
            List<Imagen> lista = new List<Imagen>();

            try
            {
                datos.setConsulta("SELECT * from imagenes where IdArticulo = @idart ");
                datos.setParametro("idart",idart);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.ID = (int)datos.Lector["ID"];
                    aux.idarticulo = (int)datos.Lector["IdArticulo"];
                    aux.url = (string)datos.Lector["ImagenUrl"];

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

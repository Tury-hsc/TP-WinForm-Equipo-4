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
            Marca aux = new Marca();
            datos.setConsulta("select * from MARCAS");
            datos.ejecutarLectura();
            while (datos.Lector.Read())
            {
                aux.ID = (int)datos.Lector["Id"];
                aux.descripcion = (string)datos.Lector["Descripcion"];

                lista.Add(aux);
            }
            
            datos.cerrarConexion();
            return lista;
        }



    }
}

using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;

namespace Negocio
{
    public class ArticuloNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        List<Articulo> lista = new List<Articulo>();
        public List<Articulo> listar()
        {

            try
            {
                string consulta = "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, a.precio, a.IdMarca, a.IdCategoria, m.Descripcion Marca, c.Descripcion Categoria from ARTICULOS A " +
                    "Inner Join Marcas as m on A.IdMarca = M.Id " +
                    "Inner Join CATEGORIAS as c on A.IdCategoria = C.Id";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.ID = (int)datos.Lector["Id"];
                    aux.codigo = (string)datos.Lector["Codigo"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];

                    // MARCA 
                    aux.marca = new Marca();
                    aux.marca.ID = (int)datos.Lector["IdMarca"];
                    aux.marca.descripcion = (string)datos.Lector["MARCA"];

                    //aux.categoria =
                    aux.categoria = new Categoria();
                    aux.categoria.ID = (int)datos.Lector["IdCategoria"];
                    aux.categoria.descripcion = (string)datos.Lector["CATEGORIA"];

                    aux.precio = (decimal)datos.Lector["Precio"];


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
        //Elimina fisicamente un articulo en la base de datos
        public void Eliminar(int id)
        {
            datos.setConsulta("Delete from Articulos where id = @id");
            datos.setParametro("id", id);
            datos.ejecutarLectura();
            datos.comando.Parameters.Clear();
            datos.cerrarConexion();

        }

        public List<int> ListarMarcasActivas()
        {
            List<int> listaCat = new List<int>();
            datos.setConsulta("select distinct a.IdMarca from ARTICULOS a");
            datos.ejecutarLectura();
            while (datos.Lector.Read())
            {
                int aux = new int();
                aux = (int)datos.Lector["IdMarca"];
                listaCat.Add(aux);

            }
            datos.cerrarConexion();
            return listaCat;
        }

        public List<int> ListarCategoriasActivas()
        {
            List<int> listaCat = new List<int>();
            datos.setConsulta("select distinct a.IdCategoria from ARTICULOS a");
            datos.ejecutarLectura();
            while (datos.Lector.Read())
            {
                int aux = new int();
                aux = (int)datos.Lector["IdCategoria"];
                listaCat.Add(aux);

            }
            datos.cerrarConexion();
            return listaCat;
        }



        public void Modificar(Articulo art)
        {
            datos.setConsulta("update ARTICULOS set nombre = @nombre, codigo=@codigo, precio=@precio, descripcion=@descripcion, idMarca=@IdMarca,IdCategoria=@IdCategoria where id=@id");
            datos.setParametro("nombre", art.nombre);
            datos.setParametro("codigo", art.codigo);
            datos.setParametro("precio", art.precio);
            datos.setParametro("descripcion", art.descripcion);
            datos.setParametro("IdMarca", art.marca.ID);
            datos.setParametro("IdCategoria", art.categoria.ID);
            //datos.setParametro("urlimagen", art.imagenURL);
            datos.setParametro("id", art.ID);
           
            datos.ejecutarAccion();
            datos.cerrarConexion();


        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("insert into ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES(@codigo,@nombre,@descripcion,@idMarca,@idCategoria,@precio)");

                datos.setParametro("@codigo", nuevo.codigo);
                datos.setParametro("@nombre", nuevo.nombre);
                datos.setParametro("@descripcion", nuevo.descripcion);
                datos.setParametro("@idMarca", nuevo.marca.ID);
                datos.setParametro("@idCategoria", nuevo.categoria.ID);
                datos.setParametro("@precio", nuevo.precio);

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

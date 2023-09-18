﻿using Dominio;
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
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion Marca, A.IdCategoria, C.Descripcion Categoria, A.Precio, I.Id, I.IdArticulo, I.ImagenUrl  FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id INNER JOIN CATEGORIAS AS C ON A.IdCategoria = C.Id LEFT JOIN IMAGENES AS I ON A.Id = I.IdArticulo";

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

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.imagenURL = (string)datos.Lector["ImagenUrl"];
                    }

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

          public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion Marca, A.IdCategoria, C.Descripcion Categoria, A.Precio, I.Id, I.IdArticulo, I.ImagenUrl  FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id INNER JOIN CATEGORIAS AS C ON A.IdCategoria = C.Id LEFT JOIN IMAGENES AS I ON A.Id = I.IdArticulo  Where ";
                if(campo == "Número")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.Id > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "A.Id < " + filtro;
                            break;
                        default:
                            consulta += "A.Id = " + filtro;
                            break;
                    }
                }
                else if(campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A.Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

           

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.ID = (int)datos.Lector["Id"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.imagenURL = (string)datos.Lector["ImagenUrl"];

                    aux.marca = new Marca();
                    aux.marca.ID = (int)datos.Lector["IdMarca"];
                    aux.marca.descripcion = (string)datos.Lector["Descripcion"];
                    aux.categoria = new Categoria();
                    aux.categoria.ID = (int)datos.Lector["IdCategoria"];
                    aux.categoria.descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

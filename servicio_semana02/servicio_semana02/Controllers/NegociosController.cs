using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using servicio_semana02.Models;
using System.Data;
using System.Data.SqlClient;

namespace servicio_semana02.Controllers
{
    public class NegociosController : Controller
    {
        //defininiendo un metodo para listar pedidos
        IEnumerable<Pedido> listado()
        {
            List<Pedido> temporal = new List<Pedido>();
            //conexion
            using (SqlConnection cn = new SqlConnection(
                "server=.;database=Negocios2019;uid=sa;pwd=sql"))
            {
                //ejecutar el procedure
                SqlCommand cmd = new SqlCommand("sp_pedidos", cn);
                cn.Open();
                //ejecutar como lector
                SqlDataReader dr = cmd.ExecuteReader();
                //mientras se pueda leer
                while (dr.Read()){
                    Pedido reg = new Pedido()
                    {
                        idpedido = dr.GetInt32(0),
                        fechapedido = dr.GetDateTime(1),
                        nombrecia = dr.GetString(2),
                        ciudad = dr.GetString(4)
                    };
                    temporal.Add(reg);
                }
                dr.Close();
                cn.Close();
            }
                return temporal;
        }
        public ActionResult listadopedidos()
        {

            return View(listado());
        }


        public ActionResult pedidospaginacion(int p = 0)
        {
            //conocer la cantidad de registros
            int c = listado().Count();
            //saber cuantos registros se visualiza por pagina
            int numreg = 15;
            //calcular la cantidad de paginas
            int numpag = c % numreg > 0 ? (c / numreg + 1) : c / numreg;
            //almaceno numpag en un ViewBag
            ViewBag.numpag = numpag;
            //filtrar segun el numero de pagina
            IEnumerable<Pedido>temporal = listado().Skip(numreg * p).Take(numreg);
            return View(temporal);
        }

        //definir el metodo para listar pedidos por año
        IEnumerable<Pedido>listadoyear(int y)
        {
            List<Pedido> temporal = new List<Pedido>();
            using(SqlConnection cn=new SqlConnection(
                "server=.;database=Negocios2019;uid=sa;pwd=sql"))
            {
                SqlCommand cmd = new SqlCommand("sp_pedidos_year", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@y", y);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) {
                    Pedido reg = new Pedido()
                    {
                        idpedido = dr.GetInt32(0),
                        fechapedido = dr.GetDateTime(1),
                        nombrecia = dr.GetString(2),
                        direccion = dr.GetString(3),
                        ciudad = dr.GetString(4)
                    };
                    temporal.Add(reg);
                }
                dr.Close();cn.Close();
            }
            return temporal;
        }

        public ActionResult pedidosyear(int y = 0)
        {
            //guradar el contenido de y
            ViewBag.y = y;
            //enviar a la vista los pedidos ya filtrados
            return View(listadoyear(y));
        }

        public ActionResult pedidosyearpaginacion(int y=0,int p = 0)
        {
            //ejecutamos el metodo
            IEnumerable<Pedido> temporal = listadoyear(y);
            //contador de registros
            int c = temporal.Count();
            int numreg = 10;

            int numpag = c % numreg > 0 ? (c / numreg + 1) : c / numreg;
            ViewBag.numpag = numpag;
            ViewBag.y = y;
            return View(temporal.Skip(p * numreg).Take(numreg));
        }
    }
}
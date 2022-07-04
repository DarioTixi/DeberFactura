using FacturaWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturaWeb.Controllers
{
    public class InicioController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public string ListarFacturas() {

            Respuesta respuesta = new Respuesta();
            List<Factura> facturas = new List<Factura>();
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("SP_ObtenerFacturas", connection);
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Factura factura = new Factura();
                        factura.Id_Venta = reader.GetInt32(0);
                        factura.Fecha = reader.GetDateTime(1).ToString();
                        factura.Cedula = reader.GetString(2);
                        factura.Cliente = reader.GetString(3);
                        factura.Subtotal = reader.GetDecimal(4).ToString();
                        factura.Iva = reader.GetDecimal(5).ToString();
                        factura.Total = reader.GetDecimal(6).ToString();
                        facturas.Add(factura);
                    }
                }
                respuesta.Datos = facturas;
                return JsonConvert.SerializeObject(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Error = ex.Message;
                return JsonConvert.SerializeObject(respuesta);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
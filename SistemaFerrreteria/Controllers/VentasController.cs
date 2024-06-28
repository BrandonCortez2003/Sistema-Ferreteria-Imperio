using SistemaFerrreteria.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaFerrreteria.Controllers
{
    public class VentasController : Controller
    {
        private bd_Ferreteria2Entities1 bd = new bd_Ferreteria2Entities1();

        // GET: Carrito
        public ActionResult Index()
        {

            List<Venta> ventas = bd.Venta.ToList();
            return View(ventas);
        }
    }
}
using SistemaFerrreteria.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaFerrreteria.Controllers
{

    public class CategoriaController : Controller
    {
        private bd_Ferreteria2Entities1 bd = new bd_Ferreteria2Entities1();

        // GET: Categoria
        public ActionResult Index()
        {
            ViewBag.ListaCategorias = bd.Categoria.ToList();

            // Obtener la lista de productos y pasarla a la vista
            var productos = bd.Producto.ToList();
            return View(productos);
        }
    }
}
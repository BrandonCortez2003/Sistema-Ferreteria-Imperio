using SistemaFerrreteria.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaFerrreteria.Controllers
{
    public class ProductoController : Controller
    {
        private bd_Ferreteria2Entities1 bd = new bd_Ferreteria2Entities1();


        public ActionResult Index(int? categoriaId, string nombre)
        {
            ViewBag.ListaCategorias = bd.Categoria.ToList();
            var productos = bd.Producto.ToList();

            if (categoriaId.HasValue)
            {
                productos = productos.Where(p => p.ID_Categoria == categoriaId).ToList();
            }
            else if (!string.IsNullOrEmpty(nombre))
            {
                productos = productos.Where(p => p.Descripcion.Contains(nombre)).ToList();
            }

            return View(productos);
        }

        public ActionResult ProductosPorCategoria(int categoriaId)
        {
            var productosPorCategoria = bd.Producto
                .Where(p => p.ID_Categoria == categoriaId && p.Stock > 0)
                .OrderBy(x => x.Descripcion)
                .ToList();

            ViewBag.ListaCategorias = bd.Categoria.ToList(); // Esto es para mantener la lista de categorías en la vista

            return View("Index", productosPorCategoria);
        }

        public ActionResult BuscarPorNombre(string nombre)
        {
            var productos = bd.Producto
                .Where(p => p.Descripcion.Contains(nombre) && p.Stock > 0)
                .OrderBy(x => x.Descripcion)
                .ToList();

            ViewBag.ListaCategorias = bd.Categoria.ToList(); // Esto es para mantener la lista de categorías en la vista

            return View("Index", productos);
        }



        public ActionResult Consulta1()
        {
            // Lógica para la Consulta 1
            return View();
        }

        public ActionResult Consulta2()
        {
            // Lógica para la Consulta 2
            return View();
        }


    }
}

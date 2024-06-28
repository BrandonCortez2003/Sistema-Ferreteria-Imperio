using SistemaFerrreteria.Datos;
using SistemaFerrreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaFerrreteria.Controllers
{
    public class CarritoController : Controller
    {
        private bd_Ferreteria2Entities1 bd = new bd_Ferreteria2Entities1();

        // GET: Carrito

        private int getIndice(int id)
        {
            List<Carrito> lista = (List<Carrito>)Session["carrito"];

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Producto.ID_Producto == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public ActionResult AgregarCarrito(int? id, int? cantidad)
        {
            if (id.HasValue && cantidad.HasValue)
            {
                Producto producto = bd.Producto.Find(id.Value);
                int cantidadSeleccionada = cantidad.Value;

                if (producto != null && producto.Stock >= cantidadSeleccionada)
                {
                    if (Session["carrito"] == null)
                    {
                        List<Carrito> lista = new List<Carrito>();
                        lista.Add(new Carrito(producto, cantidadSeleccionada));
                        Session["carrito"] = lista;
                        TempData["Mensaje"] = "Producto Agregado";
                    }
                    else
                    {
                        List<Carrito> lista = (List<Carrito>)Session["carrito"];
                        int indice = getIndice(id.Value);
                        if (indice == -1)
                        {
                            lista.Add(new Carrito(producto, cantidadSeleccionada));
                            TempData["Mensaje"] = "Producto Agregado";
                        }
                        else
                        {
                            TempData["Mensaje"] = "EL PRODUCTO YA ESTÁ EN EL CARRITO";
                        }
                        Session["carrito"] = lista;
                    }

                    // Restar la cantidad seleccionada del inventario en la base de datos
                    producto.Stock -= cantidadSeleccionada;
                    bd.SaveChanges(); // Guardar los cambios en la base de datos
                }
                else
                {
                    TempData["Mensaje"] = "No hay suficiente stock disponible";
                }
            }

            return View("AgregarCarrito");
        }

        public ActionResult Delete(int? id)
        {
            List<Carrito> lista = (List<Carrito>)Session["carrito"];
            lista.RemoveAt(getIndice(id.Value));
            TempData["Mensaje"] = "Producto Eliminado";
            return RedirectToAction("AgregarCarrito");
        }

        public ActionResult FinalizarCompra()
        {
            List<Carrito> compras = (List<Carrito>)Session["carrito"];
            if (compras != null && compras.Count > 0)
            {
                Venta nuevaVenta = new Venta();
                nuevaVenta.Fecha_de_venta = DateTime.Now;
                nuevaVenta.SubTotal = (double?)compras.Sum(x => x.Producto.Precio * x.Cantidad);
                nuevaVenta.Iva = nuevaVenta.SubTotal * 0.18;
                nuevaVenta.Total_venta = nuevaVenta.SubTotal + nuevaVenta.Iva;

                nuevaVenta.Detalle_Venta = (from Producto in compras
                                            select new Detalle_Venta
                                            {
                                                ID_Producto = Producto.Producto.ID_Producto,
                                                Cantidad = Producto.Cantidad,
                                                Precio_unitario = Producto.Producto.Precio,
                                                Total = Producto.Cantidad * Producto.Producto.Precio
                                            }).ToList();

                bd.Venta.Add(nuevaVenta);
                bd.SaveChanges();
                Session["carrito"] = new List<Carrito>();
                TempData["Mensaje"] = "Compra exitosa";
            }

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
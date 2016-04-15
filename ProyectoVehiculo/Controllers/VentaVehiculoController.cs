using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoVehiculo.Models;

namespace ProyectoVehiculo.Controllers
{
    public class VentaVehiculoController : Controller
    {
        private DB_VEHICULO db = new DB_VEHICULO();

        // GET: VentaVehiculo
        public ActionResult Index()
        {
            var ventaVehiculo = db.ventaVehiculo.Include(v => v.usuario).Include(v => v.vehiculo);
            return View(ventaVehiculo.ToList());
        }

        // GET: VentaVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaVehiculo ventaVehiculo = db.ventaVehiculo.Find(id);
            if (ventaVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(ventaVehiculo);
        }

        // GET: VentaVehiculo/Create
        public ActionResult Create(int idVehiculo)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            vehiculos.Add(db.vehiculo.Find(idVehiculo));
            ViewBag.idVehiculo = new SelectList(vehiculos, "idVehiculo", "modelo");
            List<Usuario> usuarios = new List<Usuario>();
            ViewBag.idUsuario = new SelectList(db.usuario, "idUsuario", "nombre");
            return View();
        }

        // POST: VentaVehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVentaVehiculo,fecha,idUsuario,idVehiculo")] VentaVehiculo ventaVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.ventaVehiculo.Add(ventaVehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idUsuario = new SelectList(db.usuario, "idUsuario", "nombre", ventaVehiculo.idUsuario);
            ViewBag.idVehiculo = new SelectList(db.vehiculo, "idVehiculo", "modelo", ventaVehiculo.idVehiculo);
            return View(ventaVehiculo);
        }

        // GET: VentaVehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaVehiculo ventaVehiculo = db.ventaVehiculo.Find(id);
            if (ventaVehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idUsuario = new SelectList(db.usuario, "idUsuario", "nombre", ventaVehiculo.idUsuario);
            ViewBag.idVehiculo = new SelectList(db.vehiculo, "idVehiculo", "modelo", ventaVehiculo.idVehiculo);
            return View(ventaVehiculo);
        }

        // POST: VentaVehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVentaVehiculo,fecha,idUsuario,idVehiculo")] VentaVehiculo ventaVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ventaVehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idUsuario = new SelectList(db.usuario, "idUsuario", "nombre", ventaVehiculo.idUsuario);
            ViewBag.idVehiculo = new SelectList(db.vehiculo, "idVehiculo", "modelo", ventaVehiculo.idVehiculo);
            return View(ventaVehiculo);
        }

        // GET: VentaVehiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaVehiculo ventaVehiculo = db.ventaVehiculo.Find(id);
            if (ventaVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(ventaVehiculo);
        }

        // POST: VentaVehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VentaVehiculo ventaVehiculo = db.ventaVehiculo.Find(id);
            db.ventaVehiculo.Remove(ventaVehiculo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

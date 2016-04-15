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
    public class TipoVehiculoController : Controller
    {
        private DB_VEHICULO db = new DB_VEHICULO();

        // GET: TipoVehiculo
        public ActionResult Index()
        {
            return View(db.tipoVehiculo.ToList());
        }

        // GET: TipoVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVehiculo tipoVehiculo = db.tipoVehiculo.Find(id);
            if (tipoVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoVehiculo);
        }

        // GET: TipoVehiculo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoVehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoVehiculo,tipo,descripcion")] TipoVehiculo tipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.tipoVehiculo.Add(tipoVehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoVehiculo);
        }

        // GET: TipoVehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVehiculo tipoVehiculo = db.tipoVehiculo.Find(id);
            if (tipoVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoVehiculo);
        }

        // POST: TipoVehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoVehiculo,tipo,descripcion")] TipoVehiculo tipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoVehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoVehiculo);
        }

        // GET: TipoVehiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVehiculo tipoVehiculo = db.tipoVehiculo.Find(id);
            if (tipoVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoVehiculo);
        }

        // POST: TipoVehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoVehiculo tipoVehiculo = db.tipoVehiculo.Find(id);
            db.tipoVehiculo.Remove(tipoVehiculo);
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

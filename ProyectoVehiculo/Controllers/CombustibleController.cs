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
    public class CombustibleController : Controller
    {
        private DB_VEHICULO db = new DB_VEHICULO();

        // GET: Combustible
        public ActionResult Index()
        {
            return View(db.combustible.ToList());
        }

        // GET: Combustible/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Combustible combustible = db.combustible.Find(id);
            if (combustible == null)
            {
                return HttpNotFound();
            }
            return View(combustible);
        }

        // GET: Combustible/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Combustible/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCombustible,nombre,descripcion")] Combustible combustible)
        {
            if (ModelState.IsValid)
            {
                db.combustible.Add(combustible);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(combustible);
        }

        // GET: Combustible/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Combustible combustible = db.combustible.Find(id);
            if (combustible == null)
            {
                return HttpNotFound();
            }
            return View(combustible);
        }

        // POST: Combustible/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCombustible,nombre,descripcion")] Combustible combustible)
        {
            if (ModelState.IsValid)
            {
                db.Entry(combustible).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(combustible);
        }

        // GET: Combustible/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Combustible combustible = db.combustible.Find(id);
            if (combustible == null)
            {
                return HttpNotFound();
            }
            return View(combustible);
        }

        // POST: Combustible/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Combustible combustible = db.combustible.Find(id);
            db.combustible.Remove(combustible);
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

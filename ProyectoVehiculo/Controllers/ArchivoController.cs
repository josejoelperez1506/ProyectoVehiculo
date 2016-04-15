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
    public class ArchivoController : Controller
    {
        private DB_VEHICULO db = new DB_VEHICULO();

        // GET: Archivo
        public ActionResult Index()
        {
            var archivo = db.archivo.Include(a => a.vehiculo);
            return View(archivo.ToList());
        }

        public ActionResult Oarchivo(int id)
        {
            var imagen = db.archivo.Find(id);
            return File(imagen.contenido, imagen.contentType);
        }

        // GET: Archivo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = db.archivo.Find(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // GET: Archivo/Create
        public ActionResult Create()
        {
            ViewBag.idVehiculo = new SelectList(db.vehiculo, "idVehiculo", "modelo");
            return View();
        }

        // POST: Archivo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idArchivo,nombre,contentType,tipo,contenido,idVehiculo")] Archivo archivo)
        {
            if (ModelState.IsValid)
            {
                db.archivo.Add(archivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idVehiculo = new SelectList(db.vehiculo, "idVehiculo", "modelo", archivo.idVehiculo);
            return View(archivo);
        }

        // GET: Archivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = db.archivo.Find(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idVehiculo = new SelectList(db.vehiculo, "idVehiculo", "modelo", archivo.idVehiculo);
            return View(archivo);
        }

        // POST: Archivo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idArchivo,nombre,contentType,tipo,contenido,idVehiculo")] Archivo archivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(archivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idVehiculo = new SelectList(db.vehiculo, "idVehiculo", "modelo", archivo.idVehiculo);
            return View(archivo);
        }

        // GET: Archivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = db.archivo.Find(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // POST: Archivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Archivo archivo = db.archivo.Find(id);
            db.archivo.Remove(archivo);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoVehiculo.Models;
using System.Data.Entity.Infrastructure;

namespace ProyectoVehiculo.Controllers
{
    public class VehiculoController : Controller
    {
        private DB_VEHICULO db = new DB_VEHICULO();

        // GET: Vehiculo
        public ActionResult Index(String ordenFiltro, String busqueda)
        {
            ViewBag.ordenarNombre = String.IsNullOrEmpty(ordenFiltro) ? "nom_desc" :"";
            var vehiculo = db.vehiculo.Include(v => v.combustible).Include(v => v.estado).Include(v => v.marca).Include(v => v.tipoVehiculo);
            if (!String.IsNullOrEmpty(busqueda)) {
                vehiculo = vehiculo.Where(v => v.precio.Contains(busqueda) || v.modelo.Contains(busqueda));
            }
            vehiculo = vehiculo.OrderBy(v => v.modelo);
            switch (ordenFiltro) { 
                case "nom_desc":
                    vehiculo = vehiculo.OrderByDescending(v => v.modelo);
                    break;
            }
            return View(vehiculo.ToList());
        }

        public ActionResult Listar()
        {
            var vehiculo = db.vehiculo.Include(v => v.combustible).Include(v => v.estado).Include(v => v.marca).Include(v => v.tipoVehiculo);
            return View(vehiculo.ToList());
        }

        public ActionResult Busqueda(string modelo, string validar, string tipoVehiculo) {
            var Gmodelo = new List<string>();
            var Gtodo = from v in db.vehiculo
                        orderby v.modelo
                        select v.modelo;
            Gmodelo.AddRange(Gtodo.Distinct());

            var vehiculos = from m in db.vehiculo
                            select m;

            if (!String.IsNullOrEmpty(validar)) {
                vehiculos = vehiculos.Where(v => v.modelo.Contains(validar));
            }
            if (!string.IsNullOrEmpty(modelo))
            {
                vehiculos = vehiculos.Where(x => x.modelo == modelo);
            }
            return View(vehiculos);
        }

        // GET: Vehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.vehiculo.Include(v=> v.archivos).SingleOrDefault(v=>v.idVehiculo==id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: Vehiculo/Create
        public ActionResult Create()
        {
            ViewBag.idCombustible = new SelectList(db.combustible, "idCombustible", "nombre");
            ViewBag.idEstado = new SelectList(db.estado, "idEstado", "nombre");
            ViewBag.idMarca = new SelectList(db.marca, "idMarca", "nombre");
            ViewBag.idTipoVehiculo = new SelectList(db.tipoVehiculo, "idTipoVehiculo", "tipo");
            return View();
        }

        // POST: Vehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVehiculo,idTipoVehiculo,idMarca,idCombustible,idEstado,precio,tipo,modelo")] Vehiculo vehiculo, HttpPostedFileBase archivo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(archivo != null && archivo.ContentLength > 0)
                    {
                        var imagen = new Archivo
                        {
                            nombre = System.IO.Path.GetFileName(archivo.FileName),
                            tipo = FileType.Imagen,
                            contentType = archivo.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(archivo.InputStream))
                        {
                            imagen.contenido = reader.ReadBytes(archivo.ContentLength);
                        };
                        vehiculo.archivos = new List<Archivo> { imagen };
                    }
                    db.vehiculo.Add(vehiculo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException ex) {
                ModelState.AddModelError("", "Imposible guardar información. Intente otra vez.");
            }
                    ViewBag.idCombustible = new SelectList(db.combustible, "idCombustible", "nombre", vehiculo.idCombustible);
                    ViewBag.idEstado = new SelectList(db.estado, "idEstado", "nombre", vehiculo.idEstado);
                    ViewBag.idMarca = new SelectList(db.marca, "idMarca", "nombre", vehiculo.idMarca);
                    ViewBag.idTipoVehiculo = new SelectList(db.tipoVehiculo, "idTipoVehiculo", "tipo", vehiculo.idTipoVehiculo);
                    return View(vehiculo);
        }

        // GET: Vehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.vehiculo.Include(v =>v.archivos).SingleOrDefault(v => v.idVehiculo == id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCombustible = new SelectList(db.combustible, "idCombustible", "nombre", vehiculo.idCombustible);
            ViewBag.idEstado = new SelectList(db.estado, "idEstado", "nombre", vehiculo.idEstado);
            ViewBag.idMarca = new SelectList(db.marca, "idMarca", "nombre", vehiculo.idMarca);
            ViewBag.idTipoVehiculo = new SelectList(db.tipoVehiculo, "idTipoVehiculo", "tipo", vehiculo.idTipoVehiculo);
            return View(vehiculo);
        }

        // POST: Vehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, HttpPostedFileBase archivo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehiculo = db.vehiculo.Find(id);
            if (TryUpdateModel(vehiculo, "", new string[] { "idVehiculo,idTipoVehiculo,idMarca,idCombustible,idEstado,precio,tipo,modelo" }))
            {
                try
                {
                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        if (vehiculo.archivos.Any(f => f.tipo == FileType.Imagen))
                        {
                            db.archivo.Remove(vehiculo.archivos.First(f => f.tipo == FileType.Imagen));
                        }
                        var imagen = new Archivo
                        {
                            nombre = System.IO.Path.GetFileName(archivo.FileName),
                            tipo = FileType.Imagen,
                            contentType = archivo.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(archivo.InputStream))
                        {
                            imagen.contenido = reader.ReadBytes(archivo.ContentLength);
                        }
                        vehiculo.archivos = new List<Archivo> { imagen };
                    }
                    db.Entry(vehiculo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException ex)
                {
                    ModelState.AddModelError("", "No se guardo el cambio. Intentar de nuevo, si el problema persiste, comunicarse con el Administrador");
                }
            }
            return View(vehiculo);
        }

        // GET: Vehiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculo vehiculo = db.vehiculo.Find(id);
            db.vehiculo.Remove(vehiculo);
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

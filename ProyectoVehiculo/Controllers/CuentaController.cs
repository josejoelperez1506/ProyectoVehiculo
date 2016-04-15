using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoVehiculo.Models;

namespace ProyectoVehiculo.Controllers
{
    public class CuentaController : Controller
    {
        public DB_VEHICULO db = new DB_VEHICULO();
        // GET: Cuenta
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var usr = db.usuario.FirstOrDefault(u=> u.correo==usuario.correo && u.contraseña==usuario.contraseña);
            if (usr != null)
            {
                Session["nombreUsuario"] = usr.nombre;
                Session["idUsuario"] = usr.idUsuario;
                Session["rol"] = usr.rol.idRol;
                return VerificarSesion();
            }
            else {
                ModelState.AddModelError("", "Verifique sus credenciales: Usuario o Contraseña incorrecta.");
            
            }
            return View();
        }

        // GET: Cuenta
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid) {
                var rol = db.rol.Find(2);
                usuario.idRol = rol.idRol;
                db.usuario.Add(usuario);
                db.SaveChanges();
                ViewBag.mensaje = "El usuario " + usuario.nombre + "fue registrado correctamente";
                ModelState.Clear();
            }
            return View();
        }

        public ActionResult VerificarSesion()
        {
            if (Session["IDUsuario"] != null) {
                return RedirectToAction("../Home/Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout() {
            Session.Remove("IDUsuario");
            Session.Remove("nombreUsuario");
            Session.Remove("rol");
            return RedirectToAction("Login");
        }
    }
}
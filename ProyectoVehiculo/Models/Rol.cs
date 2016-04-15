using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoVehiculo.Models
{
    public class Rol
    {
        [Key]
        public int idRol { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public String nombre { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Descripción obligatoria.")]
        public String descripcion { get; set; }

        public virtual List<Usuario> usuarios { get; set; }
    }
}
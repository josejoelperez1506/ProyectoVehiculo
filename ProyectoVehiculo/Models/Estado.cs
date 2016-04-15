using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ProyectoVehiculo.Models
{
    public class Estado
    {
        [Key]
        public int idEstado { get; set; }
        //------------
        [Display(Name = "Disponibilidad"), Required(ErrorMessage = "Nombre es obligatorio")]
        public String nombre { get; set; }

        [Display(Name = "Año de modelo"), Required(ErrorMessage = "Ingrese el año de modelo")]
        public String año { get; set; }
    }
}
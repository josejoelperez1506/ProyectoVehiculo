using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoVehiculo.Models
{
    public class VentaVehiculo
    {
        [Key]
        public int idVentaVehiculo { get; set; }
        [Display(Name = "Fecha"), Required(ErrorMessage = "Fecha Obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }

        [Display(Name = "Usuario"), Required(ErrorMessage = "Usuario Obligatorio.")]
        public int idUsuario { get; set; }
        public virtual Usuario usuario { get; set; }
        [Display(Name = "Vehiculo"), Required(ErrorMessage = "Vehiculo Obligatorio.")]
        public int idVehiculo { get; set; }

        public virtual Vehiculo vehiculo { get; set; }

    }
}
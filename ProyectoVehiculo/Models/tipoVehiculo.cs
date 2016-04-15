using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoVehiculo.Models
{
    public class TipoVehiculo
    {
        [Key]
        public int idTipoVehiculo { get; set; }
        [Display(Name = "Tipo de vehiculo")]
        public String tipo { get; set; }
        [Display(Name = "Descripción")]
        public String descripcion { get; set; }

        public virtual List<Vehiculo> vehiculos { get; set; }
    }
}
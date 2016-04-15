using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoVehiculo.Models
{
    public class Combustible
    {
        [Key]
        public int idCombustible { get; set; }
        [Display(Name = "Tipo de combustible")]
        public String nombre { get; set; }
        [Display(Name = "Descripción")]
        public String descripcion { get; set; }

        public virtual List<Vehiculo> vehiculos { get; set; }

    }
}
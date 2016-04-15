using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoVehiculo.Models
{
    public class Marca
    {
        [Key]
        public int idMarca { get; set; }
        [Display(Name = "Nombre")]
        public String nombre { get; set; }
        [Display(Name = "Descripción")]
        public String descripcion { get; set; }

        public virtual List<Vehiculo> vehiculos { get; set; }

    }
}
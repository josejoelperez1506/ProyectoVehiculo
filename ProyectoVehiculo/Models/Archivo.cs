using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoVehiculo.Models
{
    public class Archivo
    {
        [Key]
        public int idArchivo { get; set; }
        public String nombre { get; set; }
        public String contentType { get; set; }
        public FileType tipo { get; set; }
        public byte[] contenido { get; set; }

        [Display(Name="Vehiculo")]
        public int? idVehiculo { get; set; }
        public virtual Vehiculo vehiculo { get; set; }
        public virtual Usuario usuarios { get; set; }
    }
}
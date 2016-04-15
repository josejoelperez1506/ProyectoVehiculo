using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoVehiculo.Models
{
    public class Vehiculo
    {
        [Key]
        public int idVehiculo { get; set; }

        //--Heredacion
        [Display(Name = "Tipo Auto"), Required(ErrorMessage = "Tipo de auto requerido")]
        public int idTipoVehiculo{ get; set; }
        [Display(Name = "Marca"),  Required(ErrorMessage = "Marca requerida")]
        public int idMarca { get; set; }
        [Display(Name = "Tipo de Combustible"), Required(ErrorMessage = "Tipo de combustible")]
        public int idCombustible { get; set; }
        [Display(Name = "Disponibilidad"), Required(ErrorMessage = "Reservado o Disponible?")]
        public int idEstado { get; set; }

        //--
        [Display(Name = "Precio"), Required(ErrorMessage = "Precio requerido"), DataType(DataType.Text)]
        public String precio { get; set; }
        public String tipo { get; set; }
        [Display(Name = "Modelo"), Required(ErrorMessage = "Defina año del auto"), DataType(DataType.Text)]
        public String modelo { get; set; }

        public virtual List<Archivo> archivos { get; set; }
        public virtual List<VentaVehiculo> ventaVehiculo { get; set; }

        public virtual Estado estado { get; set; }
        public virtual Combustible combustible { get; set; }
        public virtual Marca marca { get; set; }
        public virtual TipoVehiculo tipoVehiculo { get; set; }
    }
}
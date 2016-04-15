using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoVehiculo.Models
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [Display(Name = "Nombre Completo"), Required(ErrorMessage = "Nombre requerido")]
        public String nombre { get; set; }

        [Display(Name = "Carne")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Carne debe de tener 7 digitos.")]
        public String carne { get; set; }

        [Display(Name = "Correo"), Required(ErrorMessage = "Correo requerido"), DataType(DataType.EmailAddress)]
        public String correo { get; set; }

        [Display(Name = "Contraseña"), Required(ErrorMessage = "Contraseña obligatoria"), DataType(DataType.Password)]
        public String contraseña { get; set; }
        [Display(Name = "Comparar Contraseña"), Required(ErrorMessage = "Contraseña Obligatoria"), DataType(DataType.Password)]
        [Compare("contraseña", ErrorMessage = "Las contraseñas no coinciden")]
        public String compararContraseña { get; set; }

        [Display(Name="Rol"), Required]
        public int idRol { get; set; }
        public virtual Rol rol { get; set; }

        public virtual List<VentaVehiculo> ventaVehiculos { get; set; }
        public virtual List<Archivo> archivos { get; set; }
    }
}
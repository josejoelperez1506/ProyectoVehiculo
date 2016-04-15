using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProyectoVehiculo.Models
{
    public partial class DB_VEHICULO:DbContext
    {
        public DB_VEHICULO() : base("name=DB_VEHICULO") { }
        public virtual DbSet<Rol> rol { get; set; }
        public virtual DbSet<Archivo> archivo { get; set; }
        public virtual DbSet<Usuario> usuario { get; set; }
        public virtual DbSet<Estado> estado { get; set; }
        public virtual DbSet<TipoVehiculo> tipoVehiculo { get; set; }
        public virtual DbSet<Marca> marca { get; set; }
        public virtual DbSet<Combustible> combustible { get; set; }
        public virtual DbSet<Vehiculo> vehiculo { get; set; }
        public virtual DbSet<VentaVehiculo> ventaVehiculo { get; set; }
    }
}
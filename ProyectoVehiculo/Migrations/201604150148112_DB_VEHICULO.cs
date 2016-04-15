namespace ProyectoVehiculo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB_VEHICULO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Archivoes",
                c => new
                    {
                        idArchivo = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        contentType = c.String(),
                        tipo = c.Int(nullable: false),
                        contenido = c.Binary(),
                        idVehiculo = c.Int(),
                        usuarios_idUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.idArchivo)
                .ForeignKey("dbo.Usuarios", t => t.usuarios_idUsuario)
                .ForeignKey("dbo.Vehiculoes", t => t.idVehiculo)
                .Index(t => t.idVehiculo)
                .Index(t => t.usuarios_idUsuario);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        idUsuario = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false),
                        carne = c.String(maxLength: 7),
                        correo = c.String(nullable: false),
                        contraseña = c.String(nullable: false),
                        compararContraseña = c.String(nullable: false),
                        idRol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idUsuario)
                .ForeignKey("dbo.Rols", t => t.idRol, cascadeDelete: true)
                .Index(t => t.idRol);
            
            CreateTable(
                "dbo.Rols",
                c => new
                    {
                        idRol = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false),
                        descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.idRol);
            
            CreateTable(
                "dbo.VentaVehiculoes",
                c => new
                    {
                        idVentaVehiculo = c.Int(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false),
                        idUsuario = c.Int(nullable: false),
                        idVehiculo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idVentaVehiculo)
                .ForeignKey("dbo.Usuarios", t => t.idUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Vehiculoes", t => t.idVehiculo, cascadeDelete: true)
                .Index(t => t.idUsuario)
                .Index(t => t.idVehiculo);
            
            CreateTable(
                "dbo.Vehiculoes",
                c => new
                    {
                        idVehiculo = c.Int(nullable: false, identity: true),
                        idTipoVehiculo = c.Int(nullable: false),
                        idMarca = c.Int(nullable: false),
                        idCombustible = c.Int(nullable: false),
                        idEstado = c.Int(nullable: false),
                        precio = c.String(nullable: false),
                        tipo = c.String(),
                        modelo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.idVehiculo)
                .ForeignKey("dbo.Combustibles", t => t.idCombustible, cascadeDelete: true)
                .ForeignKey("dbo.Estadoes", t => t.idEstado, cascadeDelete: true)
                .ForeignKey("dbo.Marcas", t => t.idMarca, cascadeDelete: true)
                .ForeignKey("dbo.TipoVehiculoes", t => t.idTipoVehiculo, cascadeDelete: true)
                .Index(t => t.idTipoVehiculo)
                .Index(t => t.idMarca)
                .Index(t => t.idCombustible)
                .Index(t => t.idEstado);
            
            CreateTable(
                "dbo.Combustibles",
                c => new
                    {
                        idCombustible = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        descripcion = c.String(),
                    })
                .PrimaryKey(t => t.idCombustible);
            
            CreateTable(
                "dbo.Estadoes",
                c => new
                    {
                        idEstado = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false),
                        año = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.idEstado);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        idMarca = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        descripcion = c.String(),
                    })
                .PrimaryKey(t => t.idMarca);
            
            CreateTable(
                "dbo.TipoVehiculoes",
                c => new
                    {
                        idTipoVehiculo = c.Int(nullable: false, identity: true),
                        tipo = c.String(),
                        descripcion = c.String(),
                    })
                .PrimaryKey(t => t.idTipoVehiculo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VentaVehiculoes", "idVehiculo", "dbo.Vehiculoes");
            DropForeignKey("dbo.Vehiculoes", "idTipoVehiculo", "dbo.TipoVehiculoes");
            DropForeignKey("dbo.Vehiculoes", "idMarca", "dbo.Marcas");
            DropForeignKey("dbo.Vehiculoes", "idEstado", "dbo.Estadoes");
            DropForeignKey("dbo.Vehiculoes", "idCombustible", "dbo.Combustibles");
            DropForeignKey("dbo.Archivoes", "idVehiculo", "dbo.Vehiculoes");
            DropForeignKey("dbo.VentaVehiculoes", "idUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "idRol", "dbo.Rols");
            DropForeignKey("dbo.Archivoes", "usuarios_idUsuario", "dbo.Usuarios");
            DropIndex("dbo.Vehiculoes", new[] { "idEstado" });
            DropIndex("dbo.Vehiculoes", new[] { "idCombustible" });
            DropIndex("dbo.Vehiculoes", new[] { "idMarca" });
            DropIndex("dbo.Vehiculoes", new[] { "idTipoVehiculo" });
            DropIndex("dbo.VentaVehiculoes", new[] { "idVehiculo" });
            DropIndex("dbo.VentaVehiculoes", new[] { "idUsuario" });
            DropIndex("dbo.Usuarios", new[] { "idRol" });
            DropIndex("dbo.Archivoes", new[] { "usuarios_idUsuario" });
            DropIndex("dbo.Archivoes", new[] { "idVehiculo" });
            DropTable("dbo.TipoVehiculoes");
            DropTable("dbo.Marcas");
            DropTable("dbo.Estadoes");
            DropTable("dbo.Combustibles");
            DropTable("dbo.Vehiculoes");
            DropTable("dbo.VentaVehiculoes");
            DropTable("dbo.Rols");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Archivoes");
        }
    }
}

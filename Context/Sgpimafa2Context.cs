using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sgpimafaback.InventarioProducto.Domain.Entities;
using sgpimafaback.Models;
using sgpimafaback.PosCaja.Domain.Entities;
using sgpimafaback.PosCajaArqueo.Domain.Entities;
using sgpimafaback.PosCajaEstado.Domain.Entities;
using sgpimafaback.PosCajaPagoFactura.Domain.Entities;
using sgpimafaback.PosCajaPagosAFavor.Domain.Entities;
using sgpimafaback.PosClientes.Domain.Entities;
using sgpimafaback.PosDevolucionProductoVendido.Domain.Entities;
using sgpimafaback.PosFacturacion.Domain.Entities;
using sgpimafaback.PosFacturaDetalle.Domain.Entities;
using sgpimafaback.PosInventarioProducto.Domain.Entities;
using sgpimafaback.PosMovimientoInventario.Domain.Entities;
using sgpimafaback.PosProductoCompuesto.Domain.Entities;
using sgpimafaback.PosTipoEstadoCaja.Domain.Entities;
using sgpimafaback.PosTipoEstadoPosCaja.Domain.Entities;
using sgpimafaback.PosTipoIdCliente.Domain.Entities;
using sgpimafaback.PosTipoPagosAFavor.Domain.Entities;
using sgpimafaback.PosUnidadesMedida.Domain.Entities;
using sgpimafaback.SedePos.Domain.Entities;

namespace sgpimafaback.Context;

public partial class Sgpimafa2Context : DbContext
{
    public Sgpimafa2Context()
    {
    }

    public Sgpimafa2Context(DbContextOptions<Sgpimafa2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ClienteModel> Clientes { get; set; }

    public virtual DbSet<PoscajaModel> Poscajas { get; set; }

    public virtual DbSet<PoscajaarqueoModel> Poscajaarqueos { get; set; }

    public virtual DbSet<PoscajaestadoModel> Poscajaestados { get; set; }

    public virtual DbSet<PoscajapagofacturaModel> Poscajapagofacturas { get; set; }

    public virtual DbSet<PoscajapagosafavorModel> Poscajapagosafavors { get; set; }

    public virtual DbSet<PosdevolucionproductovendidoModel> Posdevolucionproductovendidos { get; set; }

    public virtual DbSet<header> Posfacturas { get; set; }

    public virtual DbSet<PosfacturadetalleModel> Posfacturadetalles { get; set; }

    public virtual DbSet<PosinventarioproductoModel> Posinventarioproductos { get; set; }

    public virtual DbSet<inventarioproductoModel> inventarioproductos { get; set; }

    public virtual DbSet<PosmovimientoinventarioModel> Posmovimientoinventarios { get; set; }

    public virtual DbSet<PosproductocompuestoModel> Posproductocompuestos { get; set; }

    public virtual DbSet<PostipoembalajeModel> Postipoembalajes { get; set; }

    public virtual DbSet<PostipoestadocajaModel> Postipoestadocajas { get; set; }

    public virtual DbSet<PostipoestadoposcajaModel> Postipoestadoposcajas { get; set; }

    public virtual DbSet<PostipoidclienteModel> Postipoidclientes { get; set; }

    public virtual DbSet<PostipopagosafavorModel> Postipopagosafavors { get; set; }

    public virtual DbSet<PostipoproductoModel> Postipoproductos { get; set; }

    public virtual DbSet<PosunidadesmedidumModel> Posunidadesmedida { get; set; }

    public virtual DbSet<PosvendedorModel> Posvendedors { get; set; }

    public virtual DbSet<SedeposModel> Sedepos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClienteModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.Property(e => e.Apellidos).HasMaxLength(64);
            entity.Property(e => e.Ciudad).HasMaxLength(64);
            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Direccion).HasMaxLength(256);
            entity.Property(e => e.Dpto).HasMaxLength(64);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Estado).HasColumnType("smallint(5) unsigned zerofill");
            entity.Property(e => e.Nombres).HasMaxLength(64);
            entity.Property(e => e.Telefono).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PoscajaModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poscaja");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(1024);
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PoscajaarqueoModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poscajaarqueo");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.FechaArqueo).HasColumnType("timestamp");
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PoscajaestadoModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poscajaestado");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.FechaOperacion).HasColumnType("timestamp");
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.UserAccion)
                .HasMaxLength(16)
                .HasDefaultValueSql("''");
            entity.Property(e => e.ValorEstado).HasComment("Valor $ del estado de la caja. Puede ser inciail/final");
        });

        modelBuilder.Entity<PoscajapagofacturaModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poscajapagofactura");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.FechaPago).HasColumnType("timestamp");
            entity.Property(e => e.FormaPago)
                .HasColumnType("int(10) unsigned zerofill")
                .HasColumnName("formaPago");
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PoscajapagosafavorModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poscajapagosafavor");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.SaldoAfavor).HasColumnName("SaldoAFavor");
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
        });

        modelBuilder.Entity<PosdevolucionproductovendidoModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("posdevolucionproductovendido");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Direccion).HasMaxLength(256);
            entity.Property(e => e.FechaDevolucion).HasColumnType("timestamp");
            entity.Property(e => e.Motivo).HasMaxLength(1024);
            entity.Property(e => e.RazonSocial).HasMaxLength(256);
            entity.Property(e => e.Telefono).HasMaxLength(64);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PosfacturaModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("posfacturas");

            entity.Property(e => e.Ciudad).HasMaxLength(64);
            entity.Property(e => e.Concepto).HasMaxLength(256);
            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Direccion).HasMaxLength(256);
            entity.Property(e => e.FechaFactura).HasColumnType("timestamp");
            entity.Property(e => e.FechaVencimiento).HasColumnType("timestamp");
            entity.Property(e => e.RazonSocial).HasMaxLength(256);
            entity.Property(e => e.Telefono).HasMaxLength(64);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PosfacturadetalleModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("posfacturadetalle");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(512);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<PosinventarioproductoModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("posinventarioproductos");

            entity.Property(e => e.Color).HasMaxLength(16);
            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(512);
            entity.Property(e => e.FechaCreacion).HasColumnType("timestamp");
            entity.Property(e => e.FechaVencimiento).HasColumnType("timestamp");
            entity.Property(e => e.Lote).HasMaxLength(32);
            entity.Property(e => e.Nombre).HasMaxLength(256);
            entity.Property(e => e.Olor).HasMaxLength(16);
            entity.Property(e => e.Tamano).HasMaxLength(128);
            entity.Property(e => e.Textura).HasMaxLength(128);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<inventarioproductoModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inventarioproductos");

            entity.Property(e => e.Color).HasMaxLength(16);
            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(512);
            entity.Property(e => e.FechaCreacion).HasColumnType("timestamp");
            entity.Property(e => e.FechaVencimiento).HasColumnType("timestamp");
            entity.Property(e => e.Lote).HasMaxLength(32);
            entity.Property(e => e.Nombre).HasMaxLength(256);
            entity.Property(e => e.Olor).HasMaxLength(16);
            entity.Property(e => e.Tamano).HasMaxLength(128);
            entity.Property(e => e.Textura).HasMaxLength(128);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PosmovimientoinventarioModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("posmovimientoinventario");

            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("createdAt");
            entity.Property(e => e.FechaMovimiento)
                .HasColumnType("timestamp")
                .HasColumnName("fechaMovimiento");
            entity.Property(e => e.IdCodigo).HasColumnName("idCodigo");
            entity.Property(e => e.IdPos).HasColumnName("idPos");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updatedAt");
            entity.Property(e => e.User)
                .HasMaxLength(16)
                .HasColumnName("user");
        });

        modelBuilder.Entity<PosproductocompuestoModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("posproductocompuesto");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(256);
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'00000'")
                .HasColumnType("smallint(5) unsigned zerofill");
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PostipoembalajeModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("postipoembalaje");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(256);
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'00000'")
                .HasColumnType("smallint(5) unsigned zerofill");
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PostipoestadocajaModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("postipoestadocaja");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(256);
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'00000'")
                .HasColumnType("smallint(5) unsigned zerofill");
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PostipoestadoposcajaModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("postipoestadoposcaja");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(256);
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'00000'")
                .HasColumnType("smallint(5) unsigned zerofill");
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PostipoidclienteModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("postipoidcliente");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(256);
            entity.Property(e => e.Estado).HasColumnType("smallint(5) unsigned zerofill");
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PostipopagosafavorModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("postipopagosafavor");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(256);
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'00000'")
                .HasColumnType("smallint(5) unsigned zerofill");
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PostipoproductoModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("postipoproducto");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(256);
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'00000'")
                .HasColumnType("smallint(5) unsigned zerofill");
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PosunidadesmedidumModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("posunidadesmedida");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(256);
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'00000'")
                .HasColumnType("smallint(5) unsigned zerofill");
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        modelBuilder.Entity<PosvendedorModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("posvendedor");

            entity.Property(e => e.Apellidos).HasMaxLength(64);
            entity.Property(e => e.Ciudad).HasMaxLength(64);
            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Direccion).HasMaxLength(256);
            entity.Property(e => e.Dpto).HasMaxLength(32);
            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.Nombres).HasMaxLength(64);
            entity.Property(e => e.Telefono).HasMaxLength(64);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(32);
        });

        modelBuilder.Entity<SedeposModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sedepos");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp");
            entity.Property(e => e.Descripcion).HasMaxLength(256);
            entity.Property(e => e.Direccion).HasMaxLength(256);
            entity.Property(e => e.Nombre).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp");
            entity.Property(e => e.User).HasMaxLength(16);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

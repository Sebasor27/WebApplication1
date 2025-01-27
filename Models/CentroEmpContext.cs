using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WebApplication1.Models;

public partial class CentroEmpContext : DbContext
{
    public CentroEmpContext()
    {
    }

    public CentroEmpContext(DbContextOptions<CentroEmpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccionesIepm> AccionesIepms { get; set; }

    public virtual DbSet<Competencia> Competencias { get; set; }

    public virtual DbSet<DatosEmp> DatosEmps { get; set; }

    public virtual DbSet<Dimensione> Dimensiones { get; set; }

    public virtual DbSet<Emprendedore> Emprendedores { get; set; }

    public virtual DbSet<EncuestasIce> EncuestasIces { get; set; }

    public virtual DbSet<EncuestasIepm> EncuestasIepms { get; set; }

    public virtual DbSet<Indicadore> Indicadores { get; set; }

    public virtual DbSet<PreguntasIce> PreguntasIces { get; set; }

    public virtual DbSet<PreguntasIepm> PreguntasIepms { get; set; }

    public virtual DbSet<ResultadosIce> ResultadosIces { get; set; }

    public virtual DbSet<ResultadosIepm> ResultadosIepms { get; set; }

    public virtual DbSet<ResumenIce> ResumenIces { get; set; }

    public virtual DbSet<ResumenIepm> ResumenIepms { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=centroEmp;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AccionesIepm>(entity =>
        {
            entity.HasKey(e => e.IdAccionesIepm).HasName("PRIMARY");

            entity.ToTable("accionesIEPM");

            entity.Property(e => e.IdAccionesIepm)
                .HasColumnType("int(11)")
                .HasColumnName("id_accionesIEPM");
            entity.Property(e => e.Rango)
                .HasMaxLength(50)
                .HasColumnName("rango");
            entity.Property(e => e.Valoracion)
                .HasMaxLength(550)
                .HasColumnName("valoracion");
        });

        modelBuilder.Entity<Competencia>(entity =>
        {
            entity.HasKey(e => e.IdCompetencia).HasName("PRIMARY");

            entity.ToTable("competencias");

            entity.Property(e => e.IdCompetencia)
                .HasColumnType("int(11)")
                .HasColumnName("id_competencia");
            entity.Property(e => e.NombreCompetencia)
                .HasMaxLength(100)
                .HasColumnName("nombre_competencia");
            entity.Property(e => e.PesoRelativo)
                .HasPrecision(6, 5)
                .HasColumnName("peso_relativo");
            entity.Property(e => e.PuntosMaximos)
                .HasColumnType("int(11)")
                .HasColumnName("puntos_maximos");
        });

        modelBuilder.Entity<DatosEmp>(entity =>
        {
            entity.HasKey(e => e.IdDatosEmp).HasName("PRIMARY");

            entity.ToTable("datos_emp");

            entity.HasIndex(e => e.IdEmprendedor, "fk_id_emprendedor");

            entity.Property(e => e.IdDatosEmp)
                .HasColumnType("int(11)")
                .HasColumnName("id_datos_emp");
            entity.Property(e => e.IdEmprendedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_emprendedor");
            entity.Property(e => e.NegociosEmp)
                .HasMaxLength(50)
                .HasColumnName("negocios_emp");
            entity.Property(e => e.NivelEdAlc)
                .HasMaxLength(100)
                .HasColumnName("nivel_ed_alc");
            entity.Property(e => e.Provincia)
                .HasColumnType("text")
                .HasColumnName("provincia");
            entity.Property(e => e.RangoEdad)
                .HasMaxLength(50)
                .HasColumnName("rango_edad");
            entity.Property(e => e.TitulosAlc)
                .HasColumnType("text")
                .HasColumnName("titulos_alc");

            entity.HasOne(d => d.IdEmprendedorNavigation).WithMany(p => p.DatosEmps)
                .HasForeignKey(d => d.IdEmprendedor)
                .HasConstraintName("fk_id_emprendedor");
        });

        modelBuilder.Entity<Dimensione>(entity =>
        {
            entity.HasKey(e => e.IdDimension).HasName("PRIMARY");

            entity.ToTable("dimensiones");

            entity.Property(e => e.IdDimension)
                .HasColumnType("int(11)")
                .HasColumnName("id_dimension");
            entity.Property(e => e.NombreDimension)
                .HasMaxLength(100)
                .HasColumnName("nombre_dimension");
            entity.Property(e => e.PesoRelativo)
                .HasPrecision(6, 5)
                .HasColumnName("peso_relativo");
            entity.Property(e => e.PuntosMaximos)
                .HasColumnType("int(11)")
                .HasColumnName("puntos_maximos");
        });

        modelBuilder.Entity<Emprendedore>(entity =>
        {
            entity.HasKey(e => e.IdEmprendedor).HasName("PRIMARY");

            entity.ToTable("emprendedores");

            entity.HasIndex(e => e.Cedula, "cedula").IsUnique();

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.HasIndex(e => e.IdUsuario, "id_usuario");

            entity.HasIndex(e => e.Ruc, "ruc").IsUnique();

            entity.Property(e => e.IdEmprendedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_emprendedor");
            entity.Property(e => e.AnoCreacionEmpresa)
                .HasColumnType("year(4)")
                .HasColumnName("ano_creacion_empresa");
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .HasColumnName("cedula");
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .HasColumnName("celular");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Edad)
                .HasColumnType("enum('18-25','26-65','65+')")
                .HasColumnName("edad");
            entity.Property(e => e.EmpleadosHombres)
                .HasColumnType("int(11)")
                .HasColumnName("empleados_hombres");
            entity.Property(e => e.EmpleadosMujeres)
                .HasColumnType("int(11)")
                .HasColumnName("empleados_mujeres");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.NivelEstudio)
                .HasMaxLength(50)
                .HasColumnName("nivel_estudio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.RangoEdadEmpleados)
                .HasMaxLength(50)
                .HasColumnName("rango_edad_empleados");
            entity.Property(e => e.Ruc)
                .HasMaxLength(13)
                .HasColumnName("ruc");
            entity.Property(e => e.SueldoMensual)
                .HasColumnType("enum('0-460','460-750','750-1500')")
                .HasColumnName("sueldo_mensual");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoEmpresa)
                .HasMaxLength(50)
                .HasColumnName("tipo_empresa");
            entity.Property(e => e.TrabajoRelacionDependencia).HasColumnName("trabajo_relacion_dependencia");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Emprendedores)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emprendedores_ibfk_1");
        });

        modelBuilder.Entity<EncuestasIce>(entity =>
        {
            entity.HasKey(e => e.IdRespuesta).HasName("PRIMARY");

            entity.ToTable("encuestas_ice");

            entity.HasIndex(e => e.IdEmprendedor, "id_emprendedor");

            entity.HasIndex(e => e.IdPregunta, "id_pregunta");

            entity.Property(e => e.IdRespuesta)
                .HasColumnType("int(11)")
                .HasColumnName("id_respuesta");
            entity.Property(e => e.IdEmprendedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_emprendedor");
            entity.Property(e => e.IdPregunta)
                .HasColumnType("int(11)")
                .HasColumnName("id_pregunta");
            entity.Property(e => e.ValorRespuesta)
                .HasColumnType("int(11)")
                .HasColumnName("valor_respuesta");

            entity.HasOne(d => d.IdEmprendedorNavigation).WithMany(p => p.EncuestasIces)
                .HasForeignKey(d => d.IdEmprendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("encuestas_ice_ibfk_1");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.EncuestasIces)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("encuestas_ice_ibfk_2");
        });

        modelBuilder.Entity<EncuestasIepm>(entity =>
        {
            entity.HasKey(e => e.IdRespuesta).HasName("PRIMARY");

            entity.ToTable("encuestas_iepm");

            entity.HasIndex(e => e.IdEmprendedor, "id_emprendedor");

            entity.HasIndex(e => e.IdPregunta, "id_pregunta");

            entity.Property(e => e.IdRespuesta)
                .HasColumnType("int(11)")
                .HasColumnName("id_respuesta");
            entity.Property(e => e.IdEmprendedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_emprendedor");
            entity.Property(e => e.IdPregunta)
                .HasColumnType("int(11)")
                .HasColumnName("id_pregunta");
            entity.Property(e => e.ValorRespuesta)
                .HasColumnType("int(11)")
                .HasColumnName("valor_respuesta");

            entity.HasOne(d => d.IdEmprendedorNavigation).WithMany(p => p.EncuestasIepms)
                .HasForeignKey(d => d.IdEmprendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("encuestas_iepm_ibfk_1");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.EncuestasIepms)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("encuestas_iepm_ibfk_2");
        });

        modelBuilder.Entity<Indicadore>(entity =>
        {
            entity.HasKey(e => e.IdIndicadores).HasName("PRIMARY");

            entity.ToTable("indicadores");

            entity.Property(e => e.IdIndicadores)
                .HasColumnType("int(11)")
                .HasColumnName("id_indicadores");
            entity.Property(e => e.Acciones)
                .HasMaxLength(250)
                .HasColumnName("acciones");
            entity.Property(e => e.NivelDesarrollo)
                .HasMaxLength(550)
                .HasColumnName("nivel_desarrollo");
            entity.Property(e => e.Rango)
                .HasMaxLength(50)
                .HasColumnName("rango");
            entity.Property(e => e.Valoracion)
                .HasMaxLength(550)
                .HasColumnName("valoracion");
        });

        modelBuilder.Entity<PreguntasIce>(entity =>
        {
            entity.HasKey(e => e.IdPregunta).HasName("PRIMARY");

            entity.ToTable("preguntas_ice");

            entity.HasIndex(e => e.IdCompetencia, "id_competencia");

            entity.Property(e => e.IdPregunta)
                .HasColumnType("int(11)")
                .HasColumnName("id_pregunta");
            entity.Property(e => e.IdCompetencia)
                .HasColumnType("int(11)")
                .HasColumnName("id_competencia");
            entity.Property(e => e.TextoPregunta)
                .HasColumnType("text")
                .HasColumnName("texto_pregunta");

            entity.HasOne(d => d.IdCompetenciaNavigation).WithMany(p => p.PreguntasIces)
                .HasForeignKey(d => d.IdCompetencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("preguntas_ice_ibfk_1");
        });

        modelBuilder.Entity<PreguntasIepm>(entity =>
        {
            entity.HasKey(e => e.IdPregunta).HasName("PRIMARY");

            entity.ToTable("preguntas_iepm");

            entity.HasIndex(e => e.IdDimension, "id_dimension");

            entity.Property(e => e.IdPregunta)
                .HasColumnType("int(11)")
                .HasColumnName("id_pregunta");
            entity.Property(e => e.IdDimension)
                .HasColumnType("int(11)")
                .HasColumnName("id_dimension");
            entity.Property(e => e.TextoPregunta)
                .HasColumnType("text")
                .HasColumnName("texto_pregunta");

            entity.HasOne(d => d.IdDimensionNavigation).WithMany(p => p.PreguntasIepms)
                .HasForeignKey(d => d.IdDimension)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("preguntas_iepm_ibfk_1");
        });

        modelBuilder.Entity<ResultadosIce>(entity =>
        {
            entity.HasKey(e => e.IdResultado).HasName("PRIMARY");

            entity.ToTable("resultados_ice");

            entity.HasIndex(e => e.IdCompetencia, "id_competencia");

            entity.HasIndex(e => e.IdEmprendedor, "id_emprendedor");

            entity.Property(e => e.IdResultado)
                .HasColumnType("int(11)")
                .HasColumnName("id_resultado");
            entity.Property(e => e.IdCompetencia)
                .HasColumnType("int(11)")
                .HasColumnName("id_competencia");
            entity.Property(e => e.IdEmprendedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_emprendedor");
            entity.Property(e => e.PuntuacionCompetencia)
                .HasPrecision(6, 5)
                .HasColumnName("puntuacion_competencia");
            entity.Property(e => e.Valoracion)
                .HasColumnType("int(11)")
                .HasColumnName("valoracion");

            entity.HasOne(d => d.IdCompetenciaNavigation).WithMany(p => p.ResultadosIces)
                .HasForeignKey(d => d.IdCompetencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resultados_ice_ibfk_2");

            entity.HasOne(d => d.IdEmprendedorNavigation).WithMany(p => p.ResultadosIces)
                .HasForeignKey(d => d.IdEmprendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resultados_ice_ibfk_1");
        });

        modelBuilder.Entity<ResultadosIepm>(entity =>
        {
            entity.HasKey(e => e.IdResultado).HasName("PRIMARY");

            entity.ToTable("resultados_iepm");

            entity.HasIndex(e => e.IdDimension, "id_dimension");

            entity.HasIndex(e => e.IdEmprendedor, "id_emprendedor");

            entity.Property(e => e.IdResultado)
                .HasColumnType("int(11)")
                .HasColumnName("id_resultado");
            entity.Property(e => e.IdDimension)
                .HasColumnType("int(11)")
                .HasColumnName("id_dimension");
            entity.Property(e => e.IdEmprendedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_emprendedor");
            entity.Property(e => e.PuntuacionDimension)
                .HasPrecision(6, 5)
                .HasColumnName("puntuacion_dimension");

            entity.HasOne(d => d.IdDimensionNavigation).WithMany(p => p.ResultadosIepms)
                .HasForeignKey(d => d.IdDimension)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resultados_iepm_ibfk_2");

            entity.HasOne(d => d.IdEmprendedorNavigation).WithMany(p => p.ResultadosIepms)
                .HasForeignKey(d => d.IdEmprendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resultados_iepm_ibfk_1");
        });

        modelBuilder.Entity<ResumenIce>(entity =>
        {
            entity.HasKey(e => e.IdEmprendedor).HasName("PRIMARY");

            entity.ToTable("resumen_ice");

            entity.HasIndex(e => e.IdIndicadores, "id_indicador_ice_ibfk_2");

            entity.Property(e => e.IdEmprendedor)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_emprendedor");
            entity.Property(e => e.IdIndicadores)
                .HasColumnType("int(11)")
                .HasColumnName("id_indicadores");
            entity.Property(e => e.ValorIceTotal)
                .HasPrecision(6, 5)
                .HasColumnName("valor_ice_total");

            entity.HasOne(d => d.IdEmprendedorNavigation).WithOne(p => p.ResumenIce)
                .HasForeignKey<ResumenIce>(d => d.IdEmprendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resumen_ice_ibfk_1");

            entity.HasOne(d => d.IdIndicadoresNavigation).WithMany(p => p.ResumenIces)
                .HasForeignKey(d => d.IdIndicadores)
                .HasConstraintName("id_indicador_ice_ibfk_2");
        });

        modelBuilder.Entity<ResumenIepm>(entity =>
        {
            entity.HasKey(e => e.IdEmprendedor).HasName("PRIMARY");

            entity.ToTable("resumen_iepm");

            entity.HasIndex(e => e.IdAccionesIepm, "acciones_iepm_ibfk_2");

            entity.Property(e => e.IdEmprendedor)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_emprendedor");
            entity.Property(e => e.IdAccionesIepm)
                .HasColumnType("int(11)")
                .HasColumnName("id_accionesIEPM");
            entity.Property(e => e.ValorIepmTotal)
                .HasPrecision(6, 5)
                .HasColumnName("valor_iepm_total");

            entity.HasOne(d => d.IdAccionesIepmNavigation).WithMany(p => p.ResumenIepms)
                .HasForeignKey(d => d.IdAccionesIepm)
                .HasConstraintName("acciones_iepm_ibfk_2");

            entity.HasOne(d => d.IdEmprendedorNavigation).WithOne(p => p.ResumenIepm)
                .HasForeignKey<ResumenIepm>(d => d.IdEmprendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resumen_iepm_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoUsuario)
                .HasColumnType("enum('Administrador','Reportes')")
                .HasColumnName("tipo_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

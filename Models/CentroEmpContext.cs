using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<AccionesMejoraIepm> AccionesMejoraIepms { get; set; }

    public virtual DbSet<Competencia> Competencias { get; set; }

    public virtual DbSet<CriteriosEvaluacionIepm> CriteriosEvaluacionIepms { get; set; }

    public virtual DbSet<CuestionariosIepm> CuestionariosIepms { get; set; }

    public virtual DbSet<DatosEmp> DatosEmps { get; set; }

    public virtual DbSet<DimensionesIepm> DimensionesIepms { get; set; }

    public virtual DbSet<Emprendedore> Emprendedores { get; set; }

    public virtual DbSet<EncuestasIce> EncuestasIces { get; set; }

    public virtual DbSet<EncuestasIepm> EncuestasIepms { get; set; }

    public virtual DbSet<Indicadore> Indicadores { get; set; }

    public virtual DbSet<IndicadoresIepm> IndicadoresIepms { get; set; }

    public virtual DbSet<PreguntasIce> PreguntasIces { get; set; }

    public virtual DbSet<PreguntasIepm> PreguntasIepms { get; set; }

    public virtual DbSet<RespuestasIepm> RespuestasIepms { get; set; }

    public virtual DbSet<ResultadosAccionesIepm> ResultadosAccionesIepms { get; set; }

    public virtual DbSet<ResultadosDimensionesIepm> ResultadosDimensionesIepms { get; set; }

    public virtual DbSet<ResultadosIce> ResultadosIces { get; set; }

    public virtual DbSet<ResultadosIepm> ResultadosIepms { get; set; }

    public virtual DbSet<ResultadosIndicadoresIepm> ResultadosIndicadoresIepms { get; set; }

    public virtual DbSet<ResumenIce> ResumenIces { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=centroEmp;User Id=sa;Password=TuContraseñaFuerte123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccionesMejoraIepm>(entity =>
        {
            entity.HasKey(e => e.IdAccion).HasName("PK__Acciones__9845169B327CDA2F");

            entity.ToTable("AccionesMejoraIEPM");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.RangoMax).HasColumnType("decimal(5, 3)");
            entity.Property(e => e.RangoMin).HasColumnType("decimal(5, 3)");
            entity.Property(e => e.Recomendaciones).HasMaxLength(1000);
        });

        modelBuilder.Entity<Competencia>(entity =>
        {
            entity.HasKey(e => e.IdCompetencia);

            entity.ToTable("competencias");

            entity.Property(e => e.IdCompetencia).HasColumnName("id_competencia");
            entity.Property(e => e.NombreCompetencia)
                .HasMaxLength(100)
                .HasColumnName("nombre_competencia");
            entity.Property(e => e.PesoRelativo)
                .HasColumnType("decimal(6, 5)")
                .HasColumnName("peso_relativo");
            entity.Property(e => e.PuntosMaximos).HasColumnName("puntos_maximos");
        });

        modelBuilder.Entity<CriteriosEvaluacionIepm>(entity =>
        {
            entity.HasKey(e => e.IdCriterio).HasName("PK__Criterio__7139448222A73291");

            entity.ToTable("CriteriosEvaluacionIEPM");

            entity.Property(e => e.Descripcion).HasMaxLength(500);

            entity.HasOne(d => d.IdIndicadorNavigation).WithMany(p => p.CriteriosEvaluacionIepms)
                .HasForeignKey(d => d.IdIndicador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Criterios__IdInd__7A3223E8");
        });

        modelBuilder.Entity<CuestionariosIepm>(entity =>
        {
            entity.HasKey(e => e.IdCuestionario).HasName("PK__Cuestion__69F3E5579F6DDBF1");

            entity.ToTable("CuestionariosIEPM");

            entity.Property(e => e.Destinatario).HasMaxLength(50);
            entity.Property(e => e.Instrucciones).HasMaxLength(1000);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<DatosEmp>(entity =>
        {
            entity.HasKey(e => e.IdDatosEmp);

            entity.ToTable("datos_emp");

            entity.HasIndex(e => e.IdEmprendedor, "fk_id_emprendedor");

            entity.Property(e => e.IdDatosEmp).HasColumnName("id_datos_emp");
            entity.Property(e => e.IdEmprendedor).HasColumnName("id_emprendedor");
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

        modelBuilder.Entity<DimensionesIepm>(entity =>
        {
            entity.HasKey(e => e.IdDimensionIepm).HasName("PK__Dimensio__C687E8DEC52A8E53");

            entity.ToTable("DimensionesIEPM");

            entity.Property(e => e.IdDimensionIepm).HasColumnName("IdDimensionIEPM");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Formula).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Peso).HasColumnType("decimal(5, 3)");
        });

        modelBuilder.Entity<Emprendedore>(entity =>
        {
            entity.HasKey(e => e.IdEmprendedor).HasName("Pk_emprendedores");

            entity.ToTable("emprendedores");

            entity.HasIndex(e => e.Cedula, "cedula").IsUnique();

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.HasIndex(e => e.IdUsuario, "id_usuario");

            entity.HasIndex(e => e.Ruc, "ruc").IsUnique();

            entity.Property(e => e.IdEmprendedor).HasColumnName("id_emprendedor");
            entity.Property(e => e.AnoCreacionEmpresa).HasColumnName("ano_creacion_empresa");
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
                .HasMaxLength(10)
                .HasColumnName("edad");
            entity.Property(e => e.EmpleadosHombres).HasColumnName("empleados_hombres");
            entity.Property(e => e.EmpleadosMujeres).HasColumnName("empleados_mujeres");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
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
                .HasMaxLength(10)
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
            entity.HasKey(e => e.IdRespuesta).HasName("Pk_encuestas_ice");

            entity.ToTable("encuestas_ice");

            entity.HasIndex(e => e.IdEmprendedor, "id_emprendedor");

            entity.HasIndex(e => e.IdEncuesta, "id_encuesta");

            entity.HasIndex(e => e.IdPregunta, "id_pregunta");

            entity.Property(e => e.IdRespuesta).HasColumnName("id_respuesta");
            entity.Property(e => e.IdEmprendedor).HasColumnName("id_emprendedor");
            entity.Property(e => e.IdEncuesta)
                .HasDefaultValue(1)
                .HasColumnName("id_encuesta");
            entity.Property(e => e.IdPregunta).HasColumnName("id_pregunta");
            entity.Property(e => e.ValorRespuesta).HasColumnName("valor_respuesta");

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
            entity.HasKey(e => e.IdEncuestaIepm).HasName("PK__Encuesta__72EAE9F7AB2B5FBA");

            entity.ToTable("EncuestasIEPM");

            entity.Property(e => e.IdEncuestaIepm).HasColumnName("IdEncuestaIEPM");
            entity.Property(e => e.FechaAplicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdEmprendedorNavigation).WithMany(p => p.EncuestasIepms)
                .HasForeignKey(d => d.IdEmprendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Encuestas__IdEmp__634EBE90");
        });

        modelBuilder.Entity<Indicadore>(entity =>
        {
            entity.HasKey(e => e.IdIndicadores).HasName("Pk_indicadores");

            entity.ToTable("indicadores");

            entity.Property(e => e.IdIndicadores).HasColumnName("id_indicadores");
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

        modelBuilder.Entity<IndicadoresIepm>(entity =>
        {
            entity.HasKey(e => e.IdIndicador).HasName("PK__Indicado__F912C4F19D1724C2");

            entity.ToTable("IndicadoresIEPM");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Formula).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Peso).HasColumnType("decimal(5, 3)");

            entity.HasOne(d => d.IdDimensionNavigation).WithMany(p => p.IndicadoresIepms)
                .HasForeignKey(d => d.IdDimension)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Indicador__IdDim__6AEFE058");
        });

        modelBuilder.Entity<PreguntasIce>(entity =>
        {
            entity.HasKey(e => e.IdPregunta);

            entity.ToTable("preguntas_ice");

            entity.HasIndex(e => e.IdCompetencia, "id_competencia");

            entity.Property(e => e.IdPregunta).HasColumnName("id_pregunta");
            entity.Property(e => e.IdCompetencia).HasColumnName("id_competencia");
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
            entity.HasKey(e => e.IdPregunta).HasName("PK__Pregunta__754EC09EA2CC1C55");

            entity.ToTable("PreguntasIEPM");

            entity.Property(e => e.Enunciado).HasMaxLength(500);

            entity.HasOne(d => d.IdCuestionarioNavigation).WithMany(p => p.PreguntasIepms)
                .HasForeignKey(d => d.IdCuestionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__IdCue__70A8B9AE");

            entity.HasOne(d => d.IdIndicadorNavigation).WithMany(p => p.PreguntasIepms)
                .HasForeignKey(d => d.IdIndicador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__IdInd__719CDDE7");
        });

        modelBuilder.Entity<RespuestasIepm>(entity =>
        {
            entity.HasKey(e => e.IdRespuesta).HasName("PK__Respuest__D34801983A29682C");

            entity.ToTable("RespuestasIEPM");

            entity.Property(e => e.Comentarios).HasMaxLength(500);

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.RespuestasIepms)
                .HasForeignKey(d => d.IdEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Respuesta__IdEnc__756D6ECB");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.RespuestasIepms)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Respuesta__IdPre__76619304");

            entity.HasOne(d => d.IdEmprendedorNavigation)
                .WithMany(p => p.RespuestasIepms) // Asume que en `Emprendedores` tienes una colección `RespuestasIepms`
                .HasForeignKey(d => d.IdEmprendedor)
                .OnDelete(DeleteBehavior.ClientSetNull) // Puedes cambiar a `Cascade` si deseas eliminación en cascada
                .HasConstraintName("FK__Respuesta__IdEmp__777D8AB1");
        });

        modelBuilder.Entity<ResultadosAccionesIepm>(entity =>
 {
     entity.HasKey(e => e.IdResultadoAccion)
           .HasName("PK_ResultadosAccionesIEPM");

     entity.ToTable("ResultadosAccionesIEPM");

     // Configuración clara de las relaciones
     entity.HasOne(d => d.IdAccionNavigation)
           .WithMany(p => p.ResultadosAccionesIepms)
           .HasForeignKey(d => d.IdAccion)
           .OnDelete(DeleteBehavior.Cascade)  // Cambiado a Cascade
           .HasConstraintName("FK_ResultadosAcciones_AccionesMejora");

     entity.HasOne(d => d.IdResultadoIepmNavigation)
           .WithMany(p => p.ResultadosAccionesIepms)
           .HasForeignKey(d => d.IdResultadoIepm)
           .OnDelete(DeleteBehavior.Cascade)  // Cambiado a Cascade
           .HasConstraintName("FK_ResultadosAcciones_ResultadosIEPM");
 });

        modelBuilder.Entity<ResultadosDimensionesIepm>(entity =>
        {
            entity.HasKey(e => e.IdResultadoDimension).HasName("PK__Resultad__35BDC62F3509C98E");

            entity.ToTable("ResultadosDimensionesIEPM");

            entity.Property(e => e.FechaCalculo)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Valor).HasColumnType("decimal(5, 3)");

            entity.HasOne(d => d.IdDimensionNavigation).WithMany(p => p.ResultadosDimensionesIepms)
                .HasForeignKey(d => d.IdDimension)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__IdDim__05A3D694");

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.ResultadosDimensionesIepms)
                .HasForeignKey(d => d.IdEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__IdEnc__04AFB25B");
        });

        modelBuilder.Entity<ResultadosIce>(entity =>
        {
            entity.HasKey(e => e.IdResultado);

            entity.ToTable("resultados_ice");

            entity.HasIndex(e => e.IdCompetencia, "id_competencia");

            entity.HasIndex(e => e.IdEmprendedor, "id_emprendedor");

            entity.Property(e => e.IdResultado).HasColumnName("id_resultado");
            entity.Property(e => e.IdCompetencia).HasColumnName("id_competencia");
            entity.Property(e => e.IdEmprendedor).HasColumnName("id_emprendedor");
            entity.Property(e => e.IdEncuesta)
                .HasDefaultValue(1)
                .HasColumnName("id_encuesta");
            entity.Property(e => e.PuntuacionCompetencia)
                .HasColumnType("decimal(6, 5)")
                .HasColumnName("puntuacion_competencia");
            entity.Property(e => e.Valoracion).HasColumnName("valoracion");

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
    // 1. Configuración de clave primaria con nombre descriptivo
    entity.HasKey(e => e.IdResultadoIepm)
          .HasName("PK_ResultadosIEPM");

    entity.ToTable("ResultadosIEPM");

    // 2. Configuración explícita de propiedades
    entity.Property(e => e.IdResultadoIepm)
          .HasColumnName("IdResultadoIEPM");

    entity.Property(e => e.Iepm)
          .HasColumnType("decimal(5, 3)")
          .HasColumnName("IEPM");

    entity.Property(e => e.Valoracion)
          .HasMaxLength(50)
          .IsRequired();  // ← Añadido para claridad

    entity.Property(e => e.FechaCalculo)
          .HasDefaultValueSql("(getdate())")
          .HasColumnType("datetime");

    // 3. Relaciones mejoradas
    entity.HasOne(d => d.IdEmprendedorNavigation)
          .WithMany(p => p.ResultadosIepms)
          .HasForeignKey(d => d.IdEmprendedor)
          .OnDelete(DeleteBehavior.Cascade)  // Cambiado a Cascade
          .HasConstraintName("FK_ResultadosIEPM_Emprendedores");

    entity.HasOne(d => d.IdEncuestaNavigation)
          .WithMany(p => p.ResultadosIepms)
          .HasForeignKey(d => d.IdEncuesta)
          .OnDelete(DeleteBehavior.Cascade)  // Cambiado a Cascade
          .HasConstraintName("FK_ResultadosIEPM_Encuestas");
});
        modelBuilder.Entity<ResultadosIndicadoresIepm>(entity =>
        {
            entity.HasKey(e => e.IdResultadoIndicador).HasName("PK__Resultad__038B095E94CDFF22");

            entity.ToTable("ResultadosIndicadoresIEPM");

            entity.Property(e => e.FechaCalculo)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Valor).HasColumnType("decimal(5, 3)");

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.ResultadosIndicadoresIepms)
                .HasForeignKey(d => d.IdEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__IdEnc__7EF6D905");

            entity.HasOne(d => d.IdIndicadorNavigation).WithMany(p => p.ResultadosIndicadoresIepms)
                .HasForeignKey(d => d.IdIndicador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__IdInd__7FEAFD3E");
        });

        modelBuilder.Entity<ResumenIce>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("resumen_ice");

            entity.HasIndex(e => e.IdIndicadores, "id_indicador_ice_ibfk_2");

            entity.Property(e => e.IdEmprendedor).HasColumnName("id_emprendedor");
            entity.Property(e => e.IdEncuesta)
                .HasDefaultValue(1)
                .HasColumnName("id_encuesta");
            entity.Property(e => e.IdIndicadores).HasColumnName("id_indicadores");
            entity.Property(e => e.ValorIceTotal)
                .HasColumnType("decimal(6, 5)")
                .HasColumnName("valor_ice_total");

            entity.HasOne(d => d.IdEmprendedorNavigation).WithMany()
                .HasForeignKey(d => d.IdEmprendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resumen_ice_ibfk_1");

            entity.HasOne(d => d.IdIndicadoresNavigation).WithMany()
                .HasForeignKey(d => d.IdIndicadores)
                .HasConstraintName("id_indicador_ice_ibfk_2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(10)
                .HasColumnName("tipo_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

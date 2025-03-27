using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class FixResultadosIEPMRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "encuestas_iepm_ibfk_1",
                table: "encuestas_iepm");

            migrationBuilder.DropForeignKey(
                name: "encuestas_iepm_ibfk_2",
                table: "encuestas_iepm");

            migrationBuilder.DropForeignKey(
                name: "preguntas_iepm_ibfk_1",
                table: "preguntas_iepm");

            migrationBuilder.DropForeignKey(
                name: "resultados_iepm_ibfk_1",
                table: "resultados_iepm");

            migrationBuilder.DropForeignKey(
                name: "resultados_iepm_ibfk_2",
                table: "resultados_iepm");

            migrationBuilder.DropTable(
                name: "dimensiones");

            migrationBuilder.DropTable(
                name: "resumen_iepm");

            migrationBuilder.DropTable(
                name: "accionesIEPM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_resumen_ice",
                table: "resumen_ice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_resultados_iepm",
                table: "resultados_iepm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_preguntas_iepm",
                table: "preguntas_iepm");

            migrationBuilder.DropIndex(
                name: "id_dimension",
                table: "preguntas_iepm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_encuestas_iepm",
                table: "encuestas_iepm");

            migrationBuilder.DropIndex(
                name: "id_pregunta",
                table: "encuestas_iepm");

            migrationBuilder.DropColumn(
                name: "puntuacion_dimension",
                table: "resultados_iepm");

            migrationBuilder.DropColumn(
                name: "texto_pregunta",
                table: "preguntas_iepm");

            migrationBuilder.DropColumn(
                name: "id_respuesta",
                table: "encuestas_iepm");

            migrationBuilder.DropColumn(
                name: "id_pregunta",
                table: "encuestas_iepm");

            migrationBuilder.RenameTable(
                name: "resultados_iepm",
                newName: "ResultadosIEPM");

            migrationBuilder.RenameTable(
                name: "preguntas_iepm",
                newName: "PreguntasIEPM");

            migrationBuilder.RenameTable(
                name: "encuestas_iepm",
                newName: "EncuestasIEPM");

            migrationBuilder.RenameColumn(
                name: "id_emprendedor",
                table: "ResultadosIEPM",
                newName: "IdEmprendedor");

            migrationBuilder.RenameColumn(
                name: "id_dimension",
                table: "ResultadosIEPM",
                newName: "IdEncuesta");

            migrationBuilder.RenameColumn(
                name: "id_resultado",
                table: "ResultadosIEPM",
                newName: "IdResultadoIEPM");

            migrationBuilder.RenameIndex(
                name: "id_emprendedor",
                table: "ResultadosIEPM",
                newName: "IX_ResultadosIEPM_IdEmprendedor");

            migrationBuilder.RenameIndex(
                name: "id_dimension",
                table: "ResultadosIEPM",
                newName: "IX_ResultadosIEPM_IdEncuesta");

            migrationBuilder.RenameColumn(
                name: "id_pregunta",
                table: "PreguntasIEPM",
                newName: "IdPregunta");

            migrationBuilder.RenameColumn(
                name: "id_dimension",
                table: "PreguntasIEPM",
                newName: "Orden");

            migrationBuilder.RenameColumn(
                name: "id_emprendedor",
                table: "EncuestasIEPM",
                newName: "IdEmprendedor");

            migrationBuilder.RenameColumn(
                name: "valor_respuesta",
                table: "EncuestasIEPM",
                newName: "IdEncuestaIEPM");

            migrationBuilder.RenameIndex(
                name: "id_emprendedor",
                table: "EncuestasIEPM",
                newName: "IX_EncuestasIEPM_IdEmprendedor");

            migrationBuilder.AlterDatabase(
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_creacion",
                table: "usuarios",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<int>(
                name: "id_encuesta",
                table: "resumen_ice",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCalculo",
                table: "ResultadosIEPM",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<decimal>(
                name: "IEPM",
                table: "ResultadosIEPM",
                type: "decimal(5,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Valoracion",
                table: "ResultadosIEPM",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Enunciado",
                table: "PreguntasIEPM",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdCuestionario",
                table: "PreguntasIEPM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdIndicador",
                table: "PreguntasIEPM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdEncuestaIEPM",
                table: "EncuestasIEPM",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAplicacion",
                table: "EncuestasIEPM",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResultadosIEPM",
                table: "ResultadosIEPM",
                column: "IdResultadoIEPM");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Pregunta__754EC09EA2CC1C55",
                table: "PreguntasIEPM",
                column: "IdPregunta");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Encuesta__72EAE9F7AB2B5FBA",
                table: "EncuestasIEPM",
                column: "IdEncuestaIEPM");

            migrationBuilder.CreateTable(
                name: "AccionesMejoraIEPM",
                columns: table => new
                {
                    IdAccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RangoMin = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    RangoMax = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Recomendaciones = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Acciones__9845169B327CDA2F", x => x.IdAccion);
                });

            migrationBuilder.CreateTable(
                name: "CuestionariosIEPM",
                columns: table => new
                {
                    IdCuestionario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Destinatario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Instrucciones = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cuestion__69F3E5579F6DDBF1", x => x.IdCuestionario);
                });

            migrationBuilder.CreateTable(
                name: "DimensionesIEPM",
                columns: table => new
                {
                    IdDimensionIEPM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    Formula = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dimensio__C687E8DEC52A8E53", x => x.IdDimensionIEPM);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasIEPM",
                columns: table => new
                {
                    IdRespuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEncuesta = table.Column<int>(type: "int", nullable: false),
                    IdPregunta = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IdEmprendedor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Respuest__D34801983A29682C", x => x.IdRespuesta);
                    table.ForeignKey(
                        name: "FK__Respuesta__IdEmp__777D8AB1",
                        column: x => x.IdEmprendedor,
                        principalTable: "emprendedores",
                        principalColumn: "id_emprendedor");
                    table.ForeignKey(
                        name: "FK__Respuesta__IdEnc__756D6ECB",
                        column: x => x.IdEncuesta,
                        principalTable: "EncuestasIEPM",
                        principalColumn: "IdEncuestaIEPM");
                    table.ForeignKey(
                        name: "FK__Respuesta__IdPre__76619304",
                        column: x => x.IdPregunta,
                        principalTable: "PreguntasIEPM",
                        principalColumn: "IdPregunta");
                });

            migrationBuilder.CreateTable(
                name: "ResultadosAccionesIEPM",
                columns: table => new
                {
                    IdResultadoAccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdResultadoIepm = table.Column<int>(type: "int", nullable: false),
                    IdAccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadosAccionesIEPM", x => x.IdResultadoAccion);
                    table.ForeignKey(
                        name: "FK_ResultadosAcciones_AccionesMejora",
                        column: x => x.IdAccion,
                        principalTable: "AccionesMejoraIEPM",
                        principalColumn: "IdAccion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultadosAcciones_ResultadosIEPM",
                        column: x => x.IdResultadoIepm,
                        principalTable: "ResultadosIEPM",
                        principalColumn: "IdResultadoIEPM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndicadoresIEPM",
                columns: table => new
                {
                    IdIndicador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDimension = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Formula = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(5,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Indicado__F912C4F19D1724C2", x => x.IdIndicador);
                    table.ForeignKey(
                        name: "FK__Indicador__IdDim__6AEFE058",
                        column: x => x.IdDimension,
                        principalTable: "DimensionesIEPM",
                        principalColumn: "IdDimensionIEPM");
                });

            migrationBuilder.CreateTable(
                name: "ResultadosDimensionesIEPM",
                columns: table => new
                {
                    IdResultadoDimension = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEncuesta = table.Column<int>(type: "int", nullable: false),
                    IdDimension = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    FechaCalculo = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Resultad__35BDC62F3509C98E", x => x.IdResultadoDimension);
                    table.ForeignKey(
                        name: "FK__Resultado__IdDim__05A3D694",
                        column: x => x.IdDimension,
                        principalTable: "DimensionesIEPM",
                        principalColumn: "IdDimensionIEPM");
                    table.ForeignKey(
                        name: "FK__Resultado__IdEnc__04AFB25B",
                        column: x => x.IdEncuesta,
                        principalTable: "EncuestasIEPM",
                        principalColumn: "IdEncuestaIEPM");
                });

            migrationBuilder.CreateTable(
                name: "CriteriosEvaluacionIEPM",
                columns: table => new
                {
                    IdCriterio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdIndicador = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Criterio__7139448222A73291", x => x.IdCriterio);
                    table.ForeignKey(
                        name: "FK__Criterios__IdInd__7A3223E8",
                        column: x => x.IdIndicador,
                        principalTable: "IndicadoresIEPM",
                        principalColumn: "IdIndicador");
                });

            migrationBuilder.CreateTable(
                name: "ResultadosIndicadoresIEPM",
                columns: table => new
                {
                    IdResultadoIndicador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEncuesta = table.Column<int>(type: "int", nullable: false),
                    IdIndicador = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    FechaCalculo = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Resultad__038B095E94CDFF22", x => x.IdResultadoIndicador);
                    table.ForeignKey(
                        name: "FK__Resultado__IdEnc__7EF6D905",
                        column: x => x.IdEncuesta,
                        principalTable: "EncuestasIEPM",
                        principalColumn: "IdEncuestaIEPM");
                    table.ForeignKey(
                        name: "FK__Resultado__IdInd__7FEAFD3E",
                        column: x => x.IdIndicador,
                        principalTable: "IndicadoresIEPM",
                        principalColumn: "IdIndicador");
                });

            migrationBuilder.CreateIndex(
                name: "IX_resumen_ice_id_emprendedor",
                table: "resumen_ice",
                column: "id_emprendedor");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasIEPM_IdCuestionario",
                table: "PreguntasIEPM",
                column: "IdCuestionario");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasIEPM_IdIndicador",
                table: "PreguntasIEPM",
                column: "IdIndicador");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriosEvaluacionIEPM_IdIndicador",
                table: "CriteriosEvaluacionIEPM",
                column: "IdIndicador");

            migrationBuilder.CreateIndex(
                name: "IX_IndicadoresIEPM_IdDimension",
                table: "IndicadoresIEPM",
                column: "IdDimension");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasIEPM_IdEmprendedor",
                table: "RespuestasIEPM",
                column: "IdEmprendedor");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasIEPM_IdEncuesta",
                table: "RespuestasIEPM",
                column: "IdEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasIEPM_IdPregunta",
                table: "RespuestasIEPM",
                column: "IdPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosAccionesIEPM_IdAccion",
                table: "ResultadosAccionesIEPM",
                column: "IdAccion");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosAccionesIEPM_IdResultadoIepm",
                table: "ResultadosAccionesIEPM",
                column: "IdResultadoIepm");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosDimensionesIEPM_IdDimension",
                table: "ResultadosDimensionesIEPM",
                column: "IdDimension");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosDimensionesIEPM_IdEncuesta",
                table: "ResultadosDimensionesIEPM",
                column: "IdEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosIndicadoresIEPM_IdEncuesta",
                table: "ResultadosIndicadoresIEPM",
                column: "IdEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosIndicadoresIEPM_IdIndicador",
                table: "ResultadosIndicadoresIEPM",
                column: "IdIndicador");

            migrationBuilder.AddForeignKey(
                name: "FK__Encuestas__IdEmp__634EBE90",
                table: "EncuestasIEPM",
                column: "IdEmprendedor",
                principalTable: "emprendedores",
                principalColumn: "id_emprendedor");

            migrationBuilder.AddForeignKey(
                name: "FK__Preguntas__IdCue__70A8B9AE",
                table: "PreguntasIEPM",
                column: "IdCuestionario",
                principalTable: "CuestionariosIEPM",
                principalColumn: "IdCuestionario");

            migrationBuilder.AddForeignKey(
                name: "FK__Preguntas__IdInd__719CDDE7",
                table: "PreguntasIEPM",
                column: "IdIndicador",
                principalTable: "IndicadoresIEPM",
                principalColumn: "IdIndicador");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultadosIEPM_Emprendedores",
                table: "ResultadosIEPM",
                column: "IdEmprendedor",
                principalTable: "emprendedores",
                principalColumn: "id_emprendedor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultadosIEPM_Encuestas",
                table: "ResultadosIEPM",
                column: "IdEncuesta",
                principalTable: "EncuestasIEPM",
                principalColumn: "IdEncuestaIEPM",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Encuestas__IdEmp__634EBE90",
                table: "EncuestasIEPM");

            migrationBuilder.DropForeignKey(
                name: "FK__Preguntas__IdCue__70A8B9AE",
                table: "PreguntasIEPM");

            migrationBuilder.DropForeignKey(
                name: "FK__Preguntas__IdInd__719CDDE7",
                table: "PreguntasIEPM");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultadosIEPM_Emprendedores",
                table: "ResultadosIEPM");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultadosIEPM_Encuestas",
                table: "ResultadosIEPM");

            migrationBuilder.DropTable(
                name: "CriteriosEvaluacionIEPM");

            migrationBuilder.DropTable(
                name: "CuestionariosIEPM");

            migrationBuilder.DropTable(
                name: "RespuestasIEPM");

            migrationBuilder.DropTable(
                name: "ResultadosAccionesIEPM");

            migrationBuilder.DropTable(
                name: "ResultadosDimensionesIEPM");

            migrationBuilder.DropTable(
                name: "ResultadosIndicadoresIEPM");

            migrationBuilder.DropTable(
                name: "AccionesMejoraIEPM");

            migrationBuilder.DropTable(
                name: "IndicadoresIEPM");

            migrationBuilder.DropTable(
                name: "DimensionesIEPM");

            migrationBuilder.DropIndex(
                name: "IX_resumen_ice_id_emprendedor",
                table: "resumen_ice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResultadosIEPM",
                table: "ResultadosIEPM");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Pregunta__754EC09EA2CC1C55",
                table: "PreguntasIEPM");

            migrationBuilder.DropIndex(
                name: "IX_PreguntasIEPM_IdCuestionario",
                table: "PreguntasIEPM");

            migrationBuilder.DropIndex(
                name: "IX_PreguntasIEPM_IdIndicador",
                table: "PreguntasIEPM");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Encuesta__72EAE9F7AB2B5FBA",
                table: "EncuestasIEPM");

            migrationBuilder.DropColumn(
                name: "FechaCalculo",
                table: "ResultadosIEPM");

            migrationBuilder.DropColumn(
                name: "IEPM",
                table: "ResultadosIEPM");

            migrationBuilder.DropColumn(
                name: "Valoracion",
                table: "ResultadosIEPM");

            migrationBuilder.DropColumn(
                name: "Enunciado",
                table: "PreguntasIEPM");

            migrationBuilder.DropColumn(
                name: "IdCuestionario",
                table: "PreguntasIEPM");

            migrationBuilder.DropColumn(
                name: "IdIndicador",
                table: "PreguntasIEPM");

            migrationBuilder.DropColumn(
                name: "FechaAplicacion",
                table: "EncuestasIEPM");

            migrationBuilder.RenameTable(
                name: "ResultadosIEPM",
                newName: "resultados_iepm");

            migrationBuilder.RenameTable(
                name: "PreguntasIEPM",
                newName: "preguntas_iepm");

            migrationBuilder.RenameTable(
                name: "EncuestasIEPM",
                newName: "encuestas_iepm");

            migrationBuilder.RenameColumn(
                name: "IdEmprendedor",
                table: "resultados_iepm",
                newName: "id_emprendedor");

            migrationBuilder.RenameColumn(
                name: "IdEncuesta",
                table: "resultados_iepm",
                newName: "id_dimension");

            migrationBuilder.RenameColumn(
                name: "IdResultadoIEPM",
                table: "resultados_iepm",
                newName: "id_resultado");

            migrationBuilder.RenameIndex(
                name: "IX_ResultadosIEPM_IdEncuesta",
                table: "resultados_iepm",
                newName: "id_dimension");

            migrationBuilder.RenameIndex(
                name: "IX_ResultadosIEPM_IdEmprendedor",
                table: "resultados_iepm",
                newName: "id_emprendedor");

            migrationBuilder.RenameColumn(
                name: "IdPregunta",
                table: "preguntas_iepm",
                newName: "id_pregunta");

            migrationBuilder.RenameColumn(
                name: "Orden",
                table: "preguntas_iepm",
                newName: "id_dimension");

            migrationBuilder.RenameColumn(
                name: "IdEmprendedor",
                table: "encuestas_iepm",
                newName: "id_emprendedor");

            migrationBuilder.RenameColumn(
                name: "IdEncuestaIEPM",
                table: "encuestas_iepm",
                newName: "valor_respuesta");

            migrationBuilder.RenameIndex(
                name: "IX_EncuestasIEPM_IdEmprendedor",
                table: "encuestas_iepm",
                newName: "id_emprendedor");

            migrationBuilder.AlterDatabase(
                collation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_creacion",
                table: "usuarios",
                type: "datetime",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<int>(
                name: "id_encuesta",
                table: "resumen_ice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AddColumn<decimal>(
                name: "puntuacion_dimension",
                table: "resultados_iepm",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "texto_pregunta",
                table: "preguntas_iepm",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "valor_respuesta",
                table: "encuestas_iepm",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "id_respuesta",
                table: "encuestas_iepm",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "id_pregunta",
                table: "encuestas_iepm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_resumen_ice",
                table: "resumen_ice",
                column: "id_emprendedor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_resultados_iepm",
                table: "resultados_iepm",
                column: "id_resultado");

            migrationBuilder.AddPrimaryKey(
                name: "PK_preguntas_iepm",
                table: "preguntas_iepm",
                column: "id_pregunta");

            migrationBuilder.AddPrimaryKey(
                name: "PK_encuestas_iepm",
                table: "encuestas_iepm",
                column: "id_respuesta");

            migrationBuilder.CreateTable(
                name: "accionesIEPM",
                columns: table => new
                {
                    id_accionesIEPM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rango = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    valoracion = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accionesIEPM", x => x.id_accionesIEPM);
                });

            migrationBuilder.CreateTable(
                name: "dimensiones",
                columns: table => new
                {
                    id_dimension = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_dimension = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    peso_relativo = table.Column<decimal>(type: "decimal(6,5)", precision: 6, scale: 5, nullable: false),
                    puntos_maximos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dimensiones", x => x.id_dimension);
                });

            migrationBuilder.CreateTable(
                name: "resumen_iepm",
                columns: table => new
                {
                    id_emprendedor = table.Column<int>(type: "int", nullable: false),
                    id_accionesIEPM = table.Column<int>(type: "int", nullable: true),
                    valor_iepm_total = table.Column<decimal>(type: "decimal(6,5)", precision: 6, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_resumen_iepm", x => x.id_emprendedor);
                    table.ForeignKey(
                        name: "acciones_iepm_ibfk_2",
                        column: x => x.id_accionesIEPM,
                        principalTable: "accionesIEPM",
                        principalColumn: "id_accionesIEPM");
                    table.ForeignKey(
                        name: "resumen_iepm_ibfk_1",
                        column: x => x.id_emprendedor,
                        principalTable: "emprendedores",
                        principalColumn: "id_emprendedor");
                });

            migrationBuilder.CreateIndex(
                name: "id_dimension",
                table: "preguntas_iepm",
                column: "id_dimension");

            migrationBuilder.CreateIndex(
                name: "id_pregunta",
                table: "encuestas_iepm",
                column: "id_pregunta");

            migrationBuilder.CreateIndex(
                name: "acciones_iepm_ibfk_2",
                table: "resumen_iepm",
                column: "id_accionesIEPM");

            migrationBuilder.AddForeignKey(
                name: "encuestas_iepm_ibfk_1",
                table: "encuestas_iepm",
                column: "id_emprendedor",
                principalTable: "emprendedores",
                principalColumn: "id_emprendedor");

            migrationBuilder.AddForeignKey(
                name: "encuestas_iepm_ibfk_2",
                table: "encuestas_iepm",
                column: "id_pregunta",
                principalTable: "preguntas_iepm",
                principalColumn: "id_pregunta");

            migrationBuilder.AddForeignKey(
                name: "preguntas_iepm_ibfk_1",
                table: "preguntas_iepm",
                column: "id_dimension",
                principalTable: "dimensiones",
                principalColumn: "id_dimension");

            migrationBuilder.AddForeignKey(
                name: "resultados_iepm_ibfk_1",
                table: "resultados_iepm",
                column: "id_emprendedor",
                principalTable: "emprendedores",
                principalColumn: "id_emprendedor");

            migrationBuilder.AddForeignKey(
                name: "resultados_iepm_ibfk_2",
                table: "resultados_iepm",
                column: "id_dimension",
                principalTable: "dimensiones",
                principalColumn: "id_dimension");
        }
    }
}

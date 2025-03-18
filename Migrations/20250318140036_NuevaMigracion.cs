using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class NuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "competencias",
                columns: table => new
                {
                    id_competencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_competencia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    puntos_maximos = table.Column<int>(type: "int", nullable: false),
                    peso_relativo = table.Column<decimal>(type: "decimal(6,5)", precision: 6, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_competencias", x => x.id_competencia);
                });

            migrationBuilder.CreateTable(
                name: "dimensiones",
                columns: table => new
                {
                    id_dimension = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_dimension = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    puntos_maximos = table.Column<int>(type: "int", nullable: false),
                    peso_relativo = table.Column<decimal>(type: "decimal(6,5)", precision: 6, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dimensiones", x => x.id_dimension);
                });

            migrationBuilder.CreateTable(
                name: "indicadores",
                columns: table => new
                {
                    id_indicadores = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rango = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    valoracion = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    nivel_desarrollo = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    acciones = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_indicadores", x => x.id_indicadores);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    tipo_usuario = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    fecha_creacion = table.Column<byte[]>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "preguntas_ice",
                columns: table => new
                {
                    id_pregunta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_competencia = table.Column<int>(type: "int", nullable: false),
                    texto_pregunta = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preguntas_ice", x => x.id_pregunta);
                    table.ForeignKey(
                        name: "preguntas_ice_ibfk_1",
                        column: x => x.id_competencia,
                        principalTable: "competencias",
                        principalColumn: "id_competencia");
                });

            migrationBuilder.CreateTable(
                name: "preguntas_iepm",
                columns: table => new
                {
                    id_pregunta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_dimension = table.Column<int>(type: "int", nullable: false),
                    texto_pregunta = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preguntas_iepm", x => x.id_pregunta);
                    table.ForeignKey(
                        name: "preguntas_iepm_ibfk_1",
                        column: x => x.id_dimension,
                        principalTable: "dimensiones",
                        principalColumn: "id_dimension");
                });

            migrationBuilder.CreateTable(
                name: "emprendedores",
                columns: table => new
                {
                    id_emprendedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    edad = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    nivel_estudio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    trabajo_relacion_dependencia = table.Column<bool>(type: "bit", nullable: false),
                    sueldo_mensual = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ruc = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    empleados_hombres = table.Column<int>(type: "int", nullable: false),
                    empleados_mujeres = table.Column<int>(type: "int", nullable: false),
                    rango_edad_empleados = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    tipo_empresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ano_creacion_empresa = table.Column<short>(type: "smallint", nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cedula = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_emprendedores", x => x.id_emprendedor);
                    table.ForeignKey(
                        name: "emprendedores_ibfk_1",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "datos_emp",
                columns: table => new
                {
                    id_datos_emp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nivel_ed_alc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    rango_edad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    negocios_emp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    titulos_alc = table.Column<string>(type: "text", nullable: false),
                    provincia = table.Column<string>(type: "text", nullable: false),
                    id_emprendedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datos_emp", x => x.id_datos_emp);
                    table.ForeignKey(
                        name: "fk_id_emprendedor",
                        column: x => x.id_emprendedor,
                        principalTable: "emprendedores",
                        principalColumn: "id_emprendedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "encuestas_ice",
                columns: table => new
                {
                    id_respuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_emprendedor = table.Column<int>(type: "int", nullable: false),
                    id_pregunta = table.Column<int>(type: "int", nullable: false),
                    valor_respuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_encuestas_ice", x => x.id_respuesta);
                    table.ForeignKey(
                        name: "encuestas_ice_ibfk_1",
                        column: x => x.id_emprendedor,
                        principalTable: "emprendedores",
                        principalColumn: "id_emprendedor");
                    table.ForeignKey(
                        name: "encuestas_ice_ibfk_2",
                        column: x => x.id_pregunta,
                        principalTable: "preguntas_ice",
                        principalColumn: "id_pregunta");
                });

            migrationBuilder.CreateTable(
                name: "encuestas_iepm",
                columns: table => new
                {
                    id_respuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_emprendedor = table.Column<int>(type: "int", nullable: false),
                    id_pregunta = table.Column<int>(type: "int", nullable: false),
                    valor_respuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_encuestas_iepm", x => x.id_respuesta);
                    table.ForeignKey(
                        name: "encuestas_iepm_ibfk_1",
                        column: x => x.id_emprendedor,
                        principalTable: "emprendedores",
                        principalColumn: "id_emprendedor");
                    table.ForeignKey(
                        name: "encuestas_iepm_ibfk_2",
                        column: x => x.id_pregunta,
                        principalTable: "preguntas_iepm",
                        principalColumn: "id_pregunta");
                });

            migrationBuilder.CreateTable(
                name: "resultados_ice",
                columns: table => new
                {
                    id_resultado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_emprendedor = table.Column<int>(type: "int", nullable: false),
                    id_competencia = table.Column<int>(type: "int", nullable: false),
                    valoracion = table.Column<int>(type: "int", nullable: false),
                    puntuacion_competencia = table.Column<decimal>(type: "decimal(6,5)", precision: 6, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resultados_ice", x => x.id_resultado);
                    table.ForeignKey(
                        name: "resultados_ice_ibfk_1",
                        column: x => x.id_emprendedor,
                        principalTable: "emprendedores",
                        principalColumn: "id_emprendedor");
                    table.ForeignKey(
                        name: "resultados_ice_ibfk_2",
                        column: x => x.id_competencia,
                        principalTable: "competencias",
                        principalColumn: "id_competencia");
                });

            migrationBuilder.CreateTable(
                name: "resultados_iepm",
                columns: table => new
                {
                    id_resultado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_emprendedor = table.Column<int>(type: "int", nullable: false),
                    id_dimension = table.Column<int>(type: "int", nullable: false),
                    puntuacion_dimension = table.Column<decimal>(type: "decimal(6,5)", precision: 6, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resultados_iepm", x => x.id_resultado);
                    table.ForeignKey(
                        name: "resultados_iepm_ibfk_1",
                        column: x => x.id_emprendedor,
                        principalTable: "emprendedores",
                        principalColumn: "id_emprendedor");
                    table.ForeignKey(
                        name: "resultados_iepm_ibfk_2",
                        column: x => x.id_dimension,
                        principalTable: "dimensiones",
                        principalColumn: "id_dimension");
                });

            migrationBuilder.CreateTable(
                name: "resumen_ice",
                columns: table => new
                {
                    id_emprendedor = table.Column<int>(type: "int", nullable: false),
                    valor_ice_total = table.Column<decimal>(type: "decimal(6,5)", precision: 6, scale: 5, nullable: false),
                    id_indicadores = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resumen_ice", x => x.id_emprendedor);
                    table.ForeignKey(
                        name: "id_indicador_ice_ibfk_2",
                        column: x => x.id_indicadores,
                        principalTable: "indicadores",
                        principalColumn: "id_indicadores");
                    table.ForeignKey(
                        name: "resumen_ice_ibfk_1",
                        column: x => x.id_emprendedor,
                        principalTable: "emprendedores",
                        principalColumn: "id_emprendedor");
                });

            migrationBuilder.CreateTable(
                name: "resumen_iepm",
                columns: table => new
                {
                    id_emprendedor = table.Column<int>(type: "int", nullable: false),
                    valor_iepm_total = table.Column<decimal>(type: "decimal(6,5)", precision: 6, scale: 5, nullable: false),
                    id_accionesIEPM = table.Column<int>(type: "int", nullable: true)
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
                name: "fk_id_emprendedor",
                table: "datos_emp",
                column: "id_emprendedor");

            migrationBuilder.CreateIndex(
                name: "cedula",
                table: "emprendedores",
                column: "cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "correo",
                table: "emprendedores",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_usuario",
                table: "emprendedores",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "ruc",
                table: "emprendedores",
                column: "ruc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_emprendedor",
                table: "encuestas_ice",
                column: "id_emprendedor");

            migrationBuilder.CreateIndex(
                name: "id_pregunta",
                table: "encuestas_ice",
                column: "id_pregunta");

            migrationBuilder.CreateIndex(
                name: "id_emprendedor",
                table: "encuestas_iepm",
                column: "id_emprendedor");

            migrationBuilder.CreateIndex(
                name: "id_pregunta",
                table: "encuestas_iepm",
                column: "id_pregunta");

            migrationBuilder.CreateIndex(
                name: "id_competencia",
                table: "preguntas_ice",
                column: "id_competencia");

            migrationBuilder.CreateIndex(
                name: "id_dimension",
                table: "preguntas_iepm",
                column: "id_dimension");

            migrationBuilder.CreateIndex(
                name: "id_competencia",
                table: "resultados_ice",
                column: "id_competencia");

            migrationBuilder.CreateIndex(
                name: "id_emprendedor",
                table: "resultados_ice",
                column: "id_emprendedor");

            migrationBuilder.CreateIndex(
                name: "id_dimension",
                table: "resultados_iepm",
                column: "id_dimension");

            migrationBuilder.CreateIndex(
                name: "id_emprendedor",
                table: "resultados_iepm",
                column: "id_emprendedor");

            migrationBuilder.CreateIndex(
                name: "id_indicador_ice_ibfk_2",
                table: "resumen_ice",
                column: "id_indicadores");

            migrationBuilder.CreateIndex(
                name: "acciones_iepm_ibfk_2",
                table: "resumen_iepm",
                column: "id_accionesIEPM");

            migrationBuilder.CreateIndex(
                name: "correo",
                table: "usuarios",
                column: "correo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datos_emp");

            migrationBuilder.DropTable(
                name: "encuestas_ice");

            migrationBuilder.DropTable(
                name: "encuestas_iepm");

            migrationBuilder.DropTable(
                name: "resultados_ice");

            migrationBuilder.DropTable(
                name: "resultados_iepm");

            migrationBuilder.DropTable(
                name: "resumen_ice");

            migrationBuilder.DropTable(
                name: "resumen_iepm");

            migrationBuilder.DropTable(
                name: "preguntas_ice");

            migrationBuilder.DropTable(
                name: "preguntas_iepm");

            migrationBuilder.DropTable(
                name: "indicadores");

            migrationBuilder.DropTable(
                name: "accionesIEPM");

            migrationBuilder.DropTable(
                name: "emprendedores");

            migrationBuilder.DropTable(
                name: "competencias");

            migrationBuilder.DropTable(
                name: "dimensiones");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}

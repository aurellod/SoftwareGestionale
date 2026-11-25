using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gestionale.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreazioneIniziale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categoria", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "ubicazione",
                columns: table => new
                {
                    id_ubicazione = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_ubicazione = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    tipo_ubicazione = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    targa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    attivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ubicazione", x => x.id_ubicazione);
                });

            migrationBuilder.CreateTable(
                name: "articolo",
                columns: table => new
                {
                    id_articolo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_articolo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    id_categoria = table.Column<int>(type: "integer", nullable: false),
                    unita_misura = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, defaultValue: "pz"),
                    codice_articolo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    attivo = table.Column<bool>(type: "boolean", nullable: false),
                    data_creazione = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_articolo", x => x.id_articolo);
                    table.ForeignKey(
                        name: "fk_articolo_categoria_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "categoria",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "giacenza",
                columns: table => new
                {
                    id_articolo = table.Column<int>(type: "integer", nullable: false),
                    id_ubicazione = table.Column<int>(type: "integer", nullable: false),
                    quantita = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    soglia_minima = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    stato_riordino = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    data_aggiornamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_giacenza", x => new { x.id_articolo, x.id_ubicazione });
                    table.CheckConstraint("ck_giacenza_quantita", "quantita >= 0");
                    table.CheckConstraint("ck_giacenza_soglia_minima", "soglia_minima >= 0");
                    table.ForeignKey(
                        name: "fk_giacenza_articolo_id_articolo",
                        column: x => x.id_articolo,
                        principalTable: "articolo",
                        principalColumn: "id_articolo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_giacenza_ubicazione_id_ubicazione",
                        column: x => x.id_ubicazione,
                        principalTable: "ubicazione",
                        principalColumn: "id_ubicazione",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "movimento",
                columns: table => new
                {
                    id_movimento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_movimento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    id_articolo = table.Column<int>(type: "integer", nullable: false),
                    quantita = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    id_ubicazione_origine = table.Column<int>(type: "integer", nullable: true),
                    id_ubicazione_destinazione = table.Column<int>(type: "integer", nullable: true),
                    data_movimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movimento", x => x.id_movimento);
                    table.CheckConstraint("ck_movimento_direzione", "(tipo_movimento = 'Carico' AND id_ubicazione_origine IS NULL AND id_ubicazione_destinazione IS NOT NULL) OR (tipo_movimento = 'Uso' AND id_ubicazione_origine IS NOT NULL AND id_ubicazione_destinazione IS NULL) OR (tipo_movimento = 'Trasferimento' AND id_ubicazione_origine IS NOT NULL AND id_ubicazione_destinazione IS NOT NULL) OR (tipo_movimento = 'Correzione' AND (id_ubicazione_origine IS NULL) <> (id_ubicazione_destinazione IS NULL))");
                    table.CheckConstraint("ck_movimento_origine_destinazione", "id_ubicazione_origine <> id_ubicazione_destinazione");
                    table.CheckConstraint("ck_movimento_quantita", "quantita > 0");
                    table.ForeignKey(
                        name: "fk_movimento_articolo_id_articolo",
                        column: x => x.id_articolo,
                        principalTable: "articolo",
                        principalColumn: "id_articolo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_movimento_ubicazione_id_ubicazione_destinazione",
                        column: x => x.id_ubicazione_destinazione,
                        principalTable: "ubicazione",
                        principalColumn: "id_ubicazione",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_movimento_ubicazione_id_ubicazione_origine",
                        column: x => x.id_ubicazione_origine,
                        principalTable: "ubicazione",
                        principalColumn: "id_ubicazione",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_articolo_id_categoria",
                table: "articolo",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "ix_categoria_nome_categoria",
                table: "categoria",
                column: "nome_categoria",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_giacenza_id_ubicazione",
                table: "giacenza",
                column: "id_ubicazione");

            migrationBuilder.CreateIndex(
                name: "ix_movimento_id_articolo",
                table: "movimento",
                column: "id_articolo");

            migrationBuilder.CreateIndex(
                name: "ix_movimento_id_ubicazione_destinazione",
                table: "movimento",
                column: "id_ubicazione_destinazione");

            migrationBuilder.CreateIndex(
                name: "ix_movimento_id_ubicazione_origine",
                table: "movimento",
                column: "id_ubicazione_origine");

            migrationBuilder.CreateIndex(
                name: "ix_ubicazione_nome_ubicazione",
                table: "ubicazione",
                column: "nome_ubicazione",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "giacenza");

            migrationBuilder.DropTable(
                name: "movimento");

            migrationBuilder.DropTable(
                name: "articolo");

            migrationBuilder.DropTable(
                name: "ubicazione");

            migrationBuilder.DropTable(
                name: "categoria");
        }
    }
}

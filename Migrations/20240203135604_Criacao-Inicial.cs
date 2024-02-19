using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvc_lives.Migrations
{
    public partial class CriacaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inscrito",
                columns: table => new
                {
                    InscritoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoInstagram = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscrito", x => x.InscritoID);
                });

            migrationBuilder.CreateTable(
                name: "Instrutor",
                columns: table => new
                {
                    InstrutorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoInstagram = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrutor", x => x.InstrutorID);
                });

            migrationBuilder.CreateTable(
                name: "Live",
                columns: table => new
                {
                    LiveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrutorID = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descicao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuracaoMin = table.Column<int>(type: "int", nullable: false),
                    ValorInscricao = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Live", x => x.LiveID);
                    table.ForeignKey(
                        name: "FK_Live_Instrutor_InstrutorID",
                        column: x => x.InstrutorID,
                        principalTable: "Instrutor",
                        principalColumn: "InstrutorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscricao",
                columns: table => new
                {
                    InscricaoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscritoID = table.Column<int>(type: "int", nullable: false),
                    LiveID = table.Column<int>(type: "int", nullable: false),
                    ValorInscricao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusPagamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricao", x => x.InscricaoID);
                    table.ForeignKey(
                        name: "FK_Inscricao_Inscrito_InscritoID",
                        column: x => x.InscritoID,
                        principalTable: "Inscrito",
                        principalColumn: "InscritoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscricao_Live_LiveID",
                        column: x => x.LiveID,
                        principalTable: "Live",
                        principalColumn: "LiveID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_InscritoID",
                table: "Inscricao",
                column: "InscritoID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_LiveID",
                table: "Inscricao",
                column: "LiveID");

            migrationBuilder.CreateIndex(
                name: "IX_Live_InstrutorID",
                table: "Live",
                column: "InstrutorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscricao");

            migrationBuilder.DropTable(
                name: "Inscrito");

            migrationBuilder.DropTable(
                name: "Live");

            migrationBuilder.DropTable(
                name: "Instrutor");
        }
    }
}

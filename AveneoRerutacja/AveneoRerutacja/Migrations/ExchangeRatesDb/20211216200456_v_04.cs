using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AveneoRerutacja.Migrations.ExchangeRatesDb
{
    public partial class v_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyDailyRate");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.AddColumn<int>(
                name: "SourceCurrencyId",
                table: "DailyRates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetCurrencyId",
                table: "DailyRates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SourceCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceCurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TargetCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetCurrencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyRates_SourceCurrencyId",
                table: "DailyRates",
                column: "SourceCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRates_TargetCurrencyId",
                table: "DailyRates",
                column: "TargetCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyRates_SourceCurrencies_SourceCurrencyId",
                table: "DailyRates",
                column: "SourceCurrencyId",
                principalTable: "SourceCurrencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyRates_TargetCurrencies_TargetCurrencyId",
                table: "DailyRates",
                column: "TargetCurrencyId",
                principalTable: "TargetCurrencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyRates_SourceCurrencies_SourceCurrencyId",
                table: "DailyRates");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyRates_TargetCurrencies_TargetCurrencyId",
                table: "DailyRates");

            migrationBuilder.DropTable(
                name: "SourceCurrencies");

            migrationBuilder.DropTable(
                name: "TargetCurrencies");

            migrationBuilder.DropIndex(
                name: "IX_DailyRates_SourceCurrencyId",
                table: "DailyRates");

            migrationBuilder.DropIndex(
                name: "IX_DailyRates_TargetCurrencyId",
                table: "DailyRates");

            migrationBuilder.DropColumn(
                name: "SourceCurrencyId",
                table: "DailyRates");

            migrationBuilder.DropColumn(
                name: "TargetCurrencyId",
                table: "DailyRates");

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyDailyRate",
                columns: table => new
                {
                    CurrenciesId = table.Column<int>(type: "integer", nullable: false),
                    DailyRatesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyDailyRate", x => new { x.CurrenciesId, x.DailyRatesId });
                    table.ForeignKey(
                        name: "FK_CurrencyDailyRate_Currencies_CurrenciesId",
                        column: x => x.CurrenciesId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyDailyRate_DailyRates_DailyRatesId",
                        column: x => x.DailyRatesId,
                        principalTable: "DailyRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyDailyRate_DailyRatesId",
                table: "CurrencyDailyRate",
                column: "DailyRatesId");
        }
    }
}

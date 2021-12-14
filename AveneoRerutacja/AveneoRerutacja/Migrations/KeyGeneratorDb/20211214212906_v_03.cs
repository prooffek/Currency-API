using Microsoft.EntityFrameworkCore.Migrations;

namespace AveneoRerutacja.Migrations.KeyGeneratorDb
{
    public partial class v_03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyValue",
                table: "Keys",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyValue",
                table: "Keys");
        }
    }
}

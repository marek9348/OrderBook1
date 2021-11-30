using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderBook1.Migrations
{
    public partial class ClientCompareNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompareName",
                table: "Clients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompareName",
                table: "Clients",
                column: "CompareName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_CompareName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CompareName",
                table: "Clients");
        }
    }
}

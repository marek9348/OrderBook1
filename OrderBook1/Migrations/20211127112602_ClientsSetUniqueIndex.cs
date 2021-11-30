using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderBook1.Migrations
{
    public partial class ClientsSetUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clients_Name",
                table: "Clients",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_Name",
                table: "Clients");
        }
    }
}

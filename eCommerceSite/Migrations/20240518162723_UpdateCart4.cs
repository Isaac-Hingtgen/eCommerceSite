using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceSite.Migrations
{
    public partial class UpdateCart4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_ProfileId",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "Profiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CartId",
                table: "Profiles",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProfileId",
                table: "Carts",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Carts_CartId",
                table: "Profiles",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Carts_CartId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_CartId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProfileId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Profiles");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProfileId",
                table: "Carts",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceSite.Migrations
{
    public partial class UpdateCart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Profiles_ProfileId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProfileId",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProfileId",
                table: "Carts",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Profiles_ProfileId",
                table: "Carts",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Profiles_ProfileId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProfileId",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProfileId",
                table: "Carts",
                column: "ProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Profiles_ProfileId",
                table: "Carts",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

namespace LiverpoolFanSite.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UpdateSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "Players",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_AddedByUserId",
                table: "Players",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_AspNetUsers_AddedByUserId",
                table: "Players",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_AspNetUsers_AddedByUserId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_AddedByUserId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Players");
        }
    }
}

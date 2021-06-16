namespace LiverpoolFanSite.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeStadiumTourModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StadiumTours_AspNetUsers_UserId1",
                table: "StadiumTours");

            migrationBuilder.DropIndex(
                name: "IX_StadiumTours_UserId1",
                table: "StadiumTours");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StadiumTours");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "StadiumTours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StadiumTours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "StadiumTours",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StadiumTours_UserId1",
                table: "StadiumTours",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumTours_AspNetUsers_UserId1",
                table: "StadiumTours",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

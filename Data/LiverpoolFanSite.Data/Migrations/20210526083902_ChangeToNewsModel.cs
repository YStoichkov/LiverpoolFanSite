namespace LiverpoolFanSite.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeToNewsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "News",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_AddedByUserId",
                table: "News",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_NewsId",
                table: "Images",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_News_NewsId",
                table: "Images",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_AddedByUserId",
                table: "News",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_News_NewsId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_AddedByUserId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_AddedByUserId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Images_NewsId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

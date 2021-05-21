namespace LiverpoolFanSite.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SomeChangesToColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "ContactForms",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ContactForms",
                newName: "Phone");
        }
    }
}

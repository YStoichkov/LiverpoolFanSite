﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace LiverpoolFanSite.Data.Migrations
{
    public partial class ChangeToStadiumTourModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "StadiumTours",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "StadiumTours");
        }
    }
}

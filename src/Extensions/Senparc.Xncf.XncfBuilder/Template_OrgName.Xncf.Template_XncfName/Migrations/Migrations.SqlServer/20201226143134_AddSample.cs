﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Template_OrgName.Xncf.Template_XncfName.Migrations.Migrations.SqlServer
{
    public partial class AddSample : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Template_OrgName_Template_XncfName_Color",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Flag = table.Column<bool>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    AdminRemark = table.Column<string>(maxLength: 300, nullable: true),
                    Remark = table.Column<string>(maxLength: 300, nullable: true),
                    Red = table.Column<int>(nullable: false),
                    Green = table.Column<int>(nullable: false),
                    Blue = table.Column<int>(nullable: false),
                    AdditionNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template_OrgName_Template_XncfName_Color", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Template_OrgName_Template_XncfName_Color");
        }
    }
}


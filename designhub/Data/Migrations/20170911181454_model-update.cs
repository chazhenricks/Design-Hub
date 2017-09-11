using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace designhub.Data.Migrations
{
    public partial class modelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FileGroup",
                columns: table => new
                {
                    FileGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileGroup", x => x.FileGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Project_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    FileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    FileGroupID = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_File_FileGroup_FileGroupID",
                        column: x => x.FileGroupID,
                        principalTable: "FileGroup",
                        principalColumn: "FileGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_File_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFileGroup",
                columns: table => new
                {
                    ProjectFileGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    FileGroupID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFileGroup", x => x.ProjectFileGroupID);
                    table.ForeignKey(
                        name: "FK_ProjectFileGroup_FileGroup_FileGroupID",
                        column: x => x.FileGroupID,
                        principalTable: "FileGroup",
                        principalColumn: "FileGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectFileGroup_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileID = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comment_File_FileID",
                        column: x => x.FileID,
                        principalTable: "File",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FileID",
                table: "Comment",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_File_FileGroupID",
                table: "File",
                column: "FileGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_File_UserId",
                table: "File",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId",
                table: "Project",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFileGroup_FileGroupID",
                table: "ProjectFileGroup",
                column: "FileGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFileGroup_ProjectID",
                table: "ProjectFileGroup",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ProjectFileGroup");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "FileGroup");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}

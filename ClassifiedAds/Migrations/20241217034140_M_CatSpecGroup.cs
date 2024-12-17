using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassifiedAds.Migrations
{
    /// <inheritdoc />
    public partial class M_CatSpecGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "CategorySpec",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategorySpecGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    CategorySpecGroupId = table.Column<int>(type: "int", nullable: true),
                    DbEntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Token = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySpecGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySpecGroups_CategorySpecGroups_CategorySpecGroupId",
                        column: x => x.CategorySpecGroupId,
                        principalTable: "CategorySpecGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorySpec_GroupId",
                table: "CategorySpec",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySpecGroups_CategorySpecGroupId",
                table: "CategorySpecGroups",
                column: "CategorySpecGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySpecGroups_Token",
                table: "CategorySpecGroups",
                column: "Token",
                unique: true,
                filter: "[Token] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySpec_CategorySpecGroups_GroupId",
                table: "CategorySpec",
                column: "GroupId",
                principalTable: "CategorySpecGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySpec_CategorySpecGroups_GroupId",
                table: "CategorySpec");

            migrationBuilder.DropTable(
                name: "CategorySpecGroups");

            migrationBuilder.DropIndex(
                name: "IX_CategorySpec_GroupId",
                table: "CategorySpec");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "CategorySpec");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassifiedAds.Migrations
{
    /// <inheritdoc />
    public partial class M_Specs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySpecGroups_CategorySpecGroups_CategorySpecGroupId",
                table: "CategorySpecGroups");

            migrationBuilder.DropIndex(
                name: "IX_CategorySpecGroups_CategorySpecGroupId",
                table: "CategorySpecGroups");

            migrationBuilder.DropColumn(
                name: "CategorySpecGroupId",
                table: "CategorySpecGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategorySpecGroupId",
                table: "CategorySpecGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategorySpecGroups_CategorySpecGroupId",
                table: "CategorySpecGroups",
                column: "CategorySpecGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySpecGroups_CategorySpecGroups_CategorySpecGroupId",
                table: "CategorySpecGroups",
                column: "CategorySpecGroupId",
                principalTable: "CategorySpecGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassifiedAds.Migrations
{
    /// <inheritdoc />
    public partial class M005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Lovs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Lovs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "LovOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LovOptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "CategorySpec",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CategorySpec",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AdvertisementGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AdvertisementGroups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AdSpecValue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AdSpecValue",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AdsImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AdsImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Lovs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lovs");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "LovOptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LovOptions");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "CategorySpec");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CategorySpec");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "AdvertisementGroups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdvertisementGroups");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "AdSpecValue");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdSpecValue");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "AdsImages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdsImages");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Ads");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}

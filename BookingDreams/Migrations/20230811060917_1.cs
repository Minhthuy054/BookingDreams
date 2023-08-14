using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingDreams.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen");

            migrationBuilder.AlterColumn<int>(
                name: "IdQuyen",
                table: "PhanQuyen",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PhanQuyen",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PhanQuyen");

            migrationBuilder.AlterColumn<int>(
                name: "IdQuyen",
                table: "PhanQuyen",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen",
                column: "IdQuyen");
        }
    }
}

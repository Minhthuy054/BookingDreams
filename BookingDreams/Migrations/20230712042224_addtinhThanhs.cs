using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingDreams.Migrations
{
    public partial class addtinhThanhs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TinhThanh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHinhAnh = table.Column<int>(type: "int", nullable: false),
                    MaTinhThanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenTinhThanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhThanh", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinhThanh");
        }
    }
}

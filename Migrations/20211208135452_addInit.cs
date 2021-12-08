using Microsoft.EntityFrameworkCore.Migrations;

namespace _BAITAPLAB05_KT.Migrations
{
    public partial class addInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "diemcachly",
                columns: table => new
                {
                    MaDiemCachLy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenDiemCachLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diemcachly", x => x.MaDiemCachLy);
                });

            migrationBuilder.CreateTable(
                name: "trieuchung",
                columns: table => new
                {
                    MaTrieuChung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenTrieuChung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trieuchung", x => x.MaTrieuChung);
                });

            migrationBuilder.CreateTable(
                name: "congnhan",
                columns: table => new
                {
                    MaCongNhan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenCongNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    NamSinh = table.Column<int>(type: "int", nullable: false),
                    NuocVe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDiemCachLy = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_congnhan", x => x.MaCongNhan);
                    table.ForeignKey(
                        name: "FK_congnhan_diemcachly_MaDiemCachLy",
                        column: x => x.MaDiemCachLy,
                        principalTable: "diemcachly",
                        principalColumn: "MaDiemCachLy",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cn_tc",
                columns: table => new
                {
                    MaCongNhan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaTrieuChung = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cn_tc", x => new { x.MaCongNhan, x.MaTrieuChung });
                    table.ForeignKey(
                        name: "FK_cn_tc_congnhan_MaCongNhan",
                        column: x => x.MaCongNhan,
                        principalTable: "congnhan",
                        principalColumn: "MaCongNhan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cn_tc_trieuchung_MaTrieuChung",
                        column: x => x.MaTrieuChung,
                        principalTable: "trieuchung",
                        principalColumn: "MaTrieuChung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cn_tc_MaTrieuChung",
                table: "cn_tc",
                column: "MaTrieuChung");

            migrationBuilder.CreateIndex(
                name: "IX_congnhan_MaDiemCachLy",
                table: "congnhan",
                column: "MaDiemCachLy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cn_tc");

            migrationBuilder.DropTable(
                name: "congnhan");

            migrationBuilder.DropTable(
                name: "trieuchung");

            migrationBuilder.DropTable(
                name: "diemcachly");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pendaftran_mahasiswa.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddNomorPendaftaranToMahasiswa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomorPendaftaran",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomorPendaftaran",
                table: "Mahasiswas");
        }
    }
}

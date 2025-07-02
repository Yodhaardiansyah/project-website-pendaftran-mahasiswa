using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pendaftran_mahasiswa.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailFieldsToMahasiswa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Foto",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Agama",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlamatAsal",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlamatOrangTua",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlamatSemarang",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsalSekolah",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FakultasDipilih",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GolonganDarah",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JenisKelamin",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JurusanDipilih",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JurusanSMU",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NamaOrangTua",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoHP",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoOrtu",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoTelpRumah",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TahunLulus",
                table: "Mahasiswas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TanggalLahir",
                table: "Mahasiswas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TempatLahir",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agama",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "AlamatAsal",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "AlamatOrangTua",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "AlamatSemarang",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "AsalSekolah",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "FakultasDipilih",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "GolonganDarah",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "JenisKelamin",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "JurusanDipilih",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "JurusanSMU",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "NamaOrangTua",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "NoHP",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "NoOrtu",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "NoTelpRumah",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "TahunLulus",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "TanggalLahir",
                table: "Mahasiswas");

            migrationBuilder.DropColumn(
                name: "TempatLahir",
                table: "Mahasiswas");

            migrationBuilder.AlterColumn<string>(
                name: "Foto",
                table: "Mahasiswas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

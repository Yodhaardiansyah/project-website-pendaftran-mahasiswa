namespace pendaftran_mahasiswa.Server.Models
{
    public class Mahasiswa
    {
        public int Id { get; set; }
        public string? NomorPendaftaran { get; set; }

        // Wajib
        public string NamaLengkap { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Foto { get; set; }

        // Opsional
        public string? AlamatAsal { get; set; }
        public string? AlamatSemarang { get; set; }
        public string? TempatLahir { get; set; }
        public DateTime? TanggalLahir { get; set; }
        public string? Agama { get; set; }
        public string? JenisKelamin { get; set; }
        public string? GolonganDarah { get; set; }
        public string? AsalSekolah { get; set; }
        public string? JurusanSMU { get; set; }
        public int? TahunLulus { get; set; }
        public string? JurusanDipilih { get; set; }
        public string? FakultasDipilih { get; set; }
        public string? NoTelpRumah { get; set; }
        public string? NoHP { get; set; }

        // Orang Tua / Wali
        public string? NamaOrangTua { get; set; }
        public string? AlamatOrangTua { get; set; }
        public string? NoOrtu { get; set; }

        public string Status { get; set; } = "Pending";
        public DateTime TanggalDaftar { get; set; } = DateTime.Now;
    }
}

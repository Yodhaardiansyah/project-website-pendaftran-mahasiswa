using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using pendaftran_mahasiswa.Server.DTOs;
using pendaftran_mahasiswa.Server.Models;
using pendaftran_mahasiswa.Server.Repositories;


namespace pendaftran_mahasiswa.Server.Controllers
{
    [EnableCors("AllowLocalhostVue")]
    [ApiController]
    [Route("api/[controller]")]
    public class MahasiswaController : Controller
    {
        private readonly IMahasiswaRepository _repo;
        private readonly IWebHostEnvironment _env;

        public MahasiswaController(IMahasiswaRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] MahasiswaCreateDto dto)
        {
            // Simpan file
            string uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            string fileName = Guid.NewGuid() + Path.GetExtension(dto.Foto.FileName);
            string filePath = Path.Combine(uploads, fileName);
            // Format nomor pendaftaran: YYYYMMDD-XXX
            string tanggal = DateTime.UtcNow.ToString("yyyyMMdd");

            // Ambil semua mahasiswa dengan nomor di hari yang sama
            var semuaMahasiswa = await _repo.GetAll();
            int countHariIni = semuaMahasiswa
                .Count(m => m.NomorPendaftaran != null && m.NomorPendaftaran.StartsWith(tanggal));

            // Tambahkan urutan
            string nomorUrut = (countHariIni + 1).ToString("D3");

            // Gabungkan
            string nomorPendaftaran = $"{tanggal}-{nomorUrut}";


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Foto.CopyToAsync(stream);
            }

            var mhs = new Mahasiswa
            {
                NomorPendaftaran = nomorPendaftaran,
                NamaLengkap = dto.NamaLengkap,
                Email = dto.Email,
                Password = dto.Password,
                Foto = "/uploads/" + fileName,

                AlamatAsal = dto.AlamatAsal,
                AlamatSemarang = dto.AlamatSemarang,
                TempatLahir = dto.TempatLahir,
                TanggalLahir = dto.TanggalLahir,
                Agama = dto.Agama,
                JenisKelamin = dto.JenisKelamin,
                GolonganDarah = dto.GolonganDarah,
                AsalSekolah = dto.AsalSekolah,
                JurusanSMU = dto.JurusanSMU,
                TahunLulus = dto.TahunLulus,
                JurusanDipilih = dto.JurusanDipilih,
                FakultasDipilih = dto.FakultasDipilih,
                NoTelpRumah = dto.NoTelpRumah,
                NoHP = dto.NoHP,
                NamaOrangTua = dto.NamaOrangTua,
                AlamatOrangTua = dto.AlamatOrangTua,
                NoOrtu = dto.NoOrtu
            };


            await _repo.Add(mhs);
            return Ok(new { message = "Pendaftaran berhasil!" });
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id, [FromQuery] string status)
        {
            var mhs = await _repo.GetById(id);
            if (mhs == null)
                return NotFound();

            if (status != "Approved" && status != "Rejected")
                return BadRequest("Status harus 'Approved' atau 'Rejected'");

            mhs.Status = status;
            await _repo.Update(mhs);

            return Ok(new { message = $"Status mahasiswa diubah menjadi {status}" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetAll();
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mhs = await _repo.GetById(id);
            if (mhs == null)
                return NotFound();

            // 🧹 Hapus file foto jika ada
            if (!string.IsNullOrEmpty(mhs.Foto))
            {
                var path = Path.Combine(_env.WebRootPath ?? "wwwroot", mhs.Foto.TrimStart('/'));
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            await _repo.Delete(id);
            return Ok(new { message = "Data mahasiswa berhasil dihapus." });
        }

    }
}
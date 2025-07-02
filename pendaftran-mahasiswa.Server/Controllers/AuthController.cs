using Microsoft.AspNetCore.Mvc;
using pendaftran_mahasiswa.Server.Data;
using pendaftran_mahasiswa.Server.DTOs;
using Microsoft.EntityFrameworkCore;

namespace pendaftran_mahasiswa.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto.Role == "admin")
            {
                var admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Username == dto.EmailOrUsername && a.Password == dto.Password);
                if (admin == null)
                    return Unauthorized(new { message = "Login admin gagal: akun tidak ditemukan." });

                return Ok(new { role = "admin", username = admin.Username });
            }

            if (dto.Role == "mahasiswa")
            {
                var mhs = await _context.Mahasiswas
                    .FirstOrDefaultAsync(m => m.Email == dto.EmailOrUsername && m.Password == dto.Password);

                if (mhs == null)
                    return Unauthorized(new { message = "Login gagal: akun mahasiswa tidak ditemukan." });

                if (mhs.Status == "Pending")
                    return Unauthorized(new { message = "Akun Anda masih menunggu persetujuan admin." });

                if (mhs.Status == "Rejected")
                    return Unauthorized(new { message = "Akun Anda telah ditolak." });

                return Ok(new { role = "mahasiswa", id = mhs.Id, nama = mhs.NamaLengkap });
            }

            return BadRequest(new { message = "Role tidak valid" });
        }
    }
}

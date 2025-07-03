using pendaftran_mahasiswa.Server.Data;
using pendaftran_mahasiswa.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace pendaftran_mahasiswa.Server.Repositories
{
    public class MahasiswaRepository : IMahasiswaRepository
    {
        private readonly ApplicationDbContext _context;

        public MahasiswaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mahasiswa>> GetAll() => await _context.Mahasiswas.ToListAsync();

        public async Task<Mahasiswa?> GetByEmail(string email) =>
            await _context.Mahasiswas.FirstOrDefaultAsync(m => m.Email == email);

        public async Task<Mahasiswa?> GetById(int id) =>
            await _context.Mahasiswas.FindAsync(id);

        public async Task Add(Mahasiswa mhs)
        {
            _context.Mahasiswas.Add(mhs);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Mahasiswa mhs)
        {
            _context.Mahasiswas.Update(mhs);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var mhs = await _context.Mahasiswas.FindAsync(id);
            if (mhs != null)
            {
                _context.Mahasiswas.Remove(mhs);
                await _context.SaveChangesAsync();
            }
        }

    }
}

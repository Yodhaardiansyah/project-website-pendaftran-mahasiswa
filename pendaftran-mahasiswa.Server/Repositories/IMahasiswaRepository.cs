using pendaftran_mahasiswa.Server.Models;

namespace pendaftran_mahasiswa.Server.Repositories
{
    public interface IMahasiswaRepository
    {
        Task<List<Mahasiswa>> GetAll();
        Task<Mahasiswa?> GetByEmail(string email);
        Task<Mahasiswa?> GetById(int id);
        Task Add(Mahasiswa mhs);
        Task Update(Mahasiswa mhs);
    }
}

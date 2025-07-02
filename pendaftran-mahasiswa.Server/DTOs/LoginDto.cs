namespace pendaftran_mahasiswa.Server.DTOs
{
    public class LoginDto
    {
        public required string EmailOrUsername { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; } // "admin" atau "mahasiswa"
    }
}

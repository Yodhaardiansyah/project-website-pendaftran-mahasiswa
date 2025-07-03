using Microsoft.EntityFrameworkCore;
using pendaftran_mahasiswa.Server.Data;
using pendaftran_mahasiswa.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddNewtonsoftJson(); // untuk parsing 
builder.Services.AddScoped<IMahasiswaRepository, MahasiswaRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Tambahkan policy CORS dengan nama eksplisit
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostVue", policy =>
    {
        policy.WithOrigins("https://pendaftaran-mahasiswa.arunovasi.my.id") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting(); 
// ✅ Gunakan policy yang sesuai
app.UseCors("AllowLocalhostVue");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();

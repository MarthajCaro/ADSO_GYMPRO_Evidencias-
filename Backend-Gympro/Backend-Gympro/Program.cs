using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Application.Services;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddScoped<IClaseService, ClaseService>();
builder.Services.AddScoped<IClaseRepository, ClaseRepository>();
builder.Services.AddScoped<IMunicipioService, MunicipioService>();
builder.Services.AddScoped<IMunicipioRepository, MunicipioRepository>();
builder.Services.AddScoped<IInscripcionService, InscripcionService>();
builder.Services.AddScoped<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddScoped<IMembresiaService, MembresiaService>();
builder.Services.AddScoped<IMembresiaRepository, MembresiaRepository>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();
builder.Services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<ISuplementoDeportivoService, SuplementoDeportivoService>();
builder.Services.AddScoped<ISuplementoDeportivoRepository, SuplementoDeportivoRepository>();
builder.Services.AddScoped<ITipoMembresiaService, TipoMembresiaService>();
builder.Services.AddScoped<ITipoMembresiaRepository, TipoMembresiaRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)) // Ajusta la versión a la tuya
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

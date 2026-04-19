using Dragon_Nutrex_Web.Core.Controllers;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Dragon_Nutrex_Web.Infrastructure.Repositories;
using Dragon_Nutrex_Web.Presentation.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DragonNutrexDb")
    ?? throw new InvalidOperationException("No se encontró la cadena de conexión DragonNutrexDb.");

builder.Services.AddSingleton(new SqlConnectionFactory(connectionString));

builder.Services.AddScoped<IRepository<Usuario>, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddScoped<IRepository<Producto>, ProductoRepository>();
builder.Services.AddScoped<ProductoService>();

builder.Services.AddScoped<IRepository<MenuDiario>, MenuDiarioRepository>();
builder.Services.AddScoped<IRepository<MenuDetalle>, MenuDetalleRepository>();
builder.Services.AddScoped<IRepository<ConsumoDiario>, ConsumoDiarioRepository>();

builder.Services.AddScoped<MenuDetalleRepository>();
builder.Services.AddScoped<MenuDiarioService>();
builder.Services.AddScoped<MenuDetalleService>();
builder.Services.AddScoped<ConsumoService>();
builder.Services.AddScoped<ReportExportService>();

builder.Services.AddScoped<ConsumoController>();
builder.Services.AddScoped<NutricionService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<MenuDiarioRepository>();
builder.Services.AddScoped<MenuDetalleRepository>();

builder.Services.AddScoped<AdminEstadisticasService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
using Dragon_Nutrex_Web.Common;
using Dragon_Nutrex_Web.Core.Controllers;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Dragon_Nutrex_Web.Infrastructure.Repositories;
using Dragon_Nutrex_Web.Presentation.Components;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. CONFIGURACIÓN BASE
// ==========================================
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ==========================================
// 2. ACCESO A DATOS (Infrastructure)
// ==========================================
var connectionString = builder.Configuration.GetConnectionString("DragonNutrexDb")
    ?? throw new InvalidOperationException("No se encontró la cadena de conexión DragonNutrexDb.");

builder.Services.AddSingleton(new SqlConnectionFactory(connectionString));

// ==========================================
// 3. REPOSITORIES
// ==========================================
builder.Services.AddScoped<IRepository<Usuario>, UsuarioRepository>();
builder.Services.AddScoped<IRepository<Producto>, ProductoRepository>();
builder.Services.AddScoped<IRepository<MenuDiario>, MenuDiarioRepository>();
builder.Services.AddScoped<IRepository<MenuDetalle>, MenuDetalleRepository>();
builder.Services.AddScoped<IRepository<ConsumoDiario>, ConsumoDiarioRepository>();

builder.Services.AddScoped<IMenuDiarioRepository, MenuDiarioRepository>();
builder.Services.AddScoped<IMenuDetalleRepository, MenuDetalleRepository>();
builder.Services.AddScoped<IConsumoDiarioRepository, ConsumoDiarioRepository>();

// ==========================================
// 4. SERVICES & CONTROLLERS
// ==========================================
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<MenuDiarioService>();
builder.Services.AddScoped<MenuDetalleService>();
builder.Services.AddScoped<ConsumoService>();
builder.Services.AddScoped<NutricionService>();
builder.Services.AddScoped<ReportExportService>();
builder.Services.AddScoped<AdminEstadisticasService>();

// Controllers
builder.Services.AddScoped<ConsumoController>();

// ==========================================
// 5. APP PIPELINE (Middleware)
// ==========================================
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(context =>
        {
            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (exceptionHandlerFeature?.Error is not null)
            {
                GlobalExceptionHandler.Handle(
                    exceptionHandlerFeature.Error,
                    "Program.UseExceptionHandler");
            }

            context.Response.Redirect("/Error");
            return Task.CompletedTask;
        });
    });

    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
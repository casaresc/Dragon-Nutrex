using Dragon_Nutrex_Web.Core.Controllers;
using Dragon_Nutrex_Web.Core.Services;
using Dragon_Nutrex_Web.Infrastructure.Repositories;
using Dragon_Nutrex_Web.Presentation.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<MenuDiarioService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<MenuDetalleService>();
builder.Services.AddScoped<ConsumoController>();
builder.Services.AddScoped<ConsumoService>();
builder.Services.AddScoped<NutricionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

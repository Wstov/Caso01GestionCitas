using GestionDeCitasBLL.Mapeos;
using GestionDeCitasBLL.Servicios;
using GestionDeCitasBLL.Servicios.GestionDeCitasBLL.Servicios;
using GestionDeCitasDAL.Repositorios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Repos en memoria (viven mientras corre la app)
builder.Services.AddSingleton<IClientesRepositorio, ClientesRepositorio>();
builder.Services.AddSingleton<IVehiculosRepositorio, VehiculosRepositorio>();
builder.Services.AddSingleton<ICitasRepositorio, CitasRepositorio>();

// Servicios
builder.Services.AddScoped<IClientesServicio, ClientesServicio>();
builder.Services.AddScoped<IVehiculosServicio, VehiculosServicio>();
builder.Services.AddScoped<ICitasServicio, CitasServicio>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

var app = builder.Build();
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clientes}/{action=Index}/{id?}");

app.Run();

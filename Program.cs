using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Models;
using MVC.Repositories;
using MVC.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
//setx DB_CONNECTION_STRING "Definir a variavel de ambiente no windows"
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Configurando o Entity Framework Core com SQL Server
// Adicionando o DbContext ao cont�iner de servi�os
// Configurando a string de conex�o
// O m�todo GetConnectionString busca a string de conex�o no arquivo appsettings.json
// O m�todo UseSqlServer configura o DbContext para usar o SQL Server
// O m�todo AddDbContext adiciona o DbContext ao cont�iner de servi�os
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// O m�todo AddControllersWithViews adiciona os servi�os necess�rios para o MVC
// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrando o reposit�rio de lanches como um servi�o
builder.Services.AddTransient<ILancheRepository, LancheRepository>();

// Registrando o reposit�rio de categorias como um servi�o
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();

// Registra um carrinho de compras como um servi�o escopo, permitindo que ele seja injetado em controladores e outros servi�os
builder.Services.AddScoped(sp  => CarrinhoCompra.GetCarrinho(sp));
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton<IHttpContextAccessor, IHttpContextAccessor>();

builder.Services.AddMemoryCache();

builder.Services.AddSession();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

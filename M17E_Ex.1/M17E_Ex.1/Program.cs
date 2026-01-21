using M17E_Ex._1.Pages.Data;
using M17E_Ex._1.Pages.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Razor Pages
builder.Services.AddRazorPages();

// 🔹 Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Serviço de autenticação (se estiver usando)
builder.Services.AddScoped<AuthService>();

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();


app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.UseRouting();
app.UseSession();
app.UseAuthorization();


app.Run();

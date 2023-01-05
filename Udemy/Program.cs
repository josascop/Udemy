using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Udemy.Data;
using Udemy.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UdemyContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UdemyContext") ??
                      throw new InvalidOperationException("Connection string 'UdemyContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<VendedorService>();
builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<RegistroDeVendaService>();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var pt = new CultureInfo("pt-BR");
app.UseRequestLocalization(new RequestLocalizationOptions {
    DefaultRequestCulture = new RequestCulture(pt),
    SupportedCultures = new List<CultureInfo> { pt },
    SupportedUICultures = new List<CultureInfo> { pt },
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
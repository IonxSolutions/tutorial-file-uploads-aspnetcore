using System.Text.Json.Serialization;
using Web.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Register an HTTP client for use with Verisys Antivirus API
builder.Services.AddHttpClient<HomeController>(x =>
{
    x.BaseAddress = new Uri("https://eu1.api.av.ionxsolutions.com/v1/");
    x.DefaultRequestHeaders.Add("Accept", "application/json");
    x.DefaultRequestHeaders.Add("X-API-Key", "<YOUR API KEY HERE>");
});

builder.Services.AddControllersWithViews().AddJsonOptions(x => {
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    x.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
});

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

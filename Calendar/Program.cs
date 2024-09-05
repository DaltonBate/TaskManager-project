using Calendar.Models;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbConnection>((s) =>
{
    IDbConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("finalproject"));
    conn.Open();
    return conn;

});

builder.Services.AddTransient<IEventRepository, EventRepository>();
//build web app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{   //custom error handling page in non dev enviorment
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. 
    app.UseHsts();
}
// HTTP to HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();
// for incoming requests
app.UseRouting();

app.UseAuthorization();
//allows route to map URLs to controllers and actions 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

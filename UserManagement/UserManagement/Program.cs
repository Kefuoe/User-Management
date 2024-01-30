using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddScoped<UserService>();

builder.Services.AddDbContext<DataContext>(opt => 
{
  opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

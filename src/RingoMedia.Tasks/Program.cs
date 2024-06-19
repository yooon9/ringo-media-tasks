using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RingoMedia.Tasks.Application.Models;
using RingoMedia.Tasks.Application.Services.DepartmentServices;
using RingoMedia.Tasks.Application.Services.EmailServices;
using RingoMedia.Tasks.Application.Services.ReminderServices;
using RingoMedia.Tasks.Domain.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Db configuration
builder.Services.AddDbContext<RingoMediaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RingoMediaDb")));

//Department configuration
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IReminderService, ReminderService>();

#region Email configuration
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton(p => p.GetService<IOptions<EmailSettings>>()?.Value);
builder.Services.AddSingleton<IEmailService, EmailService>();
#endregion

//Reminder configuration

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
    pattern: "{controller=Departments}/{action=Index}/{id?}");

app.Run();

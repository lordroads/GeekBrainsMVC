using EmployeesWebApplication.Servises;
using EmployeesWebApplication.Servises.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.MapDefaultControllerRoute();

app.Run();

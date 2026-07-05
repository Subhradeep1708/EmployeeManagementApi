using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.BAL.Services;
using EmployeeManagement.DAL.Contexts;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS setup
//builder.Services.AddCors(options=>
//    options.AddPolicy("AllowReact",
//        policy =>
//        {
//            policy
//                 .WithOrigins("http://localhost:5173")
//                 .AllowAnyHeader()
//                 .AllowAnyMethod();
//        }
//    )
//);

// connection string dbContexts added
builder.Services.AddSingleton<DapperContext>();

builder.Services.AddDbContext<EmployeeManagementContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("EmployeeDBConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("EmployeeDBConnection"))
        )
);



//builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IEmployeeService, EmployeeService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("AllowReact");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

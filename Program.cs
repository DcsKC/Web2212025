using Microsoft.EntityFrameworkCore;
using Web2212025.Data;
using Web2212025.Models; // Import DbContext và Models

var builder = WebApplication.CreateBuilder(args);

// 👉 Add DbContext service với chuỗi kết nối từ appsettings.json
builder.Services.AddDbContext<StudentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDBConnectionString")));

// Add services to the container.
builder.Services.AddControllers();

// Swagger cấu hình
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) // che đi để luôn hỗ trợ Swagger
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

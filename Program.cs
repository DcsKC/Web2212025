using Microsoft.EntityFrameworkCore;
using Web2212025.Data;
using Web2212025.Models; // Import DbContext và Models

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext (Chọn giữa cấu hình cứng hoặc lấy từ appsettings.json)
var useHardcodedConnection = false; // Thay đổi thành true nếu muốn sử dụng chuỗi kết nối cứng

if (useHardcodedConnection)
{
    builder.Services.AddDbContext<StudentContext>(options =>
        options.UseSqlServer("Server=DCSKC\\MSSQLSERVER01;Database=Student;Trusted_Connection=True;TrustServerCertificate=True;"));
}
else
{
    builder.Services.AddDbContext<StudentContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDBConnectionString")));
}

// Cấu hình CORS (Chấp nhận mọi nguồn gốc, phương thức và tiêu đề)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Add services to the container
builder.Services.AddControllers();

// Cấu hình Swagger (API documentation)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware xử lý lỗi toàn cục
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"message\": \"An unexpected error occurred. Please try again later.\"}");
    });
});

// Cấu hình HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Hiển thị thông tin lỗi chi tiết trong môi trường phát triển
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sử dụng CORS
app.UseCors("AllowAll");

// Chỉ bật HTTPS khi không ở môi trường phát triển
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

// Ánh xạ các Controller
app.MapControllers();

app.Run();

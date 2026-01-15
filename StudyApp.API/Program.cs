using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StudyApp.API.Hubs;
using StudyApp.API.Services;
using StudyApp.BLL; // Chứa AddBusinessServices
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Services;
using StudyApp.BLL.Services.Learn;
using StudyApp.DAL.Data;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// 1. CẤU HÌNH SERVICES (DI CONTAINER)
// =====================================================

builder.Services.AddControllers();
builder.Services.AddOpenApi(); // .NET 9+ OpenAPI

// --- Cấu hình Database ---
builder.Services.AddDbContext<LearningDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LearningDb")));

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDb")));

// Nếu các Service khác yêu cầu SocialDb, hãy thêm luôn:
builder.Services.AddDbContext<SocialDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SocialDb")));

// --- Cấu hình SignalR (Bắt buộc để Thách đấu Real-time) ---
builder.Services.AddSignalR();

// --- Đăng ký BLL Services (Hàm mở rộng từ tầng BLL) ---
builder.Services.AddBusinessServices();

// --- Đăng ký Notifier bản SignalR cho Server ---
// Lớp này dùng IHubContext để phát tin nhắn tới WinForms
builder.Services.AddScoped<IThachDauNotifier, SignalRThachDauNotifier>();

// --- Cấu hình AutoMapper (Nếu bạn dùng chung Profile từ BLL) ---
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// =====================================================
// 2. CẤU HÌNH HTTP PIPELINE (MIDDLEWARE)
// =====================================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Thêm dòng này để vào bằng /scalar/v1
}

app.UseHttpsRedirection();
app.UseAuthorization();

// --- Đăng ký Endpoint cho SignalR Hub ---
// WinForms sẽ kết nối tới: https://localhost:7001/thachDauHub
app.MapHub<StudyApp.API.Hubs.ThachDauHub>("/thachDauHub");

app.MapControllers();

app.Run();
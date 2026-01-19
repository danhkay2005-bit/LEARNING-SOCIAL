using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Interfaces.User;
using StudyApp.BLL.Mappings.Learn;
using StudyApp.BLL.Services.Learn;
using StudyApp.BLL.Services.User;
using StudyApp.DAL.Data;
using System;
using System.IO;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.Services;

namespace WinForms
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            // 1. Cấu hình appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 2. Thiết lập DI Container
            var services = new ServiceCollection();

            // --- Cấu hình Database (Kết nối trực tiếp từ WinForms) ---
            services.AddDbContext<LearningDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LearningDb")));
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UserDb")));
            services.AddDbContext<SocialDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SocialDb")));

            // --- Đăng ký SignalR CLIENT (Dùng Singleton để giữ kết nối) ---
            services.AddSingleton<HubConnection>(sp =>
            {
                // Lấy URL từ cấu hình, mặc định là port 7001 của dự án API
                var hubUrl = configuration["SignalR:HubUrl"] ?? "https://localhost:7001/thachDauHub";

                return new HubConnectionBuilder()
                    .WithUrl(hubUrl)
                    .WithAutomaticReconnect() // Tự động kết nối lại khi mất mạng
                    .Build();
            });

            services.AddSingleton<IConfiguration>(configuration);

            // --- Đăng ký Notifier bản rỗng cho WinForms ---
            // Giúp ThachDauService khởi tạo thành công mà không báo lỗi IHubContext
            services.AddSingleton<IThachDauNotifier, WinFormsThachDauNotifier>();

            // --- Đăng ký AutoMapper ---
            services.AddAutoMapper(typeof(BoDeHocProfile).Assembly);

            // --- Đăng ký các Business Services từ tầng BLL ---
            services.AddBusinessServices();


            //================================================
            //Thêm các này vào
            //====================
            // Đăng ký Service cho Game & Nhiệm vụ
          //  services.AddScoped<IGamificationService, GamificationService>();

            // Đăng ký Service cho Chuỗi (Streak)
          //  services.AddScoped<IDailyStreakService, DailyStreakService>();

            // Đăng ký lại BoDeHocService (để đảm bảo dùng phiên bản có tích hợp Game)
            // Lưu ý: Nếu AddBusinessServices đã đăng ký rồi thì dòng này sẽ ghi đè (tốt)
          //  services.AddScoped<IBoDeHocService, BoDeHocService>();

            // ==================================================================

            // --- Tự động đăng ký tất cả các Form và UserControl ---
            services.Scan(scan => scan
                .FromAssemblyOf<MainForm>()
                .AddClasses(classes => classes.AssignableToAny(typeof(Form), typeof(UserControl)))
                .AsSelf()
                .WithTransientLifetime());

            // 3. Build Provider
            ServiceProvider = services.BuildServiceProvider();

            // 4. Khởi chạy ứng dụng WinForms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Lấy MainForm từ DI Container và chạy
            var mainForm = ServiceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.DAL.Data;
using System;
using System.Windows.Forms;
using WinForms.Forms;

namespace WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 1. Load cấu hình từ appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 2. Tạo DI container và cấu hình các DbContext
            var services = new ServiceCollection();

            // Đăng ký từng DbContext
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UserDb")));
            services.AddDbContext<LearningDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LearningDb")));
            services.AddDbContext<SocialDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SocialDb")));

            // 3. Tạo provider
            var serviceProvider = services.BuildServiceProvider();

            // 4. Khởi động chương trình như bình thường
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()); // Truyền serviceProvider vào form chính nếu muốn DI bên trong
        }
    }
}
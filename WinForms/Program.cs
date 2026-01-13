using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL;
using StudyApp.BLL.Mappings.Learn;
using StudyApp.DAL.Data;
using System;
using System.IO;
using System.Windows.Forms;
using WinForms.Forms;

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

            // --- Đăng ký DbContext ---
            services.AddDbContext<LearningDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LearningDb")));
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UserDb")));
            services.AddDbContext<SocialDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SocialDb")));

            // --- Đăng ký AutoMapper (Quét tự động các Profile) ---
            services.AddAutoMapper(typeof(BoDeHocProfile).Assembly);

            // --- Đăng ký BLL Services (Sử dụng Scrutor trong Extension Method) ---
            services.AddBusinessServices();

            // --- Tự động đăng ký tất cả các Form có trong Project WinForms ---
            // Thay vì gõ từng cái, Scrutor sẽ đăng ký tất cả lớp kế thừa từ 'Form'
            services.Scan(scan => scan
                .FromAssemblyOf<MainForm>()
                .AddClasses(classes => classes.AssignableToAny(typeof(Form), typeof(UserControl))) // Quét cả Form và UserControl
                .AsSelf()
                .WithTransientLifetime());

            // 3. Build Provider
            ServiceProvider = services.BuildServiceProvider();

            // 4. Khởi chạy ứng dụng
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Lấy MainForm ra từ DI và chạy
            var mainForm = ServiceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }
    }
}
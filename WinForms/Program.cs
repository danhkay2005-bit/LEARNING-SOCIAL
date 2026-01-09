using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Services.Implementations.User;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DAL.Data;
using System;
using System.Windows.Forms;
using WinForms.Forms;

namespace WinForms
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            Application.Run(ServiceProvider.GetRequiredService<frmDangNhap>());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // ===============================
            // AUTO MAPPER
            // ===============================
            services.AddAutoMapper(
                typeof(StudyApp.BLL.Mappers.User.NguoiDungMapping).Assembly
            );

            // ===============================
            // DB CONTEXT (BẮT BUỘC)
            // ===============================
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(
                    System.Configuration.ConfigurationManager
                        .ConnectionStrings["UserDb"].ConnectionString
                );
            });
            services.AddDbContext<SocialDbContext>(options =>
            {
                options.UseSqlServer(
                    System.Configuration.ConfigurationManager
                        .ConnectionStrings["SocialDb"].ConnectionString
                );
            });
            services.AddDbContext<LearningDbContext>(options =>
            {
                options.UseSqlServer(
                    System.Configuration.ConfigurationManager
                        .ConnectionStrings["LearningDb"].ConnectionString
                );
            });
            // ===============================
            // SERVICES
            // ===============================
            services.AddScoped<INguoiDungService, NguoiDungService>();

            // ===============================
            // FORMS
            // ===============================
            services.AddTransient<frmDangNhap>();
        }

    }
}

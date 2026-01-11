using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Services.Implementations.User;
using StudyApp.BLL.Services.Interfaces.Social;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.BLL.Services.Social;
using StudyApp.DAL.Data;
using System;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.UserControls.Pages;
using Scrutor;

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

            Application.Run(ServiceProvider.GetRequiredService<mainForm>());
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
            services.Scan(scan => scan
                .FromAssemblyOf<IBaiDangService>() // assembly BLL
                .AddClasses(classes => classes.Where(type =>
                    type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            // ===============================
            // FORMS
            // ===============================
            services.AddTransient<mainForm>();
            services.AddTransient<FeedPageControl>();
        }

    }
}

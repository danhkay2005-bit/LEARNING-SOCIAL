using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

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

            Application.Run(new Forms.frmDangNhap());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // ===============================
            // AUTO MAPPER (GIỮ LẠI)
            // ===============================
            services.AddAutoMapper(
                typeof(StudyApp.BLL.Mappers.Learning.TheFlashcardMapping).Assembly
            );

            // ===============================
            // FORM (TẠM THỜI ẨN)
            // ===============================
            // services.AddTransient<MainForm>();

            // ===============================
            // SERVICES (SAU NÀY)
            // ===============================
            // services.AddScoped<IHocTapService, HocTapService>();
        }
    }
}

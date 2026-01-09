using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.BLL.Services.Implementations.User;
using StudyApp.DAL.Data;
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
            Microsoft.Extensions.DependencyInjection.ServiceCollectionExtensions.AddAutoMapper(
                services,
                typeof(StudyApp.BLL.Mappers.User.NguoiDungMapping).Assembly);

            services.AddTransient<frmDangNhap>();

            services.AddTransient<INguoiDungService, NguoiDungService>();
        }
    }
}

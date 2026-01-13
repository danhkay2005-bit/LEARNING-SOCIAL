using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Services.Learn; // Reference đến một class bất kỳ trong BLL

namespace StudyApp.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Sử dụng Scrutor để quét Assembly
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(BoDeHocService)) // Sử dụng overload nhận Type thay vì generic
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service"))) // Tìm các lớp kết thúc bằng "Service"
                .AsImplementedInterfaces() // Đăng ký dưới dạng Interface (ví dụ: IBoDeHocService)
                .WithScopedLifetime()); // Thiết lập Lifetime là Scoped (phù hợp với DbContext)

            return services;
        }
    }
}
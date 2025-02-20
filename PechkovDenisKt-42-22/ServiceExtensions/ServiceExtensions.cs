
using PechkovDenisKt_42_22.Interfaces.TeacherInterfaces;
using PechkovDenisKt_42_22.Services.DepartmentServices;
using PechkovDenisKt_42_22.Services.DisciplineServices;

namespace PechkovDenisKt_42_22.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<TeacherService, TeacherService>();
            services.AddScoped<DepartmentService>();
            services.AddScoped<DisciplineService>();

            return services;
        }
    }
}


using PechkovDenisKt_42_22.Interfaces.TeacherInterfaces;

namespace PechkovDenisKt_42_22.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<TeacherService, TeacherService>();

            return services;
        }
    }
}

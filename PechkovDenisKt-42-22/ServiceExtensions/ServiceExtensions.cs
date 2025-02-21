﻿using PechkovDenisKt_42_22.Services.DepartmentServices;
using PechkovDenisKt_42_22.Services.DisciplineServices;
using PechkovDenisKt_42_22.Services.TeacherServices;
using PechkovDenisKt_42_22.Services.LoadServices;



namespace PechkovDenisKt_42_22.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            services.AddScoped<TeacherService>();
            
            services.AddScoped<DisciplineService>();
            services.AddScoped<LoadService>();
            services.AddScoped<DepartmentService>();

            return services;
        }
    }
}

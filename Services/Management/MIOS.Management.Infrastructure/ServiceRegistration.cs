using Microsoft.Extensions.DependencyInjection;
using MIOS.Management.Application.Interfaces;
using MIOS.Management.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOS.Management.Infrastructure
{
    public static class ServiceRegistration
    {

    
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}

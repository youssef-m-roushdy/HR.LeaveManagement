using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigrationApplicationServices(this IServiceCollection services)
        {
            //Assembly.GetExecutingAssembly() : search for every mapping profile inherits from base auto mapper profile
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
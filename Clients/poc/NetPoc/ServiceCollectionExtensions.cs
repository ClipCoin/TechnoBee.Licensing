using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace NetPoc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPocRoutines(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<PocRoutines>();

            return serviceCollection;
        }
    }
}

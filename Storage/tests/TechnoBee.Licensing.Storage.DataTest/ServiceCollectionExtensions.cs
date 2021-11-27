using Microsoft.Extensions.DependencyInjection;
using System;
using Technobee.Licensing.Storage.DataModels.Repositories;
using Technobee.Marketing.Licensing.DataModels.Data;
using TechnoBee.Marketing.Licensing.DataModels.Repositories;

namespace TechnoBee.Marketing.Licensing.DataTest
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider GetRepositoriesProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient(sp => { return new LicenseDbContext(); })
                .AddTransient<IProductRepository>(sp => { return new ProductRepository(sp.GetRequiredService<LicenseDbContext>()); })
                .BuildServiceProvider();
        }
    }
}

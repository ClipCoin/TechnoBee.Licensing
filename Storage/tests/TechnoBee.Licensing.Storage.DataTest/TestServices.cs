using Microsoft.Extensions.DependencyInjection;
using System;
using Technobee.Licensing.Storage.DataModels.UnitOfWork;
using Technobee.Marketing.Licensing.DataModels.UnitOfWork;

namespace TechnoBee.Marketing.Licensing.DataTest
{
    public class TestServices : IDisposable
    {
        public ILicenseUnitOfWork LicenseUnitOfWork { get; private set; }

        public TestServices()
        {
            IServiceProvider repositoryBox = new ServiceCollection().GetRepositoriesProvider();

            IServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<ILicenseUnitOfWork>(sp => new LicenseUnitOfWork(repositoryBox))
                .BuildServiceProvider();

            //OrganizationService = serviceProvider.GetRequiredService<IOrganizationService>(); // new OrganizationService(_cmUOW);
            //UserService = serviceProvider.GetRequiredService<IUserService>(); // new UserService(_cmUOW);

            //loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            LicenseUnitOfWork = serviceProvider.GetRequiredService<ILicenseUnitOfWork>();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

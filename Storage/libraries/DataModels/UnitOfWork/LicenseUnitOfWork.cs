using System;
using Technobee.Licensing.Storage.DataModels.UnitOfWork;
using Technobee.Marketing.Core_Abstractions.UnitOfWork;
using Technobee.Marketing.Licensing.DataModels.Data;

namespace Technobee.Marketing.Licensing.DataModels.UnitOfWork
{
    public class LicenseUnitOfWork : CommonUnitOfWork<LicenseDbContext>, ILicenseUnitOfWork
    {
        protected LicenseDbContext _cmContext;

        public LicenseUnitOfWork(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}

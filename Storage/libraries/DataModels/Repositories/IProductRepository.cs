using System;
using System.Collections.Generic;
using System.Text;
using Technobee.Licensing.Storage.DataModels.Models;
using Technobee.Marketing.Core_Abstractions.Repositories;

namespace Technobee.Licensing.Storage.DataModels.Repositories
{
    public interface IProductRepository : ICommonRepository<Product, int>
    {
    }
}

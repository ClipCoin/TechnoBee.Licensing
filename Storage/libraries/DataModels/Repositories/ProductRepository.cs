using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technobee.Licensing.Storage.DataModels.Models;
using Technobee.Licensing.Storage.DataModels.Repositories;
using Technobee.Marketing.Licensing.DataModels.Data;
using TechnoBee.Counters.Storage.Technobee.Marketing.Core_Abstractions.Repositories;

namespace TechnoBee.Marketing.Licensing.DataModels.Repositories
{
    public class ProductRepository : BaseRepository<LicenseDbContext, Product, int>, IProductRepository
    {
        public ProductRepository(LicenseDbContext context) : base(context) { }

        public override IQueryable<Product> QuerySingle => base.QuerySingle;

        public override Product Add(Product person)
        {
            base.Add(person);
            SaveChanges();

            return person;
        }
    }
}

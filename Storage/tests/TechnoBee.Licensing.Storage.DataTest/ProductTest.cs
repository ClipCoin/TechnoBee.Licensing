using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Technobee.Licensing.Storage.DataModels.Models;
using Technobee.Licensing.Storage.DataModels.Repositories;

namespace TechnoBee.Marketing.Licensing.DataTest
{
    [TestClass]
    public class ProductTest :  BaseTest
    {
        [TestMethod]
        public void Test_Product_Repository()
        {
            // Prepare
            IProductRepository productRepo = _services.LicenseUnitOfWork.GetRepository<IProductRepository>();

            // Pre-validate
            Assert.IsNotNull(productRepo);

            // Perform
            Product product = productRepo.Get(1);

            // Post-validate
            Assert.IsNotNull(product);
        }
    }
}

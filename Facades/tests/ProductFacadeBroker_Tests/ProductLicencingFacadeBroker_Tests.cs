using System;
using Xunit;

using TechnoBee.Licensing.Facades;

namespace ProductFacadeBroker_Tests
{
    public class ProductLicencingFacadeBroker_Tests
    {
        [Theory]
        [InlineData("48652F17-2D5B-45E1-96AC-7468E853866D")]
        public void ProductLicencingFacadeBroker_GetProductLicensingFacade(String productGuid)
        {
            //  Prepare
            ProductLicencingFacadeBroker broker = new ProductLicencingFacadeBroker();

            //  Pre-validate

            //  Perform

            IProductLicensingFacade facade = broker.GetProductLicensingFacade(productGuid);

            //  Post-validate
            Assert.NotNull(facade);
        }
    }
}

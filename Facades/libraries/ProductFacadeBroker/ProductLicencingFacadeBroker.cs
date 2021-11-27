using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TechnoBee.Licensing.Helpers;

namespace TechnoBee.Licensing.Facades
{
    [ComVisible(true)]
    [Guid("089D297B-840C-4B0D-8A1B-A4549EF8A694")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("TechnoBee.Licensing.Facades.ProductLicencingFacadeBroker")]
    public class ProductLicencingFacadeBroker
        : IProductLicencingFacadeBroker
    {
        public IProductLicensingFacade GetProductLicensingFacade(string productGuid)
        {
            // select suitable descriptor from _lazyDescriptors and start com object
            throw new NotImplementedException();
        }

        private static IEnumerable<ProductFacadeDescriptor> LoadDescriptors()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("")
                .Parse()
                .AsText()
                .AsYaml(builder => {
                    builder.WithNamingConvention(new YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention());
                })
                .To<List<ProductFacadeDescriptor>>();
        }

        private Lazy<IEnumerable<ProductFacadeDescriptor>> _lazyDescriptors 
            = new Lazy<IEnumerable<ProductFacadeDescriptor>>(LoadDescriptors);
    }
}
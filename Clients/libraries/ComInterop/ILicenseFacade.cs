using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients
{
    //[Guid("8CD2C604-581F-4ADD-83EE-752DC56FA558")]
    //[ComVisible(true)]
    //[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ILicenseFacade
    {
      //  [DispId(1)]
        bool CheckLicense(string LicenseFileName, string LicenseApplicationName, string LicenseProductGuid, string LicenseProductVersion);
        
        /// <summary>
        /// Create fingerprint of current machine
        /// </summary>
        /// <param name="outputFilepath">File to write fingerprint</param>
        /// <param name="overwriteFile">Overwrite if exist</param>
        /// <returns></returns>
       // [DispId(2)]
        bool GenerateFingerprint(string outputFilepath, bool overwriteFile = true);
    }
}

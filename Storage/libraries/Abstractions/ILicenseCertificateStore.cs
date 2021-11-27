using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Counters.Storage
{
    public interface ILicenseCertificateStore
    {
        IEnumerable<ILicenseCertificateFile> LoadFiles();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

namespace TechnoBee.Counters.Storage
{
    public interface ILicenseCertificateFile
    {
        ILicenseCertificate Content { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public interface ILicenseCertificate
    {
        Guid Id { get; }
        DateTime IssueTime { get; }
        Version Version { get; }
        IEnumerable<IProductLicense> Licenses { get; }
    }
}

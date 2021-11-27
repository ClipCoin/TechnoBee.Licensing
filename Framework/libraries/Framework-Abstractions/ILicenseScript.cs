using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public interface ILicenseScript
    {
        String Hook { get; }
        String Language { get; }
        String Code { get; }
    }
}

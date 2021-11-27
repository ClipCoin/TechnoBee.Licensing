using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Counters.Storage
{
    public interface ILocalCertificateStoreConfig
    {
        ILocalCertificateStoreConfig AddFile(String filePath);
        ILocalCertificateStoreConfig AddFiles(String directoryName, String fileGlob);
        ILocalCertificateStoreConfig AddDirectory(String directoryName);
    }
}

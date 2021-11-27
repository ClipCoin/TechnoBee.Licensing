using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Glob;

namespace TechnoBee.Counters.Storage
{
    public class LocalCertificateStoreConfig
        : ILocalCertificateStoreConfig
    {
        public LocalCertificateStoreConfig(){
            _lazyFiles = new Lazy<IEnumerable<FileInfo>>(TraverseLocations);
        }

        public IEnumerable<FileInfo> Files => _lazyFiles.Value;

        public ILocalCertificateStoreConfig AddDirectory(String directoryName)
        {
            _enumerators.Add(() => new DirectoryInfo(directoryName).EnumerateFiles());

            return this;
        }

        public ILocalCertificateStoreConfig AddFile(String filePath)
        {
            _enumerators.Add(() => new FileInfo[] { new FileInfo(filePath) });

            return this;
        }

        public ILocalCertificateStoreConfig AddFiles(String directoryName, String pattern)
        {
            _enumerators.Add(() => new DirectoryInfo(directoryName).GlobFiles(pattern));

            return this;
        }

        private IEnumerable<FileInfo> TraverseLocations()
        {
            return _enumerators.SelectMany(e => e());
        }

        private List<Func<IEnumerable<FileInfo>>> _enumerators = new List<Func<IEnumerable<FileInfo>>>();
        private Lazy<IEnumerable<FileInfo>> _lazyFiles;
    }
}

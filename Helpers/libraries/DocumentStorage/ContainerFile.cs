using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class TechnoBeeDocumentContainerSection
    {
        internal TechnoBeeDocumentContainerSection(ZipArchiveEntry archiveEntry)
        {
            _archiveEntry = archiveEntry;
            _lazyStream = new Lazy<Stream>(() => _archiveEntry.Open());
        }

        public Stream GetStream()
        {
            return _lazyStream.Value;
        }

        private Lazy<Stream> _lazyStream;
        private ZipArchiveEntry _archiveEntry;
    }

    public class ContainerFile
        : IDisposable
    {
        public ContainerFile(String filePath, FileAccess fileAccess = FileAccess.Read)
        {
            ZipArchiveMode archiveMode = fileAccess == FileAccess.Read 
                ? ZipArchiveMode.Read 
                : ZipArchiveMode.Update;

            _underlyingArchive = ZipFile.Open(filePath, archiveMode);
        }

        public TechnoBeeDocumentContainerSection GetSection(String sectionName, Boolean isRequired = false)
        {
            ZipArchiveEntry archiveEntry = _underlyingArchive.GetEntry(sectionName);

            if (archiveEntry != null)
            {
                return new TechnoBeeDocumentContainerSection(archiveEntry);
            }
            else
            {
                if (isRequired)
                {
                    throw new TechnoBeeDocumentContainerSectionNotFoundException(sectionName);
                }
                else
                {
                    return null;
                }
            }
        }

        public TechnoBeeDocumentContainerSection CreateSection(String sectionName)
        {
            ZipArchiveEntry archiveEntry = _underlyingArchive.CreateEntry(sectionName);

            return new TechnoBeeDocumentContainerSection(archiveEntry);
        }

        public static ContainerFile Open(String filePath, FileAccess fileAccess = FileAccess.Read)
        {
            return new ContainerFile(filePath, fileAccess);
        }

        public void Dispose()
        {
            _underlyingArchive.Dispose();
        }

        private ZipArchive _underlyingArchive;
    }
}

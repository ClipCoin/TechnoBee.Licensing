using System;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechnoBee.Licensing.Helpers
{
    public static class ZipArchiveExtensions
    {
        public static ZipArchiveEntry Assert(this ZipArchiveEntry entry, Predicate<ZipArchiveEntry> assertion, Action<ZipArchiveEntry> onFail)
        {
            if (!assertion(entry))
                onFail(entry);

            return entry;
        }
    }
}

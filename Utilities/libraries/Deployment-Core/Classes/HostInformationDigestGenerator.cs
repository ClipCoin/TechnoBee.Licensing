using System;
using System.Linq;
using AutoMapper;
using TechnoBee.Licensing.Utilities.Common;
using TechnoBee.Licensing.Utilities.DeploymentCore.Services;
using TechnoBee.Licensing.Utilities.LicensingHelper.Services;

namespace TechnoBee.Licensing.Utilities.Deployment
{
    internal sealed class HostInformationDigestProvider
        : IHostInformationDigestProvider
    {
        public HostInformationDigestProvider( IWmiService wmiService, IFileService fileService )
        {
            _wmiService = wmiService;
            _fileService = fileService;
        }

        public IHostInformationDigest GenerateHostInformationDigest()
        {
            return new HostInformationDigest()
            {
                Version = new Version("1.0"),
                Timestamp = DateTime.Now,
                HostName = _wmiService.GetHostName(),
                DiskDrives = _wmiService
                        .GetDiskDrives()
                        .Select(dd => _lazyMapper.Value.Map<DiskDrive>(dd))
                        .ToArray(),
                NetworkInterfaces = _wmiService
                        .GetNetworkInterfaces()
                        .Select(ni => _lazyMapper.Value.Map<NetworkInterface>(ni))
                        .ToArray()
            };
        }

        private readonly IWmiService _wmiService;
        private readonly IFileService _fileService;

        private static Lazy<IMapper> _lazyMapper = new Lazy<IMapper>(() => {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IDiskDrive, DiskDrive>();
                cfg.CreateMap<INetworkInterface, NetworkInterface>();
            })
            .CreateMapper();
        });
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.CommandLineUtils;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class CommandLineApplicationEx
        : CommandLineApplication
    {
        public CommandLineApplicationEx(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //public CommandLineApplication Command(string commandName, Action<CommandLineApplicationEx> configuration)
        //{
        //    return base.Command(commandName, app => configuration(app))
        //}

        public IServiceProvider ServiceProvider => _serviceProvider; 

        private IServiceProvider _serviceProvider;
    }
}

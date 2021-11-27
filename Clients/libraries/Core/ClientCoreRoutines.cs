using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients
{
    public static class ClientCoreRoutines
    {
        public static void SayHello(ref ExportType result)
        {
            Console.WriteLine("Hello from TechnoBee.Licensing.Clients.ClientCoreRoutines");

            result.StringValue = "Updated String value";
        }
    }
}

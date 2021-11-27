using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

[assembly: Guid("5ddf5244-d9f9-4049-a656-ea0430f9f28d")]
[assembly:ComVisible(true)]

namespace TechnoBee.Licensing.Clients
{
    [Guid("DBE0E8C4-1C61-41f3-B6A4-4E2F353D3D05")]
    public interface IManagedInterface
    {
        int PrintHi(string name);
    }

    [Guid("C6659361-1625-4746-931C-36014B146679")]
    public class InterfaceImplementation : IManagedInterface
    {
        public int PrintHi(string name)
        {
            Console.WriteLine("Hello, {0}!", name);
            return 33;
        }
    }
}

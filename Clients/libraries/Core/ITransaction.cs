using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Client
{
    public interface ITransaction
    {
        void Commit();
        void Discard();
    }
}

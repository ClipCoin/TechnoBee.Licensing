using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Clients;

namespace NetPoc
{
    public class PocRoutines
    {
        public PocRoutines(ILicensing licensing)
        {
            _licensing = licensing;
        }

        public void Routine1()
        {
            //_licensing
            //    .CreateRegistry()
            //    .AddHostLicenseFileStorage()
            //    .BuildLicenseRegistry()
            //    .ConstructApplicationLicenseQuery()
            //    .ForApplication()

            throw new NotImplementedException();
        }

        private ILicensing _licensing;
    }
}

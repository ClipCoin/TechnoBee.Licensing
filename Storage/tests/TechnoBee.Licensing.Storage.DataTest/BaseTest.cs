using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Marketing.Licensing.DataTest
{
    public class BaseTest
    {
        protected TestServices _services;

        public BaseTest()
        {
            _services = new TestServices();
        }
    }
}

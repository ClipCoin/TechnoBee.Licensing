using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LicensingServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Licensing")]
    public class LicensingController : Controller
    {
        [HttpPost]
        public string RegisterFingerprint(string ticket, string tbhid)
        {
            return "{'friends': ['Michael', 'Tom', 'Daniel', 'John', 'Nick']}";
        }

        [HttpPost]
        public bool CheckLicense(byte[] license, string fingerprint)
        {
            return false;
        }
    }
}
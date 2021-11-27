using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Technobee.Marketing.Counters.DataModels.DTO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TechnoBee.Marketing.App.CountersViewer.Controllers
{
    public class CounterController : Controller
    {
        // GET: Counter
        public ActionResult Index()
        {
            List<LineRecordViewDTO> list = GetRecords(new StoreRecordsFilterDTO { id = 1, from = DateTime.Now.AddMonths(-8), to = DateTime.Now });
            return View(list);
        }

        private List<LineRecordViewDTO> GetRecords(StoreRecordsFilterDTO filter)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
            {
               { "id", filter.id.ToString() },
               { "from", filter.from.ToString("yyyy-MM-dd") },
               { "to", filter.to.ToString("yyyy-MM-dd") }
            };

            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync("http://localhost:8001/api/values", content).Result; //53970

            client.Dispose();
            var responseString = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<List<LineRecordViewDTO>>(responseString);
        }
    }
}

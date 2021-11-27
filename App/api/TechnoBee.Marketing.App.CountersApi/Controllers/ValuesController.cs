using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Technobee.Marketing.Counters.DataModels.DTO;
using Technobee.Marketing.Counters.DataModels.Entities;
using Technobee.Marketing.Counters.DataModels.Helpers;
using Technobee.Marketing.Counters.DataModels.Services;


namespace TechnoBee.Marketing.App.CountersApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            try
            {
                _LineService.GetByStore(1, DateTime.Now, DateTime.Now);
                return "+ok";
            }
            catch (Exception ex)
            {
                return "-err: " + ex.Message;
            }
        }


        ILineRecordService _LineService;


        public ValuesController(ILineRecordService LineService)
        {
            _LineService = LineService;
        }


       // [HttpPost]
        public List<LineRecordViewDTO> Post(StoreRecordsFilterDTO request)
        {
            var records = _LineService.GetByStore(request.id, request.from, request.to);
            return records.Select(x => x.ToSerial()).ToList();
        }

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

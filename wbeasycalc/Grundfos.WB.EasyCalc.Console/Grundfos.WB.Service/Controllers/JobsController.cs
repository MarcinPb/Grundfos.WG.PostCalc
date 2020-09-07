using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Grundfos.WB.Service.Models;
using Hangfire;

namespace Grundfos.WB.Service.Controllers
{
    public class JobsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// POST api/jobs
        /// </summary>
        /// <param name="zoneIdentifier"></param>
        public void Post([FromBody] WbJobModel jobModel)
        {
            string zoneIdentifier = jobModel.ZoneIdentifier;
            BackgroundJob.Enqueue<Jobs.WbCalculationJob>(x => x.ExecuteCalculation(null, DateTime.Now, zoneIdentifier));
        }
    }
}
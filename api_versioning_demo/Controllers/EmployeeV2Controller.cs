using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_versioning_demo.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [ApiVersion("2.1")]
    [Route("api/v{version:apiVersion}/employee")]
    public class EmployeeV2Controller : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("employees from v2 controller");
        }

        // GET api/values
        [HttpGet]
        [MapToApiVersion("2.1")] // v2.1 specific action for GET api/values endpoint
        public ActionResult GetV2_1()
        {
            return new OkObjectResult("employees from v2.1 controller");
        }
    }
}

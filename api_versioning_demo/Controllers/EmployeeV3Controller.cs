using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_versioning_demo.Controllers
{
    [ApiController]
    [ApiVersion("3.0")]
    [ApiVersion("3.1")]
    [Route("api/v{version:apiVersion}/employee")]
    public class EmployeeV3Controller : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("employees from v3 controller");
        }

        // GET api/values
        [HttpGet]
        [MapToApiVersion("3.1")] // v3.1 specific action for GET api/values endpoint
        public ActionResult GetV3_1()
        {
            return new OkObjectResult("employees from v3.1 controller");
        }
    }
}

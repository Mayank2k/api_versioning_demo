using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_versioning_demo.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/employee")]

    public class EmployeeController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("employees from v1 controller");
        }

        // GET api/values
        [HttpGet]
        [MapToApiVersion("1.1")] // v1.1 specific action for GET api/values endpoint
        public ActionResult GetV1_1()
        {
            return new OkObjectResult("employees from v1.1 controller");
        }
    }
}

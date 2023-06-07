using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Model;

namespace WarehouseWeb.Controllers
{
    public class BaseController : ControllerBase
    {
        [HttpGet("[action]/{r}")]
        public ActionResult GetResultByStatusCode(Result r)
        {
            switch (r.StatusCode)
            {
                case StatusCodes.Status404NotFound:
                    return NotFound(r);
                case StatusCodes.Status204NoContent:
                    return NoContent();
                case StatusCodes.Status406NotAcceptable:
                    return BadRequest(r);
                case StatusCodes.Status500InternalServerError:
                    return BadRequest(r);
                case StatusCodes.Status200OK:
                    return Ok(r);
                default:
                    return NotFound(r);
            }
        }
    }
}

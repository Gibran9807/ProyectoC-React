using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
       [Route("/api/error")]
       public IActionResult HandleError() => Problem();
    }
}
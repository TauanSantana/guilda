using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HostNameProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProcessamentoController : ControllerBase
    {
        public string Get()
        {
            var x = 0.0001;
            for (int i = 0; i < 1000000; i++)
            {
                x += Math.Sqrt(x);
            }

            return "OK";

        }
    }
}

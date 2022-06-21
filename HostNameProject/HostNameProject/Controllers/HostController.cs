using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HostNameProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HostController : ControllerBase
    {
        public string Get()
        {
            return $"Host Name: {Dns.GetHostName()}";
        }

    }
}
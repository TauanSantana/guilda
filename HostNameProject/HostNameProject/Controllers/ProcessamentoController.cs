using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HostNameProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProcessamentoController : ControllerBase
    {
        public void Get()
        {
            List<Thread> threads = new List<Thread>();
            var dataInicio = DateTime.UtcNow;
            while (true)
            {
                if (dataInicio.AddMinutes(1) <= DateTime.UtcNow)
                    break;

                threads.Add(new Thread(new ThreadStart(KillCore)));
            }
        }

        private void KillCore()
        {
            Random rand = new Random();
            long num = 0;
            while (true)
            {
                num += rand.Next(100, 1000);
                if (num > 1000000) { num = 0; }
            }
        }
    }
}

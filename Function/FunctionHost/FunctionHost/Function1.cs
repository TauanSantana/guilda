using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using RabbitMQ.Client;
using System.Text;
using System.Collections.Generic;

namespace FunctionHost
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = $"Azure Function Host: { Dns.GetHostName()} - {DateTime.UtcNow.AddHours(-3)}";

            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RabbitMq_host", EnvironmentVariableTarget.Process),
                UserName = Environment.GetEnvironmentVariable("RabbitMq_user", EnvironmentVariableTarget.Process),
                Password = Environment.GetEnvironmentVariable("RabbitMq_pass", EnvironmentVariableTarget.Process)
            };

            

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var props = channel.CreateBasicProperties();
                props.ContentType = "text/plain";
                props.Headers = new Dictionary<string, object>();

                channel.QueueDeclare(queue: "queue-guilda",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicPublish(exchange: "",
                                        routingKey: "queue-guilda",
                                        basicProperties: props,
                                        body: Encoding.UTF8.GetBytes(responseMessage));

            }
            return new OkObjectResult(responseMessage);
           
        }
    }
}

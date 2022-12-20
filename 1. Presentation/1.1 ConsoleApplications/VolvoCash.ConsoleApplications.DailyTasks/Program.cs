using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

namespace VolvoCash.ConsoleApplications.DailyTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("config.json", optional: false);

            IConfiguration config = builder.Build();
            var baseUrl = config.GetValue<string>("BaseUrl");

            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var batchesExpirationUri = "/api_web/batches/expire_batches";            
            var batchExpirationResult = client.PostAsync(batchesExpirationUri,null).Result;
            Console.WriteLine("Perform Batch Expiration Result = " + batchExpirationResult.StatusCode);

            var generateLiquidationsUri = "/api_web/liquidations/generate";
            var generateLiquidationsResult = client.GetAsync(generateLiquidationsUri).Result;
            Console.WriteLine("Perform Generate Liquidations Result = " + generateLiquidationsResult.StatusCode);
        }
    }
}

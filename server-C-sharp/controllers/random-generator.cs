using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Constants;
using Unosquare.Labs.EmbedIO.Modules;
using System.Threading;


namespace serverC_sharp.controllers
{
    class RandomGenerator : WebApiController
    {
        Random random = new Random();
        bool isNowCrashed = false;
        IHttpContext context;
        // You need to add a default constructor where the first argument
        // is an IHttpContext
        public RandomGenerator(IHttpContext context)
            : base(context)
        {
            this.context = context;
        }

        // You need to include the WebApiHandler attribute to each method
        // where you want to export an endpoint. The method should return
        // bool or Task<bool>.
        [WebApiHandler(HttpVerbs.Get, "/api_v2/random-generator/getrandom")]
        public async Task<bool> GetPersonById()
        {
            try
            {
                HttpContext.Response.Headers.Set("Access-Control-Allow-Origin", "*");

                if (random.NextDouble() > 0.7)
                {
                    Console.WriteLine("crash simulation");
                    isNowCrashed = true;

                    await Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((task) =>
                    {
                        isNowCrashed = false;
                    });
                    return await context.JsonExceptionResponseAsync(new Exception("Timed out"), System.Net.HttpStatusCode.Forbidden, true, default(CancellationToken));
                }
                else
                {
                    if (isNowCrashed)
                        return await context.JsonExceptionResponseAsync(new Exception("Timed out"), System.Net.HttpStatusCode.Forbidden, true, default(CancellationToken));
                    else
                        return await context.JsonResponseAsync("{\"random\": \"" + random.NextDouble() + "\", \"server\": \"c#\"}");
                }
                // return await Ok("{\"random: \""+random.NextDouble()+ "\", \"server\": \"c#\"}");
            }
            catch (Exception ex)
            {
                return await InternalServerError(ex);
            }
        }

        // You can override the default headers and add custom headers to each API Response.
        public override void SetDefaultHeaders() => HttpContext.NoCache();
    }
}

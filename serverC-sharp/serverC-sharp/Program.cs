using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using serverC_sharp.controllers;
using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Constants;
using Unosquare.Labs.EmbedIO.Modules;

namespace serverC_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new WebServer("http://127.0.0.1:4000/", RoutingStrategy.Regex);

            server.RegisterModule(new WebApiModule());
            server.Module<WebApiModule>().RegisterController<RandomGenerator>();
            server.RunAsync();
            Console.ReadKey(true);
        }
    }
}

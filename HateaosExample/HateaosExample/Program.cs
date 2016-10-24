using System;

using Microsoft.Owin.Hosting;

namespace HateaosExample
{
    // Source: http://pracujlabs.io/2015/04/30/becoming-hateoas-with-webapi.html
    internal class Program
    {
        private static void Main()
        {
            var baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine($"Running on {baseAddress}");

                Console.ReadLine();
            }
        }
    }
}
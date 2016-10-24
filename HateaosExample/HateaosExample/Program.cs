using System;

using Microsoft.Owin.Hosting;

namespace HateaosExample
{
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
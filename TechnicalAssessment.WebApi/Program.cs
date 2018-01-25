// <copyright file="Program.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.WebApi
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using TechnicalAssessment.WebApi.Configuration;

    /// <summary>
    /// Program runtime initialization class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runtime initialization method.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(ApplicationConstants.ServiceUriAddress)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
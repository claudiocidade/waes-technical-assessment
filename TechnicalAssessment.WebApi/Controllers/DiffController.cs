// <copyright file="DiffController.cs" company="WAES">
//  Copyright (c) WAES. All rights reserved.
// </copyright>
namespace TechnicalAssessment.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Compares two JSON base-64 encoded binary 
    /// data and returns the diagnostics of the comparison process.
    /// </summary>
    [Produces("application/json")]
    [Route("diff")]
    public class DiffController : Controller
    {
        /// <summary>
        /// A simple hello world test method.
        /// </summary>
        /// <returns>Returns "Hello world!".</returns>
        [HttpGet]
        [Route("hello")]
        public async Task<string> Hello()
        {
            return "Hello world!";
        }
    }
}
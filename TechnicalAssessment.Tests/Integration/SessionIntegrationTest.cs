// <copyright file="SessionIntegrationTest.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Tests.Integration
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Shouldly;
    using TechnicalAssessment.WebApi.Configuration;
    using TechnicalAssessment.WebApi.Models;

    /// <summary>
    /// An integration test class for <see cref="Domain.Session"/> web api.
    /// </summary>
    [TestClass]
    public class SessionIntegrationTest
    {
        /// <summary>
        /// An instance of the <see cref="TestServer"/> class.
        /// </summary>
        private readonly TestServer server;

        /// <summary>
        /// An instance of the <see cref="JsonSerializerSettings"/> class.
        /// </summary>
        private readonly JsonSerializerSettings jsonSerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionIntegrationTest"/> class.
        /// </summary>
        public SessionIntegrationTest()
        {
            this.jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            this.server = new TestServer(new WebHostBuilder().UseStartup<Tests.Startup>());
        }

        /// <summary>
        /// Should save a single message to the session left side.
        /// </summary>
        /// <returns>An instance of the <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldSaveMessageLeft()
        {
            Guid sessionId = Guid.NewGuid();

            HttpClient client = this.server.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            WebApi.Models.Message message = new Message() { Data = ApplicationConstants.DefaultData };

            string json = JsonConvert.SerializeObject(message, Formatting.None, this.jsonSerializerSettings);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                
            HttpResponseMessage response = await client.PostAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}/left"), content);

            string result = (await response.Content.ReadAsStringAsync()).Replace(@"""", string.Empty);

            result.ShouldContain("true");
        }

        /// <summary>
        /// Should save a single message to the session right side.
        /// </summary>
        /// <returns>An instance of the <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldSaveMessageRight()
        {
            Guid sessionId = Guid.NewGuid();

            HttpClient client = this.server.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            WebApi.Models.Message message = new Message() { Data = ApplicationConstants.DefaultData };

            string json = JsonConvert.SerializeObject(message, Formatting.None, this.jsonSerializerSettings);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}/right"), content);

            string result = (await response.Content.ReadAsStringAsync()).Replace(@"""", string.Empty);

            result.ShouldContain("true");
        }

        /// <summary>
        /// Both messages should have the same content.
        /// </summary>
        /// <returns>An instance of the <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldGetSameContentMessageResult()
        {
            Guid sessionId = Guid.NewGuid();

            HttpClient client = this.server.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Message message = new Message() { Data = ApplicationConstants.DefaultData };

            string json = JsonConvert.SerializeObject(message, Formatting.None, this.jsonSerializerSettings);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}/left"), content);

            await client.PostAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}/right"), content);

            HttpResponseMessage compare = await client.GetAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}"));

            (await compare.Content.ReadAsStringAsync()).Replace(@"""", string.Empty).ShouldBe("Content is the same");
        }

        /// <summary>
        /// Both messages should have different sizes.
        /// </summary>
        /// <returns>An instance of the <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldGetDifferentSizesMessageResult()
        {
            Guid sessionId = Guid.NewGuid();

            HttpClient client = this.server.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Message leftMessage = new Message() { Data = ApplicationConstants.DefaultData };
            Message rightMessage = new Message() { Data = ApplicationConstants.DifferentLargerData };

            await client.PostAsync(
                new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}/left"), 
                new StringContent(
                    JsonConvert.SerializeObject(leftMessage, Formatting.None, this.jsonSerializerSettings), 
                    Encoding.UTF8, 
                    "application/json"));

            await client.PostAsync(
                new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}/right"), 
                new StringContent(
                    JsonConvert.SerializeObject(rightMessage, Formatting.None, this.jsonSerializerSettings), 
                    Encoding.UTF8, 
                    "application/json"));

            HttpResponseMessage compare = await client.GetAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}"));

            (await compare.Content.ReadAsStringAsync()).Replace(@"""", string.Empty).ShouldBe("Sizes are different");
        }

        /// <summary>
        /// Gets the comparison results between the two sides
        /// </summary>
        /// <returns>An instance of the <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldGetComparisonMessageResult()
        {
            Guid sessionId = Guid.NewGuid();

            HttpClient client = this.server.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Message leftMessage = new Message() { Data = ApplicationConstants.DefaultData };
            Message rightMessage = new Message() { Data = ApplicationConstants.SameSizeDifferentData };

            await client.PostAsync(
                new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}/left"), 
                new StringContent(
                    JsonConvert.SerializeObject(leftMessage, Formatting.None, this.jsonSerializerSettings), 
                    Encoding.UTF8, 
                    "application/json"));

            await client.PostAsync(
                new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}/right"), 
                new StringContent(
                    JsonConvert.SerializeObject(rightMessage, Formatting.None, this.jsonSerializerSettings), 
                    Encoding.UTF8, 
                    "application/json"));

            HttpResponseMessage compare = await client.GetAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/diff/{sessionId}"));

            (await compare.Content.ReadAsStringAsync()).Replace(@"""", string.Empty).ShouldContain("Differences found starting at 3 ending at 5");
            (await compare.Content.ReadAsStringAsync()).Replace(@"""", string.Empty).ShouldContain("Differences found starting at 66 ending at 68");
        }
    }
}
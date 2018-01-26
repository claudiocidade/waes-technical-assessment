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
        private readonly TestServer server;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        public SessionIntegrationTest()
        {
            this.jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            this.server = new TestServer(new WebHostBuilder().UseStartup<Tests.Startup>());
        }

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
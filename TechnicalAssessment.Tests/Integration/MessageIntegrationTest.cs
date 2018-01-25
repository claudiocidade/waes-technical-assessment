// <copyright file="MessageIntegrationTest.cs" company="WAES">
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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Shouldly;
    using TechnicalAssessment.Domain.Builders;
    using TechnicalAssessment.WebApi.Configuration;
    using TechnicalAssessment.WebApi.Models;

    [TestClass]
    public class MessageIntegrationTest
    {
        private readonly TestServer server;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        public MessageIntegrationTest()
        {
            this.jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            this.server = new TestServer(new WebHostBuilder().UseStartup<Tests.Startup>());
        }

        [TestMethod]
        public async Task ShouldSaveMessage()
        {
            HttpClient client = server.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            WebApi.Models.Message message = new Message() { Data = ApplicationConstants.DefaultData };

            string json = JsonConvert.SerializeObject(message, Formatting.None, this.jsonSerializerSettings);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                
            HttpResponseMessage response = await client.PostAsync(new Uri(ApplicationConstants.ServiceUri, "v1/message"), content);

            string id = (await response.Content.ReadAsStringAsync()).Replace("\"", string.Empty);

            Guid insertId;

            Guid.TryParse(id, out insertId);

            insertId.ShouldNotBe(default(Guid));
        }

        [TestMethod]
        public async Task ShouldGetSameContentMessageResult()
        {
            HttpClient client = server.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Message message = new Message() { Data = ApplicationConstants.DefaultData };

            string json = JsonConvert.SerializeObject(message, Formatting.None, this.jsonSerializerSettings);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage left = await client.PostAsync(new Uri(ApplicationConstants.ServiceUri, "v1/message"), content);

            HttpResponseMessage right = await client.PostAsync(new Uri(ApplicationConstants.ServiceUri, "v1/message"), content);

            Guid leftId, rightId;

            Guid.TryParse((await left.Content.ReadAsStringAsync()).Replace("\"", string.Empty), out leftId);
            Guid.TryParse((await right.Content.ReadAsStringAsync()).Replace("\"", string.Empty), out rightId);

            leftId.ShouldNotBe(default(Guid));
            rightId.ShouldNotBe(default(Guid));

            HttpResponseMessage compare = await client.GetAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/message/{leftId}/compare/{rightId}"));

            (await compare.Content.ReadAsStringAsync()).Replace(@"""", string.Empty).ShouldBe("Content is the same");
        }

        [TestMethod]
        public async Task ShouldGetDifferentSizesMessageResult()
        {
            HttpClient client = server.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Message leftMessage = new Message() { Data = ApplicationConstants.DefaultData };
            Message rightMessage = new Message() { Data = ApplicationConstants.DifferentLargerData };

            HttpResponseMessage left = await client.PostAsync(
                new Uri(ApplicationConstants.ServiceUri, "v1/message"), 
                new StringContent(JsonConvert.SerializeObject(leftMessage, Formatting.None, this.jsonSerializerSettings), 
                Encoding.UTF8, 
                "application/json"));

            HttpResponseMessage right = await client.PostAsync(
                new Uri(ApplicationConstants.ServiceUri, "v1/message"), 
                new StringContent(JsonConvert.SerializeObject(rightMessage, Formatting.None, this.jsonSerializerSettings), 
                Encoding.UTF8, 
                "application/json"));

            Guid leftId, rightId;

            Guid.TryParse((await left.Content.ReadAsStringAsync()).Replace("\"", string.Empty), out leftId);

            Guid.TryParse((await right.Content.ReadAsStringAsync()).Replace("\"", string.Empty), out rightId);

            leftId.ShouldNotBe(default(Guid));
            rightId.ShouldNotBe(default(Guid));

            HttpResponseMessage compare = await client.GetAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/message/{leftId}/compare/{rightId}"));

            (await compare.Content.ReadAsStringAsync()).Replace(@"""", string.Empty).ShouldBe("Sizes are different");
        }

        [TestMethod]
        public async Task ShouldGetComparisonMessageResult()
        {
            HttpClient client = server.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Message leftMessage = new Message() { Data = ApplicationConstants.DefaultData };
            Message rightMessage = new Message() { Data = ApplicationConstants.SameSizeDifferentData };

            HttpResponseMessage left = await client.PostAsync(
                new Uri(ApplicationConstants.ServiceUri, "v1/message"), 
                new StringContent(JsonConvert.SerializeObject(leftMessage, Formatting.None, this.jsonSerializerSettings), 
                Encoding.UTF8, 
                "application/json"));

            HttpResponseMessage right = await client.PostAsync(
                new Uri(ApplicationConstants.ServiceUri, "v1/message"), 
                new StringContent(JsonConvert.SerializeObject(rightMessage, Formatting.None, this.jsonSerializerSettings), 
                Encoding.UTF8, 
                "application/json"));

            Guid leftId, rightId;

            Guid.TryParse((await left.Content.ReadAsStringAsync()).Replace("\"", string.Empty), out leftId);
            Guid.TryParse((await right.Content.ReadAsStringAsync()).Replace("\"", string.Empty), out rightId);

            leftId.ShouldNotBe(default(Guid));
            rightId.ShouldNotBe(default(Guid));

            HttpResponseMessage compare = await client.GetAsync(new Uri(ApplicationConstants.ServiceUri, $"v1/message/{leftId}/compare/{rightId}"));

            (await compare.Content.ReadAsStringAsync()).Replace(@"""", string.Empty).ShouldContain("Differences found starting at 3 ending at 5");
            (await compare.Content.ReadAsStringAsync()).Replace(@"""", string.Empty).ShouldContain("Differences found starting at 66 ending at 68");
        }
    }
}
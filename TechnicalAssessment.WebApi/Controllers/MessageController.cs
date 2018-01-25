// <copyright file="DiffController.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.WebApi.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TechnicalAssessment.Data;
    using TechnicalAssessment.Data.Repositories;
    using TechnicalAssessment.Data.Repositories.Contracts;
    using TechnicalAssessment.Domain;
    using TechnicalAssessment.Domain.Builders;
    using TechnicalAssessment.Domain.Services;
    using TechnicalAssessment.Domain.Services.Contracts;

    /// <summary>
    /// Compares two JSON base-64 encoded binary 
    /// data and returns the diagnostics of the comparison process.
    /// </summary>
    [Produces("application/json")]
    [Route("message")]
    public class MessageController : Controller
    {
        private readonly IMessageRepository messageRepository = new MessageRepository(DatabaseClient.Create());

        /// <summary>
        /// Sets the message for later comparison.
        /// </summary>
        /// <param name="message">Message to the stored.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpPost]
        [Route("")]
        public async Task<Guid> Save(Message message)
        {
            return await this.messageRepository.Save(message);
        }

        /// <summary>
        /// Gets the comparison results of both sides messages.
        /// </summary>
        /// <param name="lid">Left side message identification.</param>
        /// <param name="rid">Right side message identification.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpGet]
        [Route("{lid}/compare/{rid}")]
        public async Task<string> Get(Guid lid, Guid rid)
        {
            Message left = await this.messageRepository.Get(lid);

            Message right = await this.messageRepository.Get(rid);

            IMessageDomainService domainService = new MessageDomainService();

            return domainService.AnalyzeMessages(left, right);
        }
    }
}
// <copyright file="MessageController.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// An instance of the <see cref="IMessageRepository"/> implementation class.
        /// </summary>
        private readonly IMessageRepository messageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageController"/> class.
        /// </summary>
        /// <param name="messageRepository">An instance of the <see cref="IMessageRepository"/> implementation class.</param>
        public MessageController(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        /// <summary>
        /// Sets the message for later comparison.
        /// </summary>
        /// <param name="message">Message to the stored.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Save([FromBody]Models.Message message)
        {
            Message domain = new MessageBuilder()
                .WithData(message.Data)
                .Build();

            return this.Ok(await this.messageRepository.Save(domain));
        }

        /// <summary>
        /// Gets the comparison results of both messages.
        /// </summary>
        /// <param name="lid">Left side message identification.</param>
        /// <param name="rid">Right side message identification.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpGet]
        [Route("{lid}/compare/{rid}")]
        public async Task<ActionResult> Compare([FromRoute]Guid lid, [FromRoute]Guid rid)
        {
            Message left = await this.messageRepository.Get(lid);

            Message right = await this.messageRepository.Get(rid);

            IMessageDomainService domainService = new MessageDomainService();

            return this.Ok(domainService.AnalyzeMessages(left, right));
        }
    }
}
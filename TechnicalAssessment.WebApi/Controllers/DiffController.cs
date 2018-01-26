// <copyright file="DiffController.cs" company="WAES">
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
    [Route("diff")]
    public class DiffController : Controller
    {
        /// <summary>
        /// An instance of the <see cref="ISessionRepository"/> implementation class.
        /// </summary>
        private readonly ISessionRepository sessionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiffController"/> class.
        /// </summary>
        /// <param name="sessionRepository">An instance of the <see cref="ISessionRepository"/> implementation class.</param>
        public DiffController(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        /// <summary>
        /// Sets the message for the comparison session left side.
        /// </summary>
        /// <param name="id">Session identification.</param>
        /// <param name="message">Message to the stored.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpPost]
        [Route("{id}/left")]
        public async Task<ActionResult> Left([FromRoute]Guid id, [FromBody]Models.Message message)
        {
            SessionBuilder sessionBuilder = new SessionBuilder(await this.sessionRepository.Get(id.ToString()));

            sessionBuilder.WithLeftSideMessage(new MessageBuilder().WithData(message.Data));

            await this.sessionRepository.Save(sessionBuilder.Build());

            return this.Ok(true);
        }

        /// <summary>
        /// Sets the message for the comparison session right side.
        /// </summary>
        /// <param name="id">Session identification.</param>
        /// <param name="message">Message to the stored.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpPost]
        [Route("{id}/right")]
        public async Task<ActionResult> Right([FromRoute]Guid id, [FromBody]Models.Message message)
        {
            SessionBuilder sessionBuilder = new SessionBuilder(await this.sessionRepository.Get(id.ToString()));

            sessionBuilder.WithRightSideMessage(new MessageBuilder().WithData(message.Data));

            await this.sessionRepository.Save(sessionBuilder.Build());

            return this.Ok(true);
        }

        /// <summary>
        /// Gets the comparison results of both messages.
        /// </summary>
        /// <param name="id">Session identification.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get([FromRoute]Guid id)
        {
            Session existingSession = await this.sessionRepository.Get(id.ToString());

            IMessageDomainService messageDomainService = new MessageDomainService();

            return this.Ok(messageDomainService.AnalyzeMessages(existingSession.LeftSide, existingSession.RightSide));
        }
    }
}
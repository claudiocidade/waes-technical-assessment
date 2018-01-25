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
    [Route("diff")]
    public class DiffController : Controller
    {
        private readonly ISessionRepository sessionRepository = new SessionRepository(DatabaseClient.Create());

        /// <summary>
        /// Sets the message for the comparison session left side.
        /// </summary>
        /// <param name="id">Session identification.</param>
        /// <param name="message">Message to the stored.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpPost]
        [Route("{id}/left")]
        public async Task Left(Guid id, Message message)
        {
            SessionBuilder sessionBuilder = new SessionBuilder(await this.sessionRepository.Get(id.ToString()));

            sessionBuilder.WithLeftSideMessage(new MessageBuilder(message));

            await this.sessionRepository.Save(sessionBuilder.Build());
        }

        /// <summary>
        /// Sets the message for the comparison session right side.
        /// </summary>
        /// <param name="id">Session identification.</param>
        /// <param name="message">Message to the stored.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpPost]
        [Route("{id}/right")]
        public async Task Right(Guid id, Message message)
        {
            SessionBuilder sessionBuilder = new SessionBuilder(await this.sessionRepository.Get(id.ToString()));

            sessionBuilder.WithRightSideMessage(new MessageBuilder(message));

            await this.sessionRepository.Save(sessionBuilder.Build());
        }

        /// <summary>
        /// Gets the comparison results of both messages.
        /// </summary>
        /// <param name="id">Session identification.</param>
        /// <returns>Returns an instance of the <see cref="Task"/> representing the execution result.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<string> Get(Guid id)
        {
            Session existingSession = await this.sessionRepository.Get(id.ToString());

            IMessageDomainService messageDomainService = new MessageDomainService();

            return messageDomainService.AnalyzeMessages(existingSession.LeftSide, existingSession.RightSide);
        }
    }
}
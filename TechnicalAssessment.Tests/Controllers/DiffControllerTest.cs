// <copyright file="DiffControllerTest.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using Shouldly;
    using TechnicalAssessment.Data.Repositories.Contracts;
    using TechnicalAssessment.Domain.Builders;
    using TechnicalAssessment.WebApi.Configuration;
    using TechnicalAssessment.WebApi.Controllers;

    /// <summary>
    /// A test class for <see cref="DiffController"/>.
    /// </summary>
    [TestClass]
    public class DiffControllerTest
    {
        /// <summary>
        /// Saving a message in the left side of the session should pass.
        /// </summary>
        /// <returns>An instance of the assynchronous <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldSaveMessageToSessionLeftSide()
        {
            Guid sessionId = Guid.NewGuid();

            ISessionRepository repository = Substitute.For<ISessionRepository>();

            repository.Get(sessionId.ToString()).Returns(new SessionBuilder(sessionId.ToString()).Build());
            
            DiffController controller = new DiffController(repository);

            ActionResult result = await controller.Left(sessionId, new WebApi.Models.Message() { Data = ApplicationConstants.DefaultData });

            result.ShouldBeAssignableTo<OkObjectResult>();
        }

        /// <summary>
        /// Saving a message in the right side of the session should pass.
        /// </summary>
        /// <returns>An instance of the assynchronous <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldSaveMessageToSessionRightSide()
        {
            Guid sessionId = Guid.NewGuid();

            ISessionRepository repository = Substitute.For<ISessionRepository>();

            repository.Get(sessionId.ToString()).Returns(new SessionBuilder(sessionId.ToString()).Build());
            
            DiffController controller = new DiffController(repository);

            ActionResult result = await controller.Right(sessionId, new WebApi.Models.Message() { Data = ApplicationConstants.DefaultData });

            result.ShouldBeAssignableTo<OkObjectResult>();
        }

        /// <summary>
        /// Both messages in a session should have the same content.
        /// </summary>
        /// <returns>An instance of the assynchronous <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldGetSameContentMessageResult()
        {
            Guid sessionId = Guid.NewGuid();

            ISessionRepository repository = Substitute.For<ISessionRepository>();

            MessageBuilder messageBuilder = new MessageBuilder().WithData(ApplicationConstants.DefaultData);

            repository.Get(sessionId.ToString())
                .Returns(new SessionBuilder(sessionId.ToString())
                            .WithLeftSideMessage(messageBuilder)
                            .WithRightSideMessage(messageBuilder)
                            .Build());
            
            DiffController controller = new DiffController(repository);
            
            ActionResult result = await controller.Get(sessionId);

            result.ShouldBeAssignableTo<OkObjectResult>();

            ((OkObjectResult)result).Value.ShouldBe("Content is the same");
        }

        /// <summary>
        /// Both messages in a session should have different sizes.
        /// </summary>
        /// <returns>An instance of the assynchronous <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldGetDifferentSizesMessageResult()
        {
            Guid sessionId = Guid.NewGuid();

            ISessionRepository repository = Substitute.For<ISessionRepository>();

            MessageBuilder leftMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.DefaultData);

            MessageBuilder rightMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.DifferentLargerData);

            repository.Get(sessionId.ToString())
                .Returns(new SessionBuilder(sessionId.ToString())
                    .WithLeftSideMessage(leftMessageBuilder)
                    .WithRightSideMessage(rightMessageBuilder)
                    .Build());

            DiffController controller = new DiffController(repository);

            ActionResult result = await controller.Get(sessionId);

            result.ShouldBeAssignableTo<OkObjectResult>();

            ((OkObjectResult)result).Value.ShouldBe("Sizes are different");
        }

        /// <summary>
        /// A message describing the differences between both files is expected.
        /// </summary>
        /// <returns>An instance of the assynchronous <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldGetMessageResult()
        {
            Guid sessionId = Guid.NewGuid();

            ISessionRepository repository = Substitute.For<ISessionRepository>();

            MessageBuilder leftMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.DefaultData);

            MessageBuilder rightMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.SameSizeDifferentData);

            repository.Get(sessionId.ToString())
                .Returns(new SessionBuilder(sessionId.ToString())
                    .WithLeftSideMessage(leftMessageBuilder)
                    .WithRightSideMessage(rightMessageBuilder)
                    .Build());

            DiffController controller = new DiffController(repository);

            ActionResult result = await controller.Get(sessionId);

            result.ShouldBeAssignableTo<OkObjectResult>();

            ((OkObjectResult)result).Value.ToString().ShouldContain("Differences found starting at 3 ending at 5");
            ((OkObjectResult)result).Value.ToString().ShouldContain("Differences found starting at 66 ending at 68");
        }
    }
}
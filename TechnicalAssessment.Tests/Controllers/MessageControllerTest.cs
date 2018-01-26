// <copyright file="MessageControllerTest.cs" company="WAES">
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
    /// A test class for <see cref="MessageController"/>.
    /// </summary>
    [TestClass]
    public class MessageControllerTest
    {
        /// <summary>
        /// Should save a new message and return the ID.
        /// </summary>
        /// <returns>An instance of the assynchronous <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldSaveMessage()
        {
            IMessageRepository repository = Substitute.For<IMessageRepository>();

            Guid messageId = Guid.NewGuid();

            MessageBuilder messageBuilder = new MessageBuilder().WithData(ApplicationConstants.DefaultData);

            Domain.Message domain = messageBuilder.Build();

            repository.Save(domain).Returns(messageId);
            
            MessageController controller = new MessageController(repository);

            ActionResult result = await controller.Save(new WebApi.Models.Message() { Data = domain.Data });

            result.ShouldBeAssignableTo<OkObjectResult>();
            ((OkObjectResult)result).Value.ToString().ShouldBe(messageId.ToString());
        }

        /// <summary>
        /// Both messages should have the same content.
        /// </summary>
        /// <returns>An instance of the assynchronous <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldGetSameContentMessageResult()
        {
            Guid firstMessageId = Guid.NewGuid();

            Guid secondMessageId = Guid.NewGuid();

            IMessageRepository repository = Substitute.For<IMessageRepository>();

            MessageBuilder firstMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.DefaultData);

            MessageBuilder secondMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.DefaultData);

            repository.Get(firstMessageId)
                .Returns(firstMessageBuilder.Build());

            repository.Get(secondMessageId)
                .Returns(secondMessageBuilder.Build());
            
            MessageController controller = new MessageController(repository);
            
            ActionResult result = await controller.Compare(firstMessageId, secondMessageId);

            result.ShouldBeAssignableTo<OkObjectResult>();

            ((OkObjectResult)result).Value.ShouldBe("Content is the same");
        }

        /// <summary>
        /// Messages should have different sizes.
        /// </summary>
        /// <returns>An instance of the assynchronous <see cref="Task"/>.</returns>
        [TestMethod]
        public async Task ShouldGetDifferentSizesMessageResult()
        {
            Guid firstMessageId = Guid.NewGuid();

            Guid secondMessageId = Guid.NewGuid();

            IMessageRepository repository = Substitute.For<IMessageRepository>();

            MessageBuilder firstMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.DefaultData);

            MessageBuilder secondMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.DifferentLargerData);

            repository.Get(firstMessageId)
                .Returns(firstMessageBuilder.Build());

            repository.Get(secondMessageId)
                .Returns(secondMessageBuilder.Build());

            MessageController controller = new MessageController(repository);

            ActionResult result = await controller.Compare(firstMessageId, secondMessageId);

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
            Guid firstMessageId = Guid.NewGuid();

            Guid secondMessageId = Guid.NewGuid();

            IMessageRepository repository = Substitute.For<IMessageRepository>();

            MessageBuilder firstMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.DefaultData);

            MessageBuilder secondMessageBuilder = new MessageBuilder().WithData(ApplicationConstants.SameSizeDifferentData);

            repository.Get(firstMessageId)
                .Returns(firstMessageBuilder.Build());

            repository.Get(secondMessageId)
                .Returns(secondMessageBuilder.Build());

            MessageController controller = new MessageController(repository);

            ActionResult result = await controller.Compare(firstMessageId, secondMessageId);

            result.ShouldBeAssignableTo<OkObjectResult>();

            ((OkObjectResult)result).Value.ToString().ShouldContain("Differences found starting at 3 ending at 5");
            ((OkObjectResult)result).Value.ToString().ShouldContain("Differences found starting at 66 ending at 68");
        }
    }
}
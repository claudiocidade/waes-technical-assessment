// <copyright file="MessageDomainServiceTest.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Tests.DomainServices
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using TechnicalAssessment.Domain;
    using TechnicalAssessment.Domain.Builders;
    using TechnicalAssessment.Domain.Services;
    using TechnicalAssessment.Domain.Services.Contracts;

    /// <summary>
    /// A test class for <see cref="MessageDomainService"/>.
    /// </summary>
    [TestClass]
    public class MessageDomainServiceTest
    {
        [TestMethod]
        public void ShouldHaveSameContent()
        {
            IMessageDomainService domainService = new MessageDomainService();

            const string Data =
                "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            Session session = this.CreateTestSession(Data, Data);

            domainService.AnalyzeMessages(session.LeftSide, session.RightSide).ShouldBe("Content is the same");
        }

        [TestMethod]
        public void ShouldHaveDifferentSizes()
        {
            IMessageDomainService domainService = new MessageDomainService();

            const string First =
                "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            const string Second =
                "LTAiGQezNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            Session session = this.CreateTestSession(First, Second);

            domainService.AnalyzeMessages(session.LeftSide, session.RightSide).ShouldBe("Sizes are different");
        }

        [TestMethod]
        public void ShouldHaveBothSides()
        {
            IMessageDomainService domainService = new MessageDomainService();

            const string First =
                "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            Session session = this.CreateTestSession(First, string.Empty);

            domainService.AnalyzeMessages(session.LeftSide, session.RightSide).ShouldBe("Session must have both sides");
        }

        [TestMethod]
        public void ShouldHaveDifferencesDetected()
        {
            IMessageDomainService domainService = new MessageDomainService();

            const string First =
                "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            const string Second =
                "MTIzMDA3OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxWzOxcm51MTJub2kyMTBuMQ==";

            Session session = this.CreateTestSession(First, Second);

            string result = domainService.AnalyzeMessages(session.LeftSide, session.RightSide);

            result.ShouldContain("Differences found starting at 3 ending at 5");
            result.ShouldContain("Differences found starting at 66 ending at 68");
        }

        private Session CreateTestSession(string leftSideData, string rightSideData)
        {
            SessionBuilder sessionBuilder = new SessionBuilder(Guid.NewGuid().ToString());

            if (!string.IsNullOrEmpty(leftSideData))
            {
                sessionBuilder.WithLeftSideMessage(new MessageBuilder().WithData(leftSideData));
            }

            if (!string.IsNullOrEmpty(rightSideData))
            {
                sessionBuilder.WithRightSideMessage(new MessageBuilder().WithData(rightSideData));
            }

            return sessionBuilder.Build();
        }
    }
}

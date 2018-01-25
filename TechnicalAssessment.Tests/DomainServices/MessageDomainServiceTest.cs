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

    [TestClass]
    public class MessageDomainServiceTest
    {
        [TestMethod]
        public void ShouldHaveSameContent()
        {
            IMessageDomainService domainService = new MessageDomainService();

            const string data =
                "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            Session session = this.CreateTestSession(data, data);

            domainService.AnalyzeMessages(session.LeftSide, session.RightSide).ShouldBe("Content is the same");
        }

        [TestMethod]
        public void ShouldHaveDifferentSizes()
        {
            IMessageDomainService domainService = new MessageDomainService();

            const string first =
                "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            const string second =
                "LTAiGQezNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            Session session = this.CreateTestSession(first, second);

            domainService.AnalyzeMessages(session.LeftSide, session.RightSide).ShouldBe("Sizes are different");
        }

        [TestMethod]
        public void ShouldHaveBothSides()
        {
            IMessageDomainService domainService = new MessageDomainService();

            const string first =
                "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            Session session = this.CreateTestSession(first, string.Empty);

            domainService.AnalyzeMessages(session.LeftSide, session.RightSide).ShouldBe("Session must have both sides");
        }

        [TestMethod]
        public void ShouldHaveDifferencesDetected()
        {
            IMessageDomainService domainService = new MessageDomainService();

            const string first =
                "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            const string second =
                "MTIzMDA3OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxWzOxcm51MTJub2kyMTBuMQ==";

            Session session = this.CreateTestSession(first, second);

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

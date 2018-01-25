// <copyright file="Program.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Console
{
    using System;
    using TechnicalAssessment.Domain;
    using TechnicalAssessment.Domain.Builders;
    using TechnicalAssessment.WebApi.Controllers;

    /// <summary>
    /// Program runtime initialization class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runtime initialization method.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            string first =
                "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

            string second =
                "MTIzMDA3OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
                "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxWzOxcm51MTJub2kyMTBuMQ==";

            Message firstMessage = new MessageBuilder().WithData(first).Build();

            Message secondMessage = new MessageBuilder().WithData(second).Build();

            MessageController controller = new MessageController();

            Guid firstMessageId = controller.Save(firstMessage).GetAwaiter().GetResult();
            Guid secondMessageId = controller.Save(secondMessage).GetAwaiter().GetResult();

            Console.WriteLine(firstMessageId);
            Console.WriteLine(secondMessageId);

            Console.WriteLine(controller.Get(firstMessageId, secondMessageId).GetAwaiter().GetResult());

            Console.ReadLine();
        }
    }
}

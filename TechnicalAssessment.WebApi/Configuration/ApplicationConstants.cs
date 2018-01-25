// <copyright file="ApplicationConsstants.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.WebApi.Configuration
{
    using System;

    public static class ApplicationConstants
    {
        public static Uri ServiceUri => new Uri(ServiceUriAddress);

        public static string ServiceUriAddress => "http://localhost:8888";

        public const string DefaultData =
            "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
            "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

        public const string SameSizeDifferentData =
            "MTIzMDA3OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
            "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxWzOxcm51MTJub2kyMTBuMQ==";

        public const string DifferentLargerData =
            "LTAiGQezNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
            "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";
    }
}

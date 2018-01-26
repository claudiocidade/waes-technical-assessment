// <copyright file="ApplicationConstants.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.WebApi.Configuration
{
    using System;

    /// <summary>
    /// Application constants for configuration settings.
    /// </summary>
    public static class ApplicationConstants
    {
        /// <summary>
        /// Default base64 encoded binary data.
        /// </summary>
        public const string DefaultData =
            "MTIzNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
            "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

        /// <summary>
        /// Default base64 encoded binary data with size modifications.
        /// </summary>
        public const string SameSizeDifferentData =
            "MTIzMDA3OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
            "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxWzOxcm51MTJub2kyMTBuMQ==";

        /// <summary>
        /// Default base64 encoded binary data with minimum modifications but preserved size.
        /// </summary>
        public const string DifferentLargerData =
            "LTAiGQezNDU2OGtqbmFzbG5rYWZza25sc2FrbGZzbmtsZnNhbnNmYW5sZn" +
            "NhbmxvaXdqaTIxNDIxb2lqNDI4OXUyaDIxYnUxcm51MTJub2kyMTBuMQ==";

        /// <summary>
        /// Service address base path URI.
        /// </summary>
        public static Uri ServiceUri => new Uri(ServiceUriAddress);

        /// <summary>
        /// Service address base path string.
        /// </summary>
        public static string ServiceUriAddress => "http://localhost:8888";
    }
}

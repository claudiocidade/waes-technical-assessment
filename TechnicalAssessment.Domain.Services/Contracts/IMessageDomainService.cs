//  <copyright file="IMessageDomainService.cs" company="WAES">
//   Copyright (c) All rights reserved.
//  </copyright>
namespace TechnicalAssessment.Domain.Services.Contracts
{
    /// <summary>
    /// An implementation contract for <see cref="Message"/> domain logic service.
    /// </summary>
    public interface IMessageDomainService
    {
        /// <summary>
        /// Analyzes the messages and provide comparison diagnostics.
        /// </summary>
        /// <param name="left">Left side message.</param>
        /// <param name="right">Right side message.</param>
        /// <returns>A string containing information about the comparison process.</returns>
        string AnalyzeMessages(Message left, Message right);
    }
}
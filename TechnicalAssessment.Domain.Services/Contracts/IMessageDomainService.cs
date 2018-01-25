//  <copyright file="MessageDataAnalyzer.cs" company="WAES">
//   Copyright (c) All rights reserved.
//  </copyright>
namespace TechnicalAssessment.Domain.Services.Contracts
{
    public interface IMessageDomainService
    {
        string AnalyzeMessages(Message left, Message right);
    }
}
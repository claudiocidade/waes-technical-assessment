// <copyright file="IMessageRepository.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Data.Repositories.Contracts
{
    using System;
    using System.Threading.Tasks;
    using TechnicalAssessment.Domain;

    public interface IMessageRepository
    { 
        Task<Message> Get(Guid id);

        Task<Guid> Save(Message message);
    }
}
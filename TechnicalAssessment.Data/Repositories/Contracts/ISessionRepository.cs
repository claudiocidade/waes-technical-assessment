// <copyright file="ISessionRepository.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Data.Repositories.Contracts
{
    using System.Threading.Tasks;
    using TechnicalAssessment.Domain;

    public interface ISessionRepository
    {
        Task<Session> Get(string id);

        Task<bool> Save(Session session);
    }
}
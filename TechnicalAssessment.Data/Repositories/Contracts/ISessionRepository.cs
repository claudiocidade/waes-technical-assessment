// <copyright file="ISessionRepository.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Data.Repositories.Contracts
{
    using System.Threading.Tasks;
    using TechnicalAssessment.Domain;

    /// <summary>
    /// A <see cref="Session"/> repository implementation contract.
    /// </summary>
    public interface ISessionRepository
    {
        /// <summary>
        /// Gets an existing instance of <see cref="Session"/>.
        /// </summary>
        /// <param name="id"><see cref="Session"/> identification number.</param>
        /// <returns>An instance of the existing <see cref="Session"/>.</returns>
        Task<Session> Get(string id);

        /// <summary>
        /// Saves a new instance of <see cref="Session"/>.
        /// </summary>
        /// <param name="session">An instance of the <see cref="Session"/> that is going to be saved.</param>
        /// <returns>True when sucessfully persisted.</returns>
        Task<bool> Save(Session session);
    }
}
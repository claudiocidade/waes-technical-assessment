// <copyright file="IMessageRepository.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Data.Repositories.Contracts
{
    using System;
    using System.Threading.Tasks;
    using TechnicalAssessment.Domain;

    /// <summary>
    /// A <see cref="Message"/> repository implementation contract.
    /// </summary>
    public interface IMessageRepository
    { 
        /// <summary>
        /// Gets an existing <see cref="Message"/> by identification. 
        /// </summary>
        /// <param name="id"><see cref="Message"/> identification.</param>
        /// <returns>An existing <see cref="Message"/>.</returns>
        Task<Message> Get(Guid id);

        /// <summary>
        /// Saves a new <see cref="Message"/>.
        /// </summary>
        /// <param name="message">An instance of the <see cref="Message"/> to be saved.</param>
        /// <returns>The newly saved <see cref="Message"/> identification.</returns>
        Task<Guid> Save(Message message);
    }
}
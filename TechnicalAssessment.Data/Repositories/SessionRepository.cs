// <copyright file="SessionRepository.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using TechnicalAssessment.Data.Repositories.Contracts;
    using TechnicalAssessment.Domain;
    using TechnicalAssessment.Domain.Builders;

    /// <summary>
    /// A <see cref="Session"/> repository.
    /// </summary>
    public class SessionRepository : ISessionRepository
    {
        /// <summary>
        /// An instance of the MongoDB client that is going to be used for persistence.
        /// </summary>
        private readonly MongoClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionRepository"/> class.
        /// </summary>
        /// <param name="client">An instance of the <see cref="MongoClient"/> used by this repository.</param>
        public SessionRepository(MongoClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Gets an existing instance of <see cref="Session"/>.
        /// </summary>
        /// <param name="id"><see cref="Session"/> identification number.</param>
        /// <returns>An instance of the existing <see cref="Session"/>.</returns>
        async Task<Session> ISessionRepository.Get(string id)
        {
            IMongoCollection<Session> collection = this.GetCollection();
        
            IAsyncCursor<Session> sessions = await collection.FindAsync(s => s.Id == id);

            await sessions.MoveNextAsync();

            Session session = new SessionBuilder(id).Build();

            if (!sessions.Current.Any())
            {
                await collection.InsertOneAsync(session);
            }
            else
            {
                session = sessions.Current.First();
            }

            return session;
        }

        /// <summary>
        /// Saves a new instance of <see cref="Session"/>.
        /// </summary>
        /// <param name="session">An instance of the <see cref="Session"/> that is going to be saved.</param>
        /// <returns>True when sucessfully persisted.</returns>
        async Task<bool> ISessionRepository.Save(Session session)
        {
            IMongoCollection<Session> collection = this.GetCollection();

            await collection.FindOneAndReplaceAsync(s => s.Id == session.Id, session);

            return true;
        }

        /// <summary>
        /// Gets a collection from the selected mongo database.
        /// </summary>
        /// <returns>An instance of the collection representation class.</returns>
        private IMongoCollection<Session> GetCollection()
        {
            IMongoDatabase db = this.client.GetDatabase("waes");
            
            return db.GetCollection<Session>("session");
        }
    }
}
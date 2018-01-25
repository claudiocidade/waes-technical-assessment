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

    public class SessionRepository : ISessionRepository
    {
        private readonly MongoClient client;

        public SessionRepository(MongoClient client)
        {
            this.client = client;
        }

        async Task<Session> ISessionRepository.Get(string id)
        {
            IMongoCollection<Session> collection = GetCollection();
        
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

        async Task ISessionRepository.Save(Session session)
        {
            IMongoCollection<Session> collection = GetCollection();

            await collection.FindOneAndReplaceAsync(s => s.Id == session.Id, session);
        }

        private IMongoCollection<Session> GetCollection()
        {
            IMongoDatabase db = client.GetDatabase("waes");
            
            return db.GetCollection<Session>("session");
        }
    }
}
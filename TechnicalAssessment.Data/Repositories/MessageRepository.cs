// <copyright file="MessageRepository.cs" company="WAES">
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

    public class MessageRepository : IMessageRepository
    {
        private readonly MongoClient client;

        public MessageRepository(MongoClient client)
        {
            this.client = client;
        }

        async Task<Message> IMessageRepository.Get(Guid id)
        {
            IMongoCollection<Message> collection = GetCollection();

            IAsyncCursor<Message> messages = await collection.FindAsync(s => s.Id == id);

            await messages.MoveNextAsync();

            if (!messages.Current.Any())
            {
                return null;
            }
            else
            {
                return messages.Current.First();
            }
        }

        async Task<Guid> IMessageRepository.Save(Message message)
        {
            IMongoCollection<Message> collection = GetCollection();

            await collection.InsertOneAsync(message);

            return message.Id;
        }

        private IMongoCollection<Message> GetCollection()
        {
            IMongoDatabase db = client.GetDatabase("waes");
            
            return db.GetCollection<Message>("message");
        }
    }
}
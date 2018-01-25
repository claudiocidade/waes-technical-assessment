// //  <copyright file="DatabaseClient.cs" company="Personow">
// //  Copyright (c) Personow. All rights reserved.
// //  </copyright>
namespace TechnicalAssessment.Data
{
    using MongoDB.Driver;

    public class DatabaseClient
    {
        public static MongoClient Create(string connectionString)
        {
            return new MongoClient(connectionString);
        } 
    }
}
// //  <copyright file="DatabaseClient.cs" company="WAES">
// //  Copyright (c) WAES. All rights reserved.
// //  </copyright>
namespace TechnicalAssessment.Data
{
    using MongoDB.Driver;

    /// <summary>
    /// Database client creator.
    /// </summary>
    public class DatabaseClient
    {
        public static MongoClient Create(string connectionString)
        {
            return new MongoClient(connectionString);
        } 
    }
}
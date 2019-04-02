using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.MongoDb.Base
{
    public class BaseMongoContext : MongoClient
    {
        public BaseMongoContext(string connectString, string dbName) : base()
        {
            var client = new MongoClient(connectString);
            var db = client.GetDatabase(dbName);
            MongoClient = client;
            MongoDataBase = db;
        }

        public MongoClient MongoClient { get; set; }
        public IMongoDatabase MongoDataBase { get; set; }
    }
}

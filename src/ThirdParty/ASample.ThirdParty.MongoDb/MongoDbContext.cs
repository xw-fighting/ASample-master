using ASample.ThirdParty.MongoDb.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.MongoDb
{
    public class MongoDbContext : BaseMongoContext
    {
        public MongoDbContext(string connectString,string dbName) : base(connectString, dbName)
        {
            
        }

        
    }
}

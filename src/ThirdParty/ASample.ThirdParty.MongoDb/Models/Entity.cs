using MongoDB.Bson;

namespace ASample.ThirdParty.MongoDb.Models
{
    public abstract class EntityWithTypedId<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class Entity : EntityWithTypedId<ObjectId>
    {

    }
}

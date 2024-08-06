using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EntityLayer.PanteonEntity.MongoEntity.BaseEntity
{
    public abstract class BaseEntityModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonIgnore]
        public string IdString => Id.ToString();

        public int ObjectStatus { get; set; }

        public int AddUserId { get; set;}

    }
}

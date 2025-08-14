using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ProjectManagement.Infrastructure.Persistence
{
    public static class MongoDbConfiguration
    {
        public static void Configure()
        {
            // La lógica para registrar el serializador se mueve aquí.
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
    }
}
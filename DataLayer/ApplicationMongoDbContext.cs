using EntityLayer.PanteonEntity.MongoEntity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace DataLayer
{
    public class ApplicationMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public IMongoDatabase Database => _database;

        public ApplicationMongoDbContext()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var mongoSettings = configuration.GetSection(nameof(MongoSettings)).Get<MongoSettings>();
            var mongoClient = new MongoClient(mongoSettings?.ConnectionString);
            _database = mongoClient.GetDatabase(mongoSettings?.DatabaseName);
        }

        public ApplicationMongoDbContext(IMongoClient mongoClient, IOptions<MongoSettings> settings)
        {
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        }


        public void LoadSamples()
        {
            var collection = _database.GetCollection<PanteonBuildingType>("PanteonBuildingType");

            if (!collection.AsQueryable().Any())
            {
                var defaultBuildingTypes = new List<PanteonBuildingType>
                {
                    new() { Name = "Çiftlik", ObjectStatus = 1 },
                    new() { Name = "Akademi", ObjectStatus = 1 },
                    new() { Name = "Karargah", ObjectStatus = 1 },
                    new() { Name = "Kereste Fabrikası", ObjectStatus = 1 },
                    new() { Name = "Kışla", ObjectStatus = 1 },
                };
                collection.InsertMany(defaultBuildingTypes);
            }
        }
    }


    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
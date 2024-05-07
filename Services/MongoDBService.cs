using KnightsApi.Configurations;
using KnightsApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KnightsApi.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Knight> _knightsCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.Database);
            _knightsCollection = mongoDatabase.GetCollection<Knight>("knights");
        }

        public async Task<List<Knight>> GetAllKnightsAsync() =>
            await _knightsCollection.Find(_ => true).ToListAsync();

        public async Task<Knight?> GetKnightAsync(string id) =>
            await _knightsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateKnightAsync(Knight newKnight) =>
            await _knightsCollection.InsertOneAsync(newKnight);

        public async Task UpdateKnightAsync(string id, Knight updatedKnight) =>
            await _knightsCollection.ReplaceOneAsync(x => x.Id == id, updatedKnight);

        public async Task DeleteKnightAsync(string id) =>
            await _knightsCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Knight>> GetHeroesAsync()
        {
            var heroes = await _knightsCollection
                .Find(knight => knight.Attack >= 20) // Exemplo de filtro, pode ser ajustado
                .ToListAsync();
            return heroes;
        }

    }
}

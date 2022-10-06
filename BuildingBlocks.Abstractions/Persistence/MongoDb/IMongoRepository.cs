using BuildingBlocks.Abstractions.Domain;

namespace BuildingBlocks.Abstractions.Persistence.MongoDb;

public interface IMongoRepository<TEntity, in TId> : IRepository<TEntity, TId>
    where TEntity : class, IHaveIdentity<TId>
{
}
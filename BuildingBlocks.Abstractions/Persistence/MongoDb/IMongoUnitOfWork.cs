namespace BuildingBlocks.Abstractions.Persistence.MongoDb;

public interface IMongoUnitOfWork<out TContext> : IUnitOfWork<TContext> 
    where TContext : class, IMongoDbContext
{
}
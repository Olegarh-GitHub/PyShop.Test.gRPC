namespace PyShop.gRPC.Infrastructure.Interfaces;

public interface IReadRepository<TEntity>
{
    public IQueryable<TEntity> ReadAsQueryable();
}
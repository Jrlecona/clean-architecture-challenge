using CleanArchitectureChallenge.Domain.Entities;

namespace CleanArchitectureChallenge.Domain.Interfaces
{
    /// <summary>
    /// Contract for product data persistence.
    /// </summary>
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}

using CleanArchitectureChallenge.Domain.Entities;
using CleanArchitectureChallenge.Domain.Interfaces;

namespace CleanArchitectureChallenge.Infrastructure.Repositories
{
    /// <summary>
    /// In-memory implementation of product repository.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> _products = new();

        public Task<IEnumerable<Product>> GetAllAsync() =>
            Task.FromResult<IEnumerable<Product>>(_products);

        public Task<Product?> GetByIdAsync(Guid id) =>
            Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public Task AddAsync(Product product)
        {
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product product) => Task.CompletedTask;

        public Task DeleteAsync(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                _products.Remove(product);
            return Task.CompletedTask;
        }
    }
}

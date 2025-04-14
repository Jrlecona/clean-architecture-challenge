using CleanArchitectureChallenge.Application.DTOs;
using CleanArchitectureChallenge.Application.Interfaces;
using CleanArchitectureChallenge.Domain.Entities;
using CleanArchitectureChallenge.Domain.Interfaces;

namespace CleanArchitectureChallenge.Application.Services
{
    /// <summary>
    /// Handles business logic for products.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo) => _repo = repo;

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _repo.GetAllAsync();
            return products.Select(
                p =>
                    new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    }
            );
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            return product == null
                ? null
                : new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                };
        }

        public async Task CreateProductAsync(ProductDto dto)
        {
            var product = new Product(dto.Name, dto.Price);
            await _repo.AddAsync(product);
        }

        public async Task UpdateProductAsync(ProductDto dto)
        {
            var product = await _repo.GetByIdAsync(dto.Id);
            if (product == null)
                return;
            product.UpdatePrice(dto.Price);
            await _repo.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Guid id) => await _repo.DeleteAsync(id);
    }
}

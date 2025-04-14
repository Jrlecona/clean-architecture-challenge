using CleanArchitectureChallenge.Application.DTOs;

namespace CleanArchitectureChallenge.Application.Interfaces
{
    /// <summary>
    /// Contract for product service operations.
    /// </summary>
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task CreateProductAsync(ProductDto dto);
        Task UpdateProductAsync(ProductDto dto);
        Task DeleteProductAsync(Guid id);
    }
}

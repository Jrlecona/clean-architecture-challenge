namespace CleanArchitectureChallenge.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for product.
    /// </summary>
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}

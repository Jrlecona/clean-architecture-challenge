namespace CleanArchitectureChallenge.Domain.Entities
{
    /// <summary>
    /// Represents a product in the ecommerce system.
    /// </summary>
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }

        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
        }
    }
}

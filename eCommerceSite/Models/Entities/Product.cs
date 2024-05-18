using eCommerceSite.Data;
using System.ComponentModel.DataAnnotations;

namespace eCommerceSite.Models.Entities
{
    public class Product : IComparable<Product>
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public Categories Category { get; set; }
        public List<Cart> Carts { get; set; } = new List<Cart>();

        public int CompareTo(Product? other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return (int)Math.Ceiling(Price - other.Price);
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Product)) return false;
            Product? other = obj as Product;
            if (other == null) return false;
            return Id == other.Id;
        }
    }
}

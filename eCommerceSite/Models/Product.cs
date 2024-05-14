
using eCommerceSite.Data;
using System.ComponentModel.DataAnnotations;

namespace eCommerceSite.Models
{
    public class Product : IComparable<Product>
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public Categories Category { get; set; }

        public int CompareTo(Product? other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return (int) Math.Ceiling(Price - other.Price);
        }
    }
}

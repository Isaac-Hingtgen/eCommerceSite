
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceSite.Models.Entities
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }
        public int? ProfileId { get; set; }

        [ForeignKey("ProfileId")] 
        public Profile? Profile { get; set; } = null;

        public ICollection<Product> Products { get; } = new SortedSet<Product>();


        public Cart(Profile user) : this()
        {
            Profile = user;
        }
        public Cart() { 
            Id = Guid.NewGuid().ToString();
        }

        public bool Add(Product product)
        {
            return ((SortedSet<Product>)Products).Add(product);
        }

        public bool Remove(int productId)
        {
            foreach (Product product in Products)
            {
                if (product.Id == productId)
                {
                    return Products.Remove(product);
                }
            }
            return false;
        }

        public void Add(Cart? cart)
        {
            if (cart is null) return;
            foreach (Product p in cart.Products)
            {
                Add(p);
            }
        }
    }
}

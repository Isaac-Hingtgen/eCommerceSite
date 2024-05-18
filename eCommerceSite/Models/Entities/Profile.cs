using eCommerceSite.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceSite.Models.Entities
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public byte[] Salt { get; set; }

        public string? CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart? ShoppingCart { get; set; }



        public Profile(string name, string pwd)
        {
            Name = name;
            Salt = Salts.GenerateSalt();
            Pwd = pwd;
           // Pwd = Salts.HashPassword(pwd, Salt);
        }

        public bool IsAuthenticated(string pwd)
        {
            return Pwd.Equals(Salts.HashPassword(pwd, Salt));
        }



    }
}

using Microsoft.AspNetCore.Identity;

namespace MexicanResturent.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }

    }
}

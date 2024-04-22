using Microsoft.AspNetCore.Identity;

namespace LinqToDatabase.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? BirthDate { get; set; }
    }
}

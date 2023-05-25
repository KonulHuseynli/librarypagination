using Microsoft.AspNetCore.Identity;

namespace UniversityProject.Models
{
    public class User:IdentityUser  
    {
        public string Fullname { get; set; }
    }
}

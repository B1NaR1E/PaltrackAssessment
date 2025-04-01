namespace Assessment.Api.Models;

public class ApplicationUser : IdentityUser
{
    public bool IsDeleted { get; set; }
}
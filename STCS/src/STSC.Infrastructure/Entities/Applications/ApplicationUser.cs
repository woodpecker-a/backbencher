using Microsoft.AspNetCore.Identity;
using System;

namespace STSC.Infrastructure.Entities.Applications
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

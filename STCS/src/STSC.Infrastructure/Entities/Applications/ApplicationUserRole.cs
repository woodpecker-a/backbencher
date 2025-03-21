using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace STSC.Infrastructure.Entities.Applications
{
    public class ApplicationUserRole
        : IdentityUserRole<Guid>
    {

    }
}

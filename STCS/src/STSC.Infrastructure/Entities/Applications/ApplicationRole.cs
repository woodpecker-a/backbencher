﻿using Microsoft.AspNetCore.Identity;

namespace STCS.Infrastructure.Entities.Applications;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
        : base()
    {
    }

    public ApplicationRole(string roleName)
        : base(roleName)
    {
    }
}

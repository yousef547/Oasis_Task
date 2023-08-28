﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace oasis.Entities
{
    public class AppRole :IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace oasis.Entities
{
    public class AppUser :IdentityUser<int>
    {
        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<ToDoUsers> ToDoUsers { get; set; }
    }
}

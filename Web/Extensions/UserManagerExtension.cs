using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<bool> IsInAdminRoleAsync(this UserManager<ApplicationUser> _userManager, ClaimsPrincipal user)
        {
            return await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(user.Identity.Name), "Admin");
        }
    }
}

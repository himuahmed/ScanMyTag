using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ScanMyTag.Models;

namespace ScanMyTag.Helpers
{
    public class AplicationUserClaimsPrincipalFactory: UserClaimsPrincipalFactory<UserModel, IdentityRole>
    {
        public AplicationUserClaimsPrincipalFactory(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> option) : base(userManager, roleManager, option)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserModel user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("name", user.Name ?? ""));
            return identity;
        }
    }
}

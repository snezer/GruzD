using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace GruzD.Web.Infrastructure.Security
{
    public class ClaimsLoader
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<ClaimsLoader> _logger;
        private const string _fileName = "appclaims.json";

        public ClaimsLoader(RoleManager<IdentityRole> roleManager, ILogger<ClaimsLoader> logger)
        {
            this._roleManager = roleManager;
            this._logger = logger;
        }

        public async Task Load()
        {
            var settingPath = Path.Combine(AppContext.BaseDirectory, _fileName);
            if (!File.Exists(settingPath))
            {
                return;
            }

            using (var reader = new StreamReader(settingPath))
            {
                string json = reader.ReadToEnd();
                var roles = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
                if (roles == null)
                {
                    return;
                }

                foreach (var role in roles)
                {
                    try
                    {
                        var identityRole = await this.GetOrAddRole(role.Key);
                        if (identityRole == null)
                        {
                            continue;
                        }

                        var roleClaims = (await this._roleManager.GetClaimsAsync(identityRole))
                            .Where(claim => claim.Type == KnownClaims.ClaimType)
                            .Select(claim => claim.Value)
                            .ToArray();

                        foreach (var claim in role.Value)
                        {
                            if (!KnownClaims.Claims.Values.Contains(claim))
                            {
                                this._logger.LogWarning($"Unsupported claim '{claim}'");
                                continue;
                            }

                            if (roleClaims.Contains(claim))
                            {
                                continue;
                            }

                            await this._roleManager.AddClaimAsync(identityRole, new System.Security.Claims.Claim(KnownClaims.ClaimType, claim));
                        }
                    }
                    catch (Exception ex)
                    {
                        this._logger.LogError(ex, $"Unhadled exception: '{role}'");
                    }
                }
            }
        }

        private async Task<IdentityRole> GetOrAddRole(string roleName)
        {
            var identityRole = await this._roleManager.FindByNameAsync(roleName);
            if (identityRole != null)
            {
                return identityRole;
            }

            identityRole = new IdentityRole
            {
                Name = roleName
            };

            var result = await this._roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return identityRole;
            }
            else
            {
                this._logger.LogError(string.Join(";", result.Errors.Select(error => $"{error.Code}: {error.Description}").ToArray()));
            }

            return null;
        }
    }
}

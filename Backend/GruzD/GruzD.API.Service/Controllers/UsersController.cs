using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GruzD.DataModel.Paging;
using GruzD.Web.Contracts.User;
using GruzD.Web.Data;
using GruzD.Web.Infrastructure.Extensions;
using GruzD.Web.Infrastructure.Security;
using GruzD.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace GruzD.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = KnownRoles.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<PagedItems<UserContract>> GetAll([FromQuery] PagedFilter pagedFilter)
        {
            var users = await this._userManager.Users
                .Where(user => !user.IsDeleted)
                .ApplyFilter(pagedFilter?.Filters)
                .ApplyOrderBy(pagedFilter)
                .Skip(pagedFilter.Page * pagedFilter.ItemsPerPage)
                .Take(pagedFilter.ItemsPerPage)
                .ToArrayAsync();

            var total = await this._userManager.Users
                .Where(user => !user.IsDeleted)
                .ApplyFilter(pagedFilter?.Filters)
                .CountAsync();

            var roleFilter = pagedFilter.Filters.Where(x => x.Name == "Role").FirstOrDefault();
            var userRoles = await this.GetUserRoles(users, roleFilter);

            var resultUsers = users
                .Where(user => userRoles.ContainsKey(user.Id))
                .Select(user => user.MapTo(userRoles))
                .ToArray();

            var resultTotal = resultUsers.Length;

            return new PagedItems<UserContract>()
            {
                Items = resultUsers,
                Total = resultTotal
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserContract>> GetUser(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            if (user == null || user.IsDeleted)
            {
                return this.NotFound();
            }

            var roles = await this.GetUserRoles(new[] { user }, null);

            return user.MapTo(roles);
        }

        [HttpPost]
        //[ModelStateValidation]
        public async Task<IActionResult> Create([FromBody] UserCreateContract contract)
        {
            var existsUser = await this._userManager.FindByEmailAsync(contract.Email);
            if (existsUser != null)
            {
                this.ModelState.AddModelError(nameof(UserContract.Email), "Пользователь с таким Email уже существует");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = contract.Email,
                Email = contract.Email,
                FullName = contract.FullName,
                Position = contract.Position,
                PhoneNumber = contract.Phone,
                EmailConfirmed = true,
                IsDeleted = false
            };

            var createResult = await this._userManager.CreateAsync(user, contract.Password);
            if (!createResult.Succeeded)
            {
                return this.BadRequest(createResult.Errors.ToDictionary(e => e.Code, e => e.Description));
            }

            var role = await this._roleManager.FindByIdAsync(contract.RoleId);

            var roleResult = await this._userManager.AddToRoleAsync(user, role.Name);
            if (!roleResult.Succeeded)
            {
                //Delete user?
                return this.BadRequest(roleResult.Errors.ToDictionary(e => e.Code, e => e.Description));
            }

            return this.Ok(user.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UserContract contract)
        {
            if (id != contract.Id)
            {
                return this.BadRequest();
            }

            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (user.UserName == user.Email)
            {
                user.UserName = contract.Email;
            }
            user.Email = contract.Email;
            user.PhoneNumber = contract.Phone;
            user.FullName = contract.FullName;
            user.Position = contract.Position;

            var updateResult = await this._userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return this.BadRequest(updateResult.Errors.ToDictionary(e => e.Code, e => e.Description));
            }

            var updateRole = await this._roleManager.FindByIdAsync(contract.RoleId);
            var existsRoles = await this._userManager.GetRolesAsync(user);
            if (!existsRoles.Contains(updateRole.Name))
            {
                await this._userManager.RemoveFromRolesAsync(user, existsRoles);
                await this._userManager.AddToRoleAsync(user, updateRole.Name);
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserContract>> Delete(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return this.NotFound();
            }

            await this._userManager.DeleteAsync(user);

            return this.Ok();
        }

        private async Task<Dictionary<string, string>> GetUserRoles(ApplicationUser[] applicationUsers, PropertyFilter filter)
        {
            if (applicationUsers == null)
            {
                return new Dictionary<string, string>();
            }

            var userIds = applicationUsers.Select(user => user.Id).ToArray();
            IdentityRole filterRole = null;
            if (filter != null)
            {
                filterRole = this._dbContext.Roles.Where(x => x.Name == filter.Value).FirstOrDefault();
            }

            if (filterRole == null && filter != null)
            {
                return new Dictionary<string, string>();
            }
            else
            {
                return filter == null
                    ? await this._dbContext.UserRoles
                        .Where(role => userIds.Contains(role.UserId))
                        .ToDictionaryAsync(role => role.UserId, role => role.RoleId)
                    : await this._dbContext.UserRoles
                        .Where(role => role.RoleId == filterRole.Id)
                        .Where(role => userIds.Contains(role.UserId))
                        .ToDictionaryAsync(role => role.UserId, role => role.RoleId);
            }
        }
    }

    static class UserContractExtensions
    {
        public static IQueryable<ApplicationUser> ApplyFilter(this IQueryable<ApplicationUser> applicationUsers, PropertyFilter[] filters)
        {
            if (filters == null)
            {
                return applicationUsers;
            }

            foreach (var filter in filters)
            {
                switch (filter.Name)
                {
                    case nameof(ApplicationUser.UserName):
                        applicationUsers = applicationUsers.Where(user => user.UserName.Contains(filter.Value));
                        break;

                    case nameof(ApplicationUser.FullName):
                        applicationUsers = applicationUsers.Where(user => user.FullName.Contains(filter.Value));
                        break;

                    case nameof(ApplicationUser.Email):
                        applicationUsers = applicationUsers.Where(user => user.Email.Contains(filter.Value));
                        break;

                    case nameof(ApplicationUser.Position):
                        applicationUsers = applicationUsers.Where(user => user.Position.Contains(filter.Value));
                        break;
                }
            }

            return applicationUsers;
        }

        public static UserContract MapTo(this ApplicationUser user, Dictionary<string, string> userRoles = null)
        {
            if (user == null)
            {
                return null;
            }

            var userRole = string.Empty;
            if (userRoles != null && userRoles.TryGetValue(user.Id, out string temp))
            {
                userRole = temp;
            }

            return new UserContract
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Position = user.Position,
                Phone = user.PhoneNumber,
                RoleId = userRole
            };
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.UserVm;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegationsMVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public void RemoveRoleFromUser(string id, string role)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if(user == null)
            {
                return;
            }
            _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddRolesToUser(string idUser, IEnumerable<string> role)
        {
            IdentityResult result;
            var user = _userManager.FindByIdAsync(idUser).Result;
            if(user == null)
            {
                return null;
            }
            role = RemoveDuplicateRoles(user, role);
            result = await _userManager.AddToRolesAsync(user, role);
            return result;
        }

        public IQueryable<string> GetRolesByUser(string id)
        {
            var user =  _userManager.FindByIdAsync(id).Result;
            var roles = _userManager.GetRolesAsync(user).Result.AsQueryable();
            return roles;
        }

        public IQueryable<RoleVm> GetAllRoles()
        {
            var rolesVm = _roleManager.Roles?.ProjectTo<RoleVm>(_mapper.ConfigurationProvider);
            return rolesVm;
        }

        public ListUsersForListVm GetAllUsers()
        {
            var users = _userManager.Users.ProjectTo<UserForListVm>(_mapper.ConfigurationProvider).ToList();
            var usersVm = new ListUsersForListVm()
            {
                Users = users,
                Count = users.Count
            };
            return usersVm;
        }

        public UserDetailVm GetUserDetails(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var userVm = _mapper.Map<UserDetailVm>(user);
            userVm.UserRoles = GetRolesByUser(user.Id).ToList();
            userVm.Roles = GetAllRoles().ToList();
            return userVm;
        }

        private List<string> RemoveDuplicateRoles(IdentityUser user, IEnumerable<string> roles)
        {

            var userRoles = _userManager.GetRolesAsync(user).Result.ToList();
            var rolesToAdd = roles.Where(r => !userRoles.Contains(r)).ToList();
            return rolesToAdd;
        }
    }
}

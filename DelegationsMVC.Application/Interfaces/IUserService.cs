using DelegationsMVC.Application.ViewModels.UserVm;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegationsMVC.Application.Interfaces
{
    public interface IUserService
    {
        ListUsersForListVm GetAllUsers();
        IQueryable<RoleVm> GetAllRoles();
        IQueryable<string> GetRolesByUser(string id);
        UserDetailVm GetUserDetails(string id);
        //void AddRolesToUser(string idUser, IEnumerable<string> role);
        Task<IdentityResult> ChangeUserRolesAsync(string idUser, IEnumerable<string> role);
        void RemoveRoleFromUser(string id, string role);
        Task<IdentityResult> DeleteUserAsync(string id);
    }
}

using DelegationsMVC.Application.ViewModels.UserVm;
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
        void AddRoleToUser(string idUser, string idRole);
        void RemoveRoleFromUser(string id, string role);
    }
}

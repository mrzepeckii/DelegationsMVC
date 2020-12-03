using DelegationsMVC.Application.ViewModels.UserVm;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.Interfaces
{
    public interface IUserService
    {
        List<UserDetailVm> GetAllUsers();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.UserVm
{
    public class ListUsersForListVm
    {
        public List<UserForListVm> Users { get; set; }
        public int Count { get; set; }
    }
}

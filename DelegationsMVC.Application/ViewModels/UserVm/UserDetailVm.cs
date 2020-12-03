using AutoMapper;
using DelegationsMVC.Application.Mapping;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.UserVm
{
    public class UserDetailVm : IMapFrom<IdentityUser>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IdentityUser, UserDetailVm>();
        }
    }
}

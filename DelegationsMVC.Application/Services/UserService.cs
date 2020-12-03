using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.UserVm;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public List<UserDetailVm> GetAllUsers()
        {
            var users = _userManager.Users.ProjectTo<UserDetailVm>(_mapper.ConfigurationProvider).ToList();
            return users;
        }
    }
}

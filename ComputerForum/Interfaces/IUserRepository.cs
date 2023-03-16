﻿using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(UserVM userVM);
    }
}
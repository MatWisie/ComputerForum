﻿using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ComputerForum.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumDbContext _context;
        public UserRepository(ForumDbContext context)
        {
            _context = context;
        }

        public User? GetUser(UserLoginVM userVM)
        {
            return _context.Users.FirstOrDefault(e => e.Name == userVM.Name);
        }
        public int? GetUserIdByName(string userName)
        {
            return _context.Users.FirstOrDefault(e => e.Name == userName).Id;
        }
        public User? GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(e => e.Id == userId);
        }
        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(e => e.Email == email);
        }
        public User? GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(e => e.Name == name);
        }
        public User? GetUserByIdWithInclude(int userId)
        {
            return _context.Users.Include(e => e.Topics).Include(e => e.Comments).FirstOrDefault(e => e.Id == userId);
        }
        public bool CheckIfUserExists(UserRegisterVM userVM)
        {
            return _context.Users.Any(e => e.Name == userVM.Name);
        }

        public int CountUsers()
        {
            return _context.Users.Count();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}

using CarWashAppFinal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarWashAppFinal.Repositories
{
    public interface IUserRepository
    {
        void Register(User user);
        User? Login (string username);
        void Save();
        List<User> Load();
    }
}

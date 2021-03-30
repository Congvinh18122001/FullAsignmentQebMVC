using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public interface IAccountRepository
    {
        User AddUser(User user);
        User GetUserByID(int id);
    }
}
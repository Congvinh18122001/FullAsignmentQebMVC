using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstAppMVC.Models;
namespace FirstAppMVC.Services
{
    public interface ITestService
    {
        List<Role> GetRoles();
        List<User> GetUsers();
    }
}
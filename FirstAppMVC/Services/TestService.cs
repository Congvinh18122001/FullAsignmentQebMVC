using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAppMVC.Models;
namespace FirstAppMVC.Services
{
    public class TestService : ITestService
    {
        private readonly FirstAppMVCContext _dbContext;

        public TestService(FirstAppMVCContext dbContext)
        {
            _dbContext = dbContext;
        }
       public List<Role> GetRoles(){  
           return _dbContext.Roles.ToList();
       }
       public List<User> GetUsers(){
           return _dbContext.Users.ToList();
       }
    }
}
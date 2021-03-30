using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FirstAppMVC.Models
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FirstAppMVCContext _dbContext;

        public AccountRepository(FirstAppMVCContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User AddUser(User user){
           if (user !=null)
           {
               _dbContext.Users.Add(user);
               _dbContext.SaveChanges();
               return user;
           }
           return null;
        }
        public User GetUserByID(int id){
            return _dbContext.Users.SingleOrDefault(u=>u.UserID == id);
        }
    }
}
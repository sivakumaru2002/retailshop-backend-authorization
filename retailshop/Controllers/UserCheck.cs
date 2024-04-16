using retailshop.Models;
using retailshop.Repository;

namespace retailshop.Controllers
{
    public class UserCheck : IUserCheck
    {
        private readonly AppDbContext _appDbContext;

        public UserCheck(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool CheckUser(string user)
        {
            if (user == null)
            {
                return false;
            }

            var exists = _appDbContext.UserModel.Any(u => u.Username == user);
            return exists;
        }
    }
}

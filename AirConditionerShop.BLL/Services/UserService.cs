using AirConditionerShop.DAL.Entities;
using AirConditionerShop.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop.BLL.Services
{
    public class UserService
    {
        private UserRepository _userRepo = new();

        public StaffMember? Authenticate(string email, string password)
        {
            return _userRepo.GetUser(email, password);
        }
    }
}

using AirConditionerShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop.DAL.Repository
{
    public class UserRepository
    {
        private AirConditionerShop2024DbContext _context;

        public StaffMember? GetUser(string email, string password)
        {
            _context = new();
            return _context.StaffMembers.FirstOrDefault(x => x.EmailAddress.ToLower().Trim()==email.ToLower().Trim() && x.Password==password);
        }
    }
}

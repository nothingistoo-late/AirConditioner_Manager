using AirConditionerShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop.DAL.Repository
{
    public class AirConRepository
    {
        // logic/ flow chạy
        // GUI(WPF) -- BLL (Service) -- DAL (REPO) -- DBCONTEXT -- TABLE THẬT 

        // thằng repo là thằng lo giao tiếp trực tiếp với data base
        // nó cần gọi DBCONTEXT - giao tiếp với cơ sở dữ liệu do nó có kết nối với SEVER 

        private AirConditionerShop2024DbContext _context; // không new, khi nào xài mới new 

        // _Context đại diện cho database, nó quản lí 3 cái table
        // giờ ra sẽ CRUD trên table AIRCONDITIONER, nhờ context giúp xuống DB
        // CRUD TABLE
        
        public List<AirConditioner> GetAll()
        {
            _context = new();

            return _context.AirConditioners.ToList();
            //              trả về DBSet mình convert thành List
            // đã select * from AirConditioner
        }
    }
}

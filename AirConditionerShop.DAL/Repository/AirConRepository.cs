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

        // hàm create(), update(), delete() đều nhận vào 1 object
        // gọi dbcontext.aircons là cái arrayList, mà arrayList là cái túi 3 gang, là cái cabinet, thoải mái cập nhật, .add(), .remove(), .delete()

        public void Delete(AirConditioner obj)// obj muốn xóa, có id muốn xóa, nằm ở selected ở gui
        {
            _context.AirConditioners.Remove(obj);
            // build câu delete from where obj.ID
            _context.SaveChanges(); // chính thức lệnh xóa trong RAM
        }

        public void Update(AirConditioner obj)// obj muốn update, có id muốn update, nằm ở selected ở gui
        {
            _context.AirConditioners.Update(obj);
            // build câu cập nhật from where obj.ID
            _context.SaveChanges(); // chính thức lệnh xóa trong RAM
        }

        public void Create(AirConditioner obj)// obj muốn Create, có id muốn create, nằm ở selected ở gui
        {
            _context.AirConditioners.Add(obj);
            // build câu insert into
            _context.SaveChanges(); // chính thức lệnh xóa trong RAM
        }

    }
}

using AirConditionerShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop.DAL.Repository
{
    public class SupplierRepository
    {

        // thằng này chơi trược tiếp table suppliercompany


        // về lí thuyết, 1 table phải đủ hàm crud

        private AirConditionerShop2024DbContext? _context;

        public List<SupplierCompany> GetAll()
        {
            _context = new();
            return _context.SupplierCompanies.ToList();
        }
    }
}

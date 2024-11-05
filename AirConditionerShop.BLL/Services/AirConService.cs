using AirConditionerShop.DAL.Entities;
using AirConditionerShop.DAL.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop.BLL.Services
{
    public class AirConService
    {
        private AirConRepository _repo = new(); // new luôn

        // các hàm CRUD AIRCON nhưng gọi qua repo
        public List<AirConditioner> GetAllAirCons()
        {
            return _repo.GetAll();
        }

        public void Delete(AirConditioner obj)
        {
            _repo.Delete(obj); // xong, bán cái cho repo
                               // repo bán cái cho dbcontext
        }

        public void Update(AirConditioner obj) => _repo.Update(obj);
        

        public void Add(AirConditioner obj) => _repo.Create(obj);
        

        public List<AirConditioner> SearchAirConByFeatureAndQuantity(string? feature, int? quantity)
        {
            // tình huống hàm search kh có để cái gì, trả về full
            List<AirConditioner> result = _repo.GetAll();
            if (feature.IsNullOrEmpty() && !quantity.HasValue)
                return  result;

            if (!feature.IsNullOrEmpty())
                result = result.Where(x => x.FeatureFunction.ToLower().Contains(feature.ToLower())).ToList();
            if (quantity.HasValue)
            {
                result = result.Where(x => x.Quantity==quantity).ToList();
            }
            return result;

        }
    }
}

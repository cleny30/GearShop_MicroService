using BusinessObject.DTOS;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly HomeDAO homeDAO;

        public HomeRepository(HomeDAO homeDAO)
        {
            this.homeDAO = homeDAO;
        }

        //  public List<ProductData> GetSpecialSaleProducts() => homeDAO.GetSpecialSaleProducts(); 


        public List<ProductData> GetMouseProducts() => homeDAO.GetMouseProducts();

        public List<ProductData> GetKeyboardProducts() => homeDAO.GetKeyboardProducts();


        public List<ProductData> HomeGetData() => homeDAO.HomeGetData();

    }
}

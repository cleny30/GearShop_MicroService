using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;
using Repository.Core.Constants;

namespace Repository.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly HomeDAO homeDAO;

        public HomeRepository(HomeDAO homeDAO)
        {
            this.homeDAO = homeDAO;
        }

        //  public List<ProductData> GetSpecialSaleProducts() => homeDAO.GetSpecialSaleProducts(); 


        public List<ProductData> GetMouseProducts() => homeDAO.GetMouseProducts().Where(p => p.CateId.Equals((int)CategoryType.Mouse) && p.IsAvailable).ToList();

        public List<ProductData> GetKeyboardProducts() => homeDAO.GetKeyboardProducts().Where(p => p.CateId.Equals((int)CategoryType.Keyboard) && p.IsAvailable).ToList();


        public List<ProductData> HomeGetData() => homeDAO.HomeGetData();

    }
}

using BusinessObject.DTOS;

namespace Repository.IRepository
{
    public interface IHomeRepository
    {
        //List<ProductData> GetSpecialSaleProducts();
        List<ProductData> GetMouseProducts();
        List<ProductData> GetKeyboardProducts();
        List<ProductData> HomeGetData();
    }
}

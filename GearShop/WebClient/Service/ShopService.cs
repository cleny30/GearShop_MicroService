using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using WebClient.Models;

namespace WebClient.Service
{
    public class ShopService
    {
        public ShopModel GetData(List<ProductData> list,List<Brand> brandList,List<Category> categoryList, string sortFilter, string orderFilter, string category, string brand, int currentPage)
        {
            List<CategoryModel> categoryModels = GetCategoryList(categoryList).Where(c => c.IsAvailable == true).ToList();
            List<BrandModel> brandModels = GetBrandList(brandList).Where(b => b.IsAvailable == true).ToList();

            foreach (var item in brandModels)
            {
                item.quantity = list.Count(product => product.BrandId == item.BrandId);
            }

            // Calculate quantities for each category
            foreach (var item in categoryModels)
            {
                item.quantity = list.Count(product => product.CateId == item.CateId);
            }

            //Let out stock product -> end list
            list = list.OrderBy(p => p.ProQuan == 0 || !p.IsAvailable ? 1 : 0).ToList();

            list = SortProduct(list, sortFilter);

            list = OrderProduct(list, orderFilter);

            var selectedCategory = category.Split(',');
            var selectedBrand = brand.Split(',');
            var selectedCategoryIds = new List<int>();
            var selectedBrandIds = new List<int>();

            if (selectedCategory.Length > 0 && !selectedCategory.Equals(""))
            {
                foreach (var item in selectedCategory)
                {
                    if (int.TryParse(item, out int parsedCategory))
                    {
                        selectedCategoryIds.Add(parsedCategory);
                    }
                }

            }
            var productsByCategory = list.Where(pro => selectedCategoryIds.Contains(pro.CateId)).ToList();

            //Filter by brand
            if (selectedBrand.Length > 0 && !selectedBrand.Equals(""))
            {
                foreach (var item in selectedBrand)
                {
                    if (int.TryParse(item, out int parsedCategory))
                    {
                        selectedBrandIds.Add(parsedCategory);
                    }
                }

            }
            var productsByBrand = list.Where(pro => selectedBrandIds.Contains(pro.BrandId)).ToList();

            // Kích thước trang (số sản phẩm mỗi trang)
            int pageSize = 9;

            // Tính chỉ số bắt đầu của sản phẩm cần hiển thị
            int startIndex = (currentPage - 1) * pageSize;

            var productToshow = list.Skip(startIndex).Take(pageSize);
            var combineProduct = Enumerable.Empty<ProductData>();

            if (selectedCategoryIds.Count() > 0 && selectedBrandIds.Count() > 0)
            {
                combineProduct = productsByBrand.Intersect(productsByCategory);
            }
            else if (productsByCategory.Count() > 0)
            {
                combineProduct = productsByCategory;
            }
            else if (productsByBrand.Count() > 0)
            {
                combineProduct = productsByBrand;
            }


            if (combineProduct.Count() > 0)
            {
                productToshow = combineProduct.Skip(startIndex).Take(pageSize);
            }
            else if (combineProduct.Count() == 0 && (selectedBrandIds.Count() > 0 || selectedCategoryIds.Count() > 0))
            {
                productToshow = null;
            }

            int totalItems = selectedCategoryIds.Count == 0 && selectedBrandIds.Count == 0
                         ? list.Count()
                         : combineProduct.Count();

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            bool isFirstPage = currentPage == 1;
            bool isLastPage = currentPage == totalPages;

            ShopModel model = new ShopModel
            {
                products = productToshow!=null? productToshow.ToList():null,
                brandModels = brandModels,
                categoryModels = categoryModels,
                navigationModel = new PageNavigationModel
                {
                    currentPage = currentPage,
                    isFirstPage = isFirstPage,
                    isLastPage = isLastPage,
                    totalPage = totalPages
                },
                selectedBrand = selectedBrandIds,
                selectedCategory = selectedCategoryIds,
                sortFilter = sortFilter,
                orderFilter = orderFilter,
                saleAmount = list.Where(p => p.Discount > 0).Count(),
            };

            return model;
        }

        private List<ProductData> SortProduct(List<ProductData> list, string sortFilter)
        {
            var sortProduct = Enumerable.Empty<ProductData>();

            if (sortFilter.Length > 0 && !sortFilter.Equals(""))
            {
                var discount = Enumerable.Empty<ProductData>();
                var selling = Enumerable.Empty<ProductData>();
                if (sortFilter.Contains("discount"))
                {
                    discount = list.Where(pro => pro.Discount > 0).ToList();
                }

                if (discount.Count() > 0 && selling.Count() > 0)
                {
                    sortProduct = discount.Intersect(selling);
                }
                else if (sortFilter.Contains("discount"))
                {
                    sortProduct = discount;
                }
                else if (sortFilter.Contains("best_selling"))
                {
                    sortProduct = selling;
                }
            }

            if (sortProduct.Count() > 0)
            {
                list = sortProduct.ToList();
            }

            return list;
        }

        private List<ProductData> OrderProduct(List<ProductData> list, string orderFilter)
        {
            if (orderFilter.Equals("highest"))
            {
                list = list.OrderByDescending(product => product.ProPrice - (product.ProPrice * product.Discount) / 100).ToList();
            }
            else if (orderFilter.Equals("lowest"))
            {
                list = list.OrderBy(product => product.ProPrice - (product.ProPrice * product.Discount) / 100).ToList();
            }

            return list;
        }

        public List<BrandModel> GetBrandList(List<Brand> list)
        {
            List<BrandModel> brandModels = new List<BrandModel>();
            foreach (var item in list)
            {
                BrandModel b = new BrandModel();
                b.CopyProperties(item);
                brandModels.Add(b);
            }
            return brandModels;
        }

        public List<CategoryModel> GetCategoryList(List<Category> list)
        {
            List<CategoryModel> categoryModels = new List<CategoryModel>();
            foreach (var item in list)
            {
                CategoryModel c = new CategoryModel();
                c.CopyProperties(item);
                categoryModels.Add(c);
            }
            return categoryModels;
        }
    }
}

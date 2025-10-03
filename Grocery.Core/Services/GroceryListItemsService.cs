using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Linq;

namespace Grocery.Core.Services
{
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
        }

        public List<GroceryListItem> GetAll()
        {
            var items = _groceriesRepository.GetAll();
            FillService(items);
            return items;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            var items = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(items);
            return items;
        }

        public GroceryListItem Add(GroceryListItem item) => _groceriesRepository.Add(item);

        public GroceryListItem? Delete(GroceryListItem item)
        {
            throw new NotImplementedException();
        }

        public GroceryListItem? Update(GroceryListItem item) => _groceriesRepository.Update(item);

        public GroceryListItem? Get(int id) => _groceriesRepository.Get(id);

        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5)
        {
            var items = _groceriesRepository.GetAll();

            var grouped = items
                .GroupBy(i => i.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(i => i.Amount)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(topX)
                .ToList();

            var result = grouped
                .Select((g, index) =>
                {
                    var product = _productRepository.Get(g.ProductId) ?? new Product(0, "Onbekend", 0);
                    return new BestSellingProducts(
                        product.Id,
                        product.Name,
                        product.Stock,
                        g.TotalSold,
                        index + 1
                    );
                })
                .ToList();

            return result;
        }

        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (var g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new Product(0, "", 0);
            }
        }
    }
}

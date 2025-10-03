using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using System.Linq;

namespace Grocery.Core.Data.Repositories
{
    public class GroceryListItemsRepository : IGroceryListItemsRepository
    {
        private readonly List<GroceryListItem> groceryListItems;

        public GroceryListItemsRepository()
        {
            groceryListItems = new List<GroceryListItem>
            {
                new GroceryListItem(1, 1, 1, 3),
                new GroceryListItem(2, 1, 2, 1),
                new GroceryListItem(3, 1, 3, 4),
                new GroceryListItem(4, 2, 1, 2),
                new GroceryListItem(5, 2, 2, 5),
            };
        }

        public List<GroceryListItem> GetAll() => groceryListItems;

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId) =>
            groceryListItems.Where(g => g.GroceryListId == groceryListId).ToList();

        public GroceryListItem Add(GroceryListItem item)
        {
            int newId = groceryListItems.Any() ? groceryListItems.Max(g => g.Id) + 1 : 1;
            item.Id = newId;
            groceryListItems.Add(item);
            return item;
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            var listItem = groceryListItems.FirstOrDefault(i => i.Id == item.Id);
            if (listItem == null) return null;

            // Update in-place
            listItem.GroceryListId = item.GroceryListId;
            listItem.ProductId = item.ProductId;
            listItem.Amount = item.Amount;
            listItem.Product = item.Product;

            return listItem;
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            var existing = groceryListItems.FirstOrDefault(g => g.Id == item.Id);
            if (existing == null) return null;

            groceryListItems.Remove(existing);
            return existing;
        }

        public GroceryListItem? Get(int id) =>
            groceryListItems.FirstOrDefault(g => g.Id == id);
    }
}

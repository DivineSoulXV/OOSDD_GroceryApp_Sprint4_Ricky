using CommunityToolkit.Mvvm.ComponentModel;

namespace Grocery.Core.Models
{
    public partial class GroceryListItem : ObservableObject
    {
        public int Id { get; set; }
        public int GroceryListId { get; set; }
        public int ProductId { get; set; }

        [ObservableProperty]
        private int amount; // This generates public property Amount with INotifyPropertyChanged

        public Product? Product { get; set; }

        public GroceryListItem(int id, int groceryListId, int productId, int amount)
        {
            Id = id;
            GroceryListId = groceryListId;
            ProductId = productId;
            Amount = amount; // sets the generated property
        }
    }
}


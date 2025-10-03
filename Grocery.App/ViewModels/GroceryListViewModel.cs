using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; set; }
        private readonly IGroceryListService _groceryListService;
        private readonly Client _currentClient;

        public Client CurrentClient => _currentClient;

        public bool IsAdmin => _currentClient.Role == Role.Admin;

        public GroceryListViewModel(IGroceryListService groceryListService, IClientService clientService)
        {
            _groceryListService = groceryListService;

            _currentClient = clientService.GetAll().FirstOrDefault() ?? new Client(0, "None", "", "") { Role = Role.None };

            Title = "Boodschappenlijsten";
            GroceryLists = new(_groceryListService.GetAll());
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            var paramater = new Dictionary<string, object> { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            GroceryLists = new(_groceryListService.GetAll());
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }

        [RelayCommand]
        public async Task ShowBoughtProducts()
        {
            if (!IsAdmin) return;

            await Shell.Current.GoToAsync(nameof(Grocery.App.Views.BoughtProductsView));
        }
    }
}

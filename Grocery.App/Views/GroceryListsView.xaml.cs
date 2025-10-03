using Grocery.App.ViewModels;
using Microsoft.Maui.Controls;

namespace Grocery.App.Views;

public partial class GroceryListsView : ContentPage
{
    public GroceryListsView(GroceryListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        if (viewModel.IsAdmin)
        {
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Verkochte producten",
                Command = viewModel.ShowBoughtProductsCommand,
                IconImageSource = "some_icon.png"
            });
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is GroceryListViewModel bindingContext)
        {
            bindingContext.OnAppearing();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is GroceryListViewModel bindingContext)
        {
            bindingContext.OnDisappearing();
        }
    }
}

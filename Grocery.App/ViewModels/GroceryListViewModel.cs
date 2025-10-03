using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; set; }

        private readonly IGroceryListService _groceryListService;
        private readonly GlobalViewModel _global;

        // Huidige client komt uit GlobalViewModel
        public Client Client => _global.Client;

        public GroceryListViewModel(IGroceryListService groceryListService, GlobalViewModel global)
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            _global = global;

            GroceryLists = new(_groceryListService.GetAll());
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }

        // UC13: Toon BoughtProductsView alleen als Client admin is
        [RelayCommand]
        public async Task ShowBoughtProducts()
        {
            if (Client != null && Client.Role == Role.Admin)
            {
                await Shell.Current.GoToAsync(nameof(Views.BoughtProductsView), true);
            }
            // Anders: niets doen
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
    }
}

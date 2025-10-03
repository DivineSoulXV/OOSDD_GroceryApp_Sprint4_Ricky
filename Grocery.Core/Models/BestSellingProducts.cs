using CommunityToolkit.Mvvm.ComponentModel;

namespace Grocery.Core.Models
{
    public partial class BestSellingProducts : Model
    {
        public int Stock { get; set; }

        [ObservableProperty]
        private int nrOfSells;

        [ObservableProperty]
        private int ranking;

        public BestSellingProducts(int productId, string name, int stock, int nrOfSells, int ranking)
            : base(productId, name)
        {
            Stock = stock;
            NrOfSells = nrOfSells;
            Ranking = ranking;
        }
    }
}

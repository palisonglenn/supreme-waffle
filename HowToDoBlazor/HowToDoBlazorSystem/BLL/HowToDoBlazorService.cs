#nullable disable
using HowToDoBlazorSystem.DAL;
using HowToDoBlazorSystem.ViewModels;

namespace HowToDoBlazorSystem.BLL
{
    public class HowToDoBlazorService
    {
        #region Fields
        private readonly GroceryListContext? _groceryListContext;
        #endregion

        #region Constructors
        internal HowToDoBlazorService(GroceryListContext GroceryListContext)
        {
            _groceryListContext = GroceryListContext;
        }
        #endregion

        public List<GroceryListView> GetGroceryList()
        {
            return _groceryListContext.Categories
                .Select(x => new GroceryListView
                {
                    ID = x.CategoryID,
                    Name = x.Description,
                    Count = x.Products.Count(),
                    MinPrice = x.Products.Min(x => x.Price),
                    MaxPrice = x.Products.Max(x => x.Price),
                    MinDiscount = x.Products.Where(x => x.Discount > 0)
                                .Min(x => x.Discount),
                    MaxDiscount = x.Products.Max(x => x.Discount)

                })
                    .OrderBy(x => x.Name)
                    .ToList();
        }
    }
}

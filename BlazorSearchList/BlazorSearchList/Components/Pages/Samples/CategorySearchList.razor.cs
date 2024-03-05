using BlazorSearchListSystem.BLL;
using BlazorSearchListSystem.ViewModels;
using Microsoft.AspNetCore.Components;

namespace BlazorSearchList.Components.Pages.Samples
{
    public partial class CategorySearchList
    {
        #region Properties
        [Inject]
        protected CategoryService CategoryService { get; set; }
        #endregion

        #region Fields
        private List<GroceryListView> CategoryList = new();
        private string feedback = "";
        private string name = "Hai";


        #endregion

        #region Methods
        private void ShowFeedback()
        {
            feedback = "C# sucks";
        }

        private void HideFeedback()
        {
            feedback = "";
        }
        private void GetGroceryList()
        {
            try
            {
                CategoryList = CategoryService.GetGroceryList();
            }
            #region catch all exceptions
            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    feedback = error.Message;
                }
            }

            catch (ArgumentNullException ex)
            {
                feedback = BlazorHelperClass.GetInnerException(ex).Message;
            }

            catch (Exception ex)
            {
                feedback = BlazorHelperClass.GetInnerException(ex).Message;
            }
            #endregion
        }
        #endregion
    }
}

using HowToDoBlazorSystem.BLL;
using HowToDoBlazorSystem.ViewModels;
using HowToDoBlazorWebApp.Components;
using Microsoft.AspNetCore.Components;


namespace HowToDoBlazor.Components.Pages
{
    public partial class HowToDoBlazor
    {
        #region Properties
        [Inject]
        protected HowToDoBlazorService HowToDoBlazorService { get; set; }
        #endregion

        #region Fields
        private List<GroceryListView> howToDoBlazorView = new();
        private string feedback;
        #endregion

        #region Methods
        private void GetGroceryList()
        {
            try
            {
                howToDoBlazorView = HowToDoBlazorService.GetGroceryList();
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
            #endregion
        }
    }
}


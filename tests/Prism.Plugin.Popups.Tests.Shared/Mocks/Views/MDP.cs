using Xamarin.Forms;

namespace Prism.Plugin.Popups.Tests.Mocks.Views
{
    public class MDP : MasterDetailPage
    {
        public MDP()
        {
            Master = new ContentPage
            {
                Title = "Menu"
            };
        }
    }
}
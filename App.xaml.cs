using Microsoft.Maui.Controls;

namespace ArcheologyNewsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set the MainPage to the ArticlesPage
            MainPage = new NavigationPage(new Views.ArticlesPage());
        }
    }
}

using Microsoft.Maui.Controls;
using ArcheologyNewsApp.ViewModels;

namespace ArcheologyNewsApp.Views
{
    public partial class ScrollableArticlesPage : ContentPage
    {
        public ScrollableArticlesPage()
        {
            InitializeComponent();
            BindingContext = new ArticlesViewModel();
        }
    }
}
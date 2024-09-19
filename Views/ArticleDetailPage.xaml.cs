using Microsoft.Maui.Controls;

namespace ArcheologyNewsApp.Views
{
    public partial class ArticleDetailPage : ContentPage
    {
        public ArticleDetailPage(string articleTitle, string articleUrl)
        {
            InitializeComponent();

            // Set the title of the article
            Title = articleTitle;

            // Load the article URL in the WebView
            ArticleWebView.Source = articleUrl;
        }
    }
}

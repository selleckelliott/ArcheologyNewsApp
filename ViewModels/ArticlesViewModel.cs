using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ArcheologyNewsApp.Models;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ArcheologyNewsApp.ViewModels
{
    public class ArticlesViewModel : INotifyPropertyChanged
    {
        private readonly ArticleScraper _liveScienceScraper;
        private readonly ArkeoNewsScraper _arkeonewsScraper;

        public ObservableCollection<Article> LiveScienceArticles { get; set; }
        public ObservableCollection<Article> ArkeoNewsArticles { get; set; }

        public ArticlesViewModel()
        {
            _liveScienceScraper = new ArticleScraper();
            _arkeonewsScraper = new ArkeoNewsScraper();
            LiveScienceArticles = new ObservableCollection<Article>();
            ArkeoNewsArticles = new ObservableCollection<Article>();
            LoadArticles();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void LoadArticles()
        {
            // Load articles from LiveScience
            var liveScienceArticles = await _liveScienceScraper.GetArticlesAsync();
            foreach (var article in liveScienceArticles)
            {
                LiveScienceArticles.Add(article);
            }

            // Load articles from ArkeoNews
            var arkeoNewsArticles = await _arkeonewsScraper.GetArticlesAsync();
            foreach (var article in arkeoNewsArticles)
            {
                ArkeoNewsArticles.Add(article);
            }
        }

        // Command to handle opening the article in a browser or in-app WebView
        public ICommand OpenArticleCommand => new Command<Article>(async (article) =>
        {
            if (article != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Views.ArticleDetailPage(article.Title, article.Link.ToString()));
            }
        });
    }
}

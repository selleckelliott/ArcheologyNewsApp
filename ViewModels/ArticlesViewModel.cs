using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ArcheologyNewsApp.Models;

namespace ArcheologyNewsApp.ViewModels
{
    public class ArticlesViewModel : INotifyPropertyChanged
    {
        private readonly ArticleScraper _articleScraper;
        public ObservableCollection<Article> Articles { get; set; }

        public ArticlesViewModel()
        {
            _articleScraper = new ArticleScraper();
            Articles = new ObservableCollection<Article>();
            LoadArticles();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void LoadArticles()
        {
            var articles = await _articleScraper.GetArticlesAsync();
            foreach (var article in articles)
            {
                Articles.Add(article);
            }
        }
        // Command to handle opening the article in a browser
        public ICommand OpenArticleCommand => new Command<Article>(async (article) =>
        {
            if (article != null)
            {
                // Navigate to the ArticleDetailPage and pass the article's title and URL
                await Application.Current.MainPage.Navigation.PushAsync
                (new Views.ArticleDetailPage(article.Title, article.Link.ToString()));
            }
        });
    }
}

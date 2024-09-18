using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
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
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        private ObservableCollection<Article> _liveScienceArticles;
        private ObservableCollection<Article> _arkeoNewsArticles;

        public ObservableCollection<Article> LiveScienceArticles
        {
            get => _liveScienceArticles;
            set
            {
                _liveScienceArticles = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Article> ArkeoNewsArticles
        {
            get => _arkeoNewsArticles;
            set
            {
                _arkeoNewsArticles = value;
                OnPropertyChanged();
            }
        }

        public ArticlesViewModel()
        {
            _liveScienceScraper = new ArticleScraper();
            _arkeonewsScraper = new ArkeoNewsScraper();
            LiveScienceArticles = new ObservableCollection<Article>();
            ArkeoNewsArticles = new ObservableCollection<Article>();
            LoadArticlesAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task LoadArticlesAsync()
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

        public ICommand OpenArticleCommand => new Command<Article>(async (article) =>
        {
            if (article != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Views.ArticleDetailPage(article.Title, article.Link.ToString()));
            }
        });
    }
}
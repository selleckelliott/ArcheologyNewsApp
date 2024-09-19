using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ArcheologyNewsApp.Models
{
    public class ArticleScraper
    {
        private readonly HttpClient _httpClient;

        public ArticleScraper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            string mainPageUrl = "https://www.livescience.com/archaeology";
            var mainPageHtml = await _httpClient.GetStringAsync(mainPageUrl);
            var mainPageDocument = new HtmlDocument();
            mainPageDocument.LoadHtml(mainPageHtml);

            var articles = new List<Article>();

            //Extract article titles and links
            var articleNodes = mainPageDocument.DocumentNode.SelectNodes("//a[contains(@class, 'article-link')]");
            if (articleNodes != null)
            {
                foreach (var node in articleNodes)
                {
                    var title = node.InnerText.Trim();
                    var link = node.GetAttributeValue("href", string.Empty);

                    // Convert relative URL to absolute URL if necessary
                    if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
                    {
                        link = new Uri(new Uri(mainPageUrl), link).ToString();
                    }

                    // Create an Article object (we'll define this class)
                    articles.Add(new Article
                    {
                        Title = title,
                        Link = new Uri(link) // Use the correctly formed URL directly
                    });
                }
            }

            return articles;
        }
    }

    // Define an Article model
    public class Article
    {
        public string Title { get; set; }
        public Uri Link { get; set; }
    }
}

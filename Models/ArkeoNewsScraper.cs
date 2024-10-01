using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ArcheologyNewsApp.Models
{
    public class ArkeoNewsScraper
    {
        private readonly HttpClient _httpClient;

        public ArkeoNewsScraper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            string mainPageUrl = "https://arkeonews.net/";
            var mainPageHtml = await _httpClient.GetStringAsync(mainPageUrl);
            var mainPageDocument = new HtmlDocument();
            mainPageDocument.LoadHtml(mainPageHtml);

            var articles = new List<Article>();
            var articleNodes = mainPageDocument.DocumentNode.SelectNodes("//article[@class='post-item']//h2[@class='title']/a");

            if (articleNodes != null)
            {
                foreach (var node in articleNodes)
                {
                    var title = node.InnerText.Trim();
                    var link = node.GetAttributeValue("href", string.Empty);

                    if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
                    {
                        link = new Uri(new Uri(mainPageUrl), link).ToString();
                    }

                    articles.Add(new Article
                    {
                        Title = title,
                        Link = new Uri(link)
                    });
                }
            }

            return articles;
        }
    }
}
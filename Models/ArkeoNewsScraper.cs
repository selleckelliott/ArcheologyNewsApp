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
            // Set User-Agent and other headers to mimic a web browser
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            // Delay the request to mimic human browsing
            await Task.Delay(1000);  // Wait for 1 second before making the request

            string mainPageUrl = "https://arkeonews.net/";
            var mainPageHtml = await _httpClient.GetStringAsync(mainPageUrl);

            // Print the fetched HTML to the console for debugging
            Console.WriteLine(mainPageHtml);

            var mainPageDocument = new HtmlDocument();
            mainPageDocument.LoadHtml(mainPageHtml);

            var articles = new List<Article>();

            // Update XPath based on the provided HTML structure
            var articleNodes = mainPageDocument.DocumentNode.SelectNodes("//h2[@class='title-black']/a");
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

                    // Add the articles to the list
                    articles.Add(new Article
                    {
                        Title = title,
                        Link = new Uri(link) // Ensure the link is a valid URI
                    });
                }
            }
            else
            {
                Console.WriteLine("No articles found in the Latest Posts section.");
            }

            return articles;
        }
    }
}

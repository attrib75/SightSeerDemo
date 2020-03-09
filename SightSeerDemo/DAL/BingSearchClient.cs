using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Microsoft.Azure.CognitiveServices.Search.WebSearch.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models;
using System.Collections.Generic;
using ImageObject = Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models.ImageObject;

namespace SightSeerDemo.DAL
{
    public class BingSearchClient
    {
        public async Task<SearchResponse> WebSearchResultTypesLookup(string subscriptionKey, string searchTerm)
        {
            var client = new WebSearchClient(new Microsoft.Azure.CognitiveServices.Search.WebSearch.ApiKeyServiceClientCredentials(subscriptionKey));
            client.Endpoint = "https://northcentralus.api.cognitive.microsoft.com/";
            try
            {
                var webData = await client.Web.SearchAsync(query: searchTerm, count: 5, safeSearch: "Strict");
                Console.WriteLine($"Searched for Query {searchTerm}");

                //WebPages
                if (webData?.WebPages?.Value?.Count > 0)
                {
                    // find the first web page
                    var firstWebPagesResult = webData.WebPages.Value.FirstOrDefault();

                    if (firstWebPagesResult != null)
                    {
                        Console.WriteLine("Webpage Results#{0}", webData.WebPages.Value.Count);
                        Console.WriteLine("First web page name: {0} ", firstWebPagesResult.Name);
                        Console.WriteLine("First web page URL: {0} ", firstWebPagesResult.Url);
                    }
                    else
                    {
                        Console.WriteLine("Couldn't find web results!");
                    }
                }
                else
                {
                    Console.WriteLine("Didn't see any Web data..");
                }

                //Images
                if (webData?.Images?.Value?.Count > 0)
                {
                    // find the first image result
                    var firstImageResult = webData.Images.Value.FirstOrDefault();

                    if (firstImageResult != null)
                    {
                        Console.WriteLine("Image Results#{0}", webData.Images.Value.Count);
                        Console.WriteLine("First Image result name: {0} ", firstImageResult.Name);
                        Console.WriteLine("First Image result URL: {0} ", firstImageResult.ContentUrl);
                    }
                    else
                    {
                        Console.WriteLine("Couldn't find first image results!");
                    }
                }
                else
                {
                    Console.WriteLine("Didn't see any image data..");
                }

                //News
                if (webData?.News?.Value?.Count > 0)
                {
                    // find the first news result
                    var firstNewsResult = webData.News.Value.FirstOrDefault();

                    if (firstNewsResult != null)
                    {
                        Console.WriteLine("News Results#{0}", webData.News.Value.Count);
                        Console.WriteLine("First news result name: {0} ", firstNewsResult.Name);
                        Console.WriteLine("First news result URL: {0} ", firstNewsResult.Url);
                    }
                    else
                    {
                        Console.WriteLine("Couldn't find any News results!");
                    }
                }
                else
                {
                    Console.WriteLine("Didn't see first news data..");
                }

                //Videos
                if (webData?.Videos?.Value?.Count > 0)
                {
                    // find the first video result
                    var firstVideoResult = webData.Videos.Value.FirstOrDefault();

                    if (firstVideoResult != null)
                    {
                        Console.WriteLine("Video Results#{0}", webData.Videos.Value.Count);
                        Console.WriteLine("First Video result name: {0} ", firstVideoResult.Name);
                        Console.WriteLine("First Video result URL: {0} ", firstVideoResult.ContentUrl);
                    }
                    else
                    {
                        Console.WriteLine("Couldn't find first video results!");
                    }
                }
                else
                {
                    Console.WriteLine("Didn't see any video data..");
                }
                return webData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encountered exception. " + ex.Message);
                return null;
            }
        }

        public Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models.Images ImageSearch(string subscriptionKey, string searchTerm)
        {
            var client = new ImageSearchClient(new Microsoft.Azure.CognitiveServices.Search.ImageSearch.ApiKeyServiceClientCredentials(subscriptionKey));
            client.Endpoint = "https://northcentralus.api.cognitive.microsoft.com/";
            try
            {
                var imageResults =
                    client.Images.SearchAsync(query: searchTerm, count: 5,
                    safeSearch: "Strict", aspect: "Square", width: 220, imageType: "Photo").Result;
                Console.WriteLine($"Search images for query {searchTerm}");

                if (imageResults == null)
                {
                    Console.WriteLine("No image result data.");
                }

                // Image results
                if (imageResults.Value.Count > 0)
                {
                    var firstImageResult = imageResults.Value.First();

                    Console.WriteLine($"Image result count: {imageResults.Value.Count}");
                    Console.WriteLine($"First image insights token: {firstImageResult.ImageInsightsToken}");
                    Console.WriteLine($"First image thumbnail url: {firstImageResult.ThumbnailUrl}");
                    Console.WriteLine($"First image content url: {firstImageResult.ContentUrl}");
                }
                else
                {
                    Console.WriteLine("Couldn't find image results!");
                }

                Console.WriteLine($"Image result total estimated matches: {imageResults.TotalEstimatedMatches}");
                Console.WriteLine($"Image result next offset: {imageResults.NextOffset}");

                // Pivot suggestions
                if (imageResults.PivotSuggestions.Count > 0)
                {
                    var firstPivot = imageResults.PivotSuggestions.First();

                    Console.WriteLine($"Pivot suggestion count: {imageResults.PivotSuggestions.Count}");
                    Console.WriteLine($"First pivot: {firstPivot.Pivot}");

                    if (firstPivot.Suggestions.Count > 0)
                    {
                        var firstSuggestion = firstPivot.Suggestions.First();

                        Console.WriteLine($"Suggestion count: {firstPivot.Suggestions.Count}");
                        Console.WriteLine($"First suggestion text: {firstSuggestion.Text}");
                        Console.WriteLine($"First suggestion web search url: {firstSuggestion.WebSearchUrl}");
                    }
                    else
                    {
                        Console.WriteLine("Couldn't find suggestions!");
                    }
                }
                else
                {
                    Console.WriteLine("Couldn't find pivot suggestions!");
                }

                // Query expansions
                if (imageResults.QueryExpansions.Count > 0)
                {
                    var firstQueryExpansion = imageResults.QueryExpansions.First();

                    Console.WriteLine($"Query expansion count: {imageResults.QueryExpansions.Count}");
                    Console.WriteLine($"First query expansion text: {firstQueryExpansion.Text}");
                    Console.WriteLine($"First query expansion search link: {firstQueryExpansion.SearchLink}");
                }
                else
                {
                    Console.WriteLine("Couldn't find query expansions!");
                }
                return imageResults;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encountered exception. " + ex.Message);
                return null;
            }
        }
    }
}
This is an ASP.NET Core web app demonstrating the use of the <a href="https://docs.microsoft.com/en-us/azure/cognitive-services/bing-web-search/" target="_blank" rel="noopener"> Microsoft Cognitive Services API offerings</a>, as well as <a href="https://azure.microsoft.com/en-us/services/azure-maps" >Azure Maps</a>.  For a more in-depth look, there is a <a href="http://patrickgoode.com/wp-admin/post.php?post=661&action=edit">blog post here</a>.

The demo concept is that of a sight seeing tour of Chicago, so I’m calling it SightSeer.  In our demo, we make our way along a set of predefined popular Chicago tourist spots.  Users click the Next button to proceed to the next tour stop on the tour.  As we proceed,  the app displays the stop name and address, as well as web and image search results for the stop as queried from the Bing Web and Image Search APIs.  Each tour stop is displayed on an Azure Maps map.  With each stop, we show a popup for that stop on the map as well.  

The code hosted  <a href="https://sightseerdemo.azurewebsites.net/" target="_blank" rel="noopener">here</a> with fake data so you can see a live demo.  Feel free to clone the repo and tke it for a spin, put your own twist on it.

<img src="https://i1.wp.com/patrickgoode.com/wp-content/uploads/2020/03/SightSeerDemoImage.png">

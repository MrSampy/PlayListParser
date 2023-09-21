using PlayListParser;

public class Program
{
    private static void Main(string[] args)
    {

        string url_1 = "https://music.amazon.com/playlists/B08BWK8W15";
        string url_2= "https://music.amazon.com/albums/B001230JXC";

        PlayListScraper scraper = new PlayListScraper();

        var playlist = scraper.ScrapPlayListFromUrl(url_1);
    }
}
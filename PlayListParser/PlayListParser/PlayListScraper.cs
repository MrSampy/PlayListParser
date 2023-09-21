using HtmlAgilityPack;
using OpenQA.Selenium;
using PlayListParser.Models;
using PlayListParser.NodeUtils;

namespace PlayListParser
{
    public class PlayListScraper
    {
        public PlayList ScrapPlayListFromUrl(string url) 
        {
            var playList = new PlayList();

            using (DriverHandler driverHandler = new())
            {

                driverHandler.NavigateTo(url);

                HtmlNode detailHeaderContainer = driverHandler.HtmlDocument.DocumentNode.SelectSingleNode(NodeDetails.DetailHeaderContainerXpath);
                if (detailHeaderContainer != null)
                {
                    playList.Title = NodeParser.GetAttributeValue(detailHeaderContainer, "headline");
                    playList.Description = (NodeParser.GetAttributeValue(detailHeaderContainer, "primary-text") + " "
                        + NodeParser.GetAttributeValue(detailHeaderContainer, "secondary-text") + " "
                        + NodeParser.GetAttributeValue(detailHeaderContainer, "tertiary-text")).Trim();
                    playList.ImageUrl = NodeParser.GetAttributeValue(detailHeaderContainer, "image-src");


                    foreach (var songXPath in NodeDetails.GetSongXPathes())
                    {
                        if (driverHandler.HtmlDocument.DocumentNode.SelectSingleNode(songXPath) != null)
                        {
                            IWebElement? lastElement = null;
                            IWebElement? songElement;
                            do
                            {
                                var songs = driverHandler.HtmlDocument.DocumentNode.SelectNodes(songXPath);

                                if (songs == null || songs.Count == 0)
                                {
                                    break;
                                }

                                int songId = 1;
                                string songXpath = string.Empty;
                                for (; songId <= songs.Count; ++songId)
                                {
                                    songXpath = NodeDetails.GetXPathWithId(songXPath, songId);

                                    HtmlNode songNode = driverHandler.HtmlDocument.DocumentNode.SelectSingleNode(songXpath);

                                    if (songNode != null)
                                    {
                                        var title = NodeParser.GetAttributeValueFromHtmlNode("title", GetSongAttributeXpathByIdAndAttribute(songId, songXPath, NodeDetails.SongTitleXpath), driverHandler.HtmlDocument.DocumentNode);
                                        var artiste = NodeParser.GetAttributeValueFromHtmlNode("title", GetSongAttributeXpathByIdAndAttribute(songId, songXPath, NodeDetails.SongArtisteXpath), driverHandler.HtmlDocument.DocumentNode);
                                        artiste = string.IsNullOrEmpty(artiste) ? NodeParser.GetAttributeValue(detailHeaderContainer, "primary-text") : artiste;
                                        var duration = NodeParser.GetAttributeValueFromHtmlNode("title", GetSongAttributeXpathByIdAndAttribute(songId, songXPath, NodeDetails.SongDurationXpath), driverHandler.HtmlDocument.DocumentNode);
                                        Song songModel = new()
                                        {
                                            Title = title,
                                            Artist = artiste,
                                            Duration = duration
                                        };
                                        if (!playList.Songs.Any(x => x.Equals(songModel)))
                                        {
                                            playList.Songs.Add(songModel);
                                        }
                                    }
                                }

                                songElement = driverHandler.ChromeDriver.FindElements(By.XPath(songXpath)).FirstOrDefault();

                                if (songElement != null && songElement.Equals(lastElement))
                                {
                                    break;
                                }
                                else
                                {
                                    lastElement = songElement;
                                    driverHandler.ChromeDriver.ExecuteScript(NodeDetails.ScriptToScrollToElement, songElement);
                                }

                            } while (true);
                        }
                    }
                }

            }
            return playList;
        }

        public string GetSongAttributeXpathByIdAndAttribute(int id, string songXPath, string attributeXPath)
        {
            return string.Format("{0}[{1}]{2}", songXPath, id, attributeXPath);
        }
    }
}

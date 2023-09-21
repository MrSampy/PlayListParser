using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListParser.NodeUtils
{
    public class NodeDetails
    {
        #region PlayList
        public readonly static string PlayListBaseXpath = "//div[@id='root']/..//music-container[@id='Web.TemplatesInterface.v1_0.Touch.DetailTemplateInterface.DetailTemplate_1']/music-container[@class='hydrated']/div[1]/div[1]/div[2]/div[1]/div[1]/";
        public readonly static string DetailHeaderContainerXpath = "//div[@id='container']/..//music-detail-header[@class='hydrated']";
        public readonly static string SongTitleXpath = "/div[@class='content']/div[1]/music-link";
        public readonly static string SongArtisteXpath = "/div[@class='content']/div[2]/music-link";
        public readonly static string SongDurationXpath = "/div[@class='content']/div[4]/music-link";
        #endregion
        #region Scripts
        public readonly static string ScriptToScrollToElement = "arguments[0].scrollIntoView(true);";
        #endregion
        public static string GetXPathWithId(string xPath, int id) 
        {
            return string.Format("{0}[{1}]", xPath,id);
        }

        public static string[] GetSongXPathes() 
        {
            return new string[]
            {
                string.Format("{0}{1}", PlayListBaseXpath, "music-image-row"),
                string.Format("{0}{1}", PlayListBaseXpath, "music-text-row")
            };
        }

    }
}

using HtmlAgilityPack;
using PlayListParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListParser.NodeUtils
{
    public class NodeParser
    {
        public static string GetAttributeValue(HtmlNode node, string attributeName) 
        {
            string result;
            try
            {                 
                result = node.Attributes[attributeName].Value;                     
            }
            catch
            {
                return string.Empty;
            }
            return result;
        }
        public static string GetAttributeValueFromHtmlNode(string attributeName, string xpath, HtmlNode documnetNode)
        {
            var node = documnetNode.SelectSingleNode(xpath);
            return GetAttributeValue(node, attributeName);
        }
    }
}

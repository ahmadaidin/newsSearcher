using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using NewsSearcher.Models;
using System.Xml;
using NReadability;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace NewsSearcher.App
{
    public class Parser
    {

        public static string GetPageContent(string sURL)
        {
            string Content = "";
            try
            {
                WebRequest wrGETURL = WebRequest.Create(sURL);

                /*WebProxy myProxy = new WebProxy("cache.itb.ac.id", 8080);
                myProxy.Credentials = new NetworkCredential("ahmad.aidin", "aidin082939");
                myProxy.BypassProxyOnLocal = true;

                wrGETURL.Proxy = myProxy;*/

                Stream objStream = wrGETURL.GetResponse().GetResponseStream();

                StreamReader objReader = new StreamReader(objStream);
                Content = objReader.ReadToEnd();
                objReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Content;
        }


        public static List<RSSItem> ParseRSS(string url)
        {
            List<RSSItem> RSSItems = new List<RSSItem>();
            try
            {
                string RSSXML = GetPageContent(url);
                XmlDocument rss = new XmlDocument();
                rss.LoadXml(RSSXML);
 
                XmlNodeList nodes = rss.DocumentElement.SelectNodes("/rss/channel/item");
                foreach (XmlNode node in nodes)
                {
                    string title = node.SelectSingleNode("title").InnerText;
                    string link = node.SelectSingleNode("link").InnerText;
                    string pubDate = node.SelectSingleNode("pubDate").InnerText;

                    RSSItem item = new RSSItem
                    {
                        Title = title,
                        Link = link,
                        PubDate = pubDate
                    };

                    RSSItems.Add(item);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RSSItems;
        }

        

        public static string[] ParseNewsHMTL(string url)
        {
            var transcoder = new NReadabilityWebTranscoder();
            List<string> result = new List<string>(); 
            try
            {
                WebTranscodingInput input = new WebTranscodingInput(url);
                WebTranscodingResult transcodeContent = transcoder.Transcode(input);
                string HTML = transcodeContent.ExtractedContent;
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(HTML);
                foreach (HtmlNode paragraph in document.DocumentNode.SelectNodes("//p"))
                {
                    // do something with the paragraph node here
                    result.Add(paragraph.InnerText.ToLower()); // or something similar
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result.ToArray();
        }
    }
}
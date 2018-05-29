using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

using NewsSearcher.Models;
using System.Xml;

namespace NewsSearcher.App
{
    public class Searcher
    {
        private string[] RSS;
        private const int size = 2;
        private List<RSSItem>[] RSSItemCollections;
        public Params Params { set; get; }

        public Searcher(Params param)
        {
            RSS = new string[size];
            RSS[0] = "http://rss.detik.com/index.php/detikcom";
            RSS[1] = "http://tempo.co/rss/terkini";
           //RSS[2] = "http://www.antaranews.com/rss/terkini";
            //RSS[3] = "http://rss.vivanews.com/get/all";
           

            Params = new Params
            {
                Keywords = param.Keywords,
                Algorithm = param.Algorithm
            };
            SetRSSItems();
        }

        private void SetRSSItems()
        {
            RSSItemCollections = new List<RSSItem>[size];
            for (int i = 0; i<RSS.Length; i++)
            {
                string sURL = RSS[i];
                List<RSSItem> RSSItems = Parser.ParseRSS(sURL);
                RSSItemCollections[i]=RSSItems;
            }
        }


        public News[] Search()
        {
            List<News> NewsResult = new List<News>();
            if (Params.Algorithm.Equals("KMP"))
            {
                for (int i = 0; i < RSSItemCollections.Length; i++)
                {
                    List<RSSItem> RSSItems = RSSItemCollections[i];
                    try
                    {
                        foreach (RSSItem rss in RSSItems)
                        {
                            string[] content = new string[1];
                            content[0] = rss.Title;
                            KMP searcher = new KMP(Params.Keywords, rss, content);
                            News result = searcher.GetMatchSentence();

                            if (result == null)
                            {
                                searcher.Contents = Parser.ParseNewsHMTL(rss.Link);
                                result = searcher.GetMatchSentence();
                            }

                            if (result != null)
                            {
                                NewsResult.Add(result);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            else if (Params.Algorithm.Equals("BM"))
            {
                for (int i = 0; i < RSSItemCollections.Length; i++)
                {
                    List<RSSItem> RSSItems = RSSItemCollections[i];
                    try
                    {
                        foreach (RSSItem rss in RSSItems)
                        {
                            string[] content = new string[1];
                            content[0] = rss.Title;
                            BM searcher = new BM(Params.Keywords, rss, content);
                            News result = searcher.GetMatchSentence();

                            if (result == null)
                            {
                                searcher.Contents = Parser.ParseNewsHMTL(rss.Link);
                                result = searcher.GetMatchSentence();
                            }

                            if (result != null)
                            {
                                NewsResult.Add(result);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            else if (Params.Algorithm.Equals("REGEX"))
            {
                for (int i = 0; i < RSSItemCollections.Length; i++)
                {
                    List<RSSItem> RSSItems = RSSItemCollections[i];
                    try
                    {
                        foreach (RSSItem rss in RSSItems)
                        {
                            string[] content = new string[1];
                            content[0] = rss.Title;
                            Regex searcher = new Regex(Params.Keywords, rss, content);
                            News result = searcher.GetMatchSentence();

                            if (result == null)
                            {
                                searcher.Contents = Parser.ParseNewsHMTL(rss.Link);
                                result = searcher.GetMatchSentence();
                            }

                            if (result != null)
                            {
                                NewsResult.Add(result);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return NewsResult.ToArray();
        }
    }
}
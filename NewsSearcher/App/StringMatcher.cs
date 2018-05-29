using NewsSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsSearcher.App
{
    public class StringMatcher
    {
        public string Keywords { set; get; }
        public RSSItem Information { set; get; }
        public string[] Contents { set; get; }
        
        public StringMatcher()
        {

        }

        public StringMatcher(string keywords, RSSItem info, string[] contents)
        {
            Keywords = keywords.ToLower();
            Information = info;
            Contents = contents;
        }

        public StringMatcher(StringMatcher strPair)
        {

            Keywords = strPair.Keywords;
            Information = strPair.Information;
            Contents = strPair.Contents;
        }

        public News GetMatchSentence()
        {
            bool found = false;
            int i = 0;

            while (!found && i < Contents.Length)
            {
                Contents[i].ToLower();
                if (Match(Contents[i]) != -1)
                {
                    News news = new News
                    {
                        Information = this.Information,
                        Content = this.Contents[i]
                    };

                    return news;
                }
                else
                    i++;
            }
            return null;
        }

        public virtual int Match(string text)
        {
            return -1;
        }
    }
}
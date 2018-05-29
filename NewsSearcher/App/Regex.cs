using NewsSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsSearcher.App
{
    public class Regex : StringMatcher
    {
        public Regex(StringMatcher strPair) : base(strPair)
        {

        }

        public Regex(string keywords, RSSItem info, string[] contents) : base(keywords, info, contents)
        {

        }

        public override int Match(string text)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(text, Keywords)){
                return 1;
            } else
                return -1;
        }
    }
}
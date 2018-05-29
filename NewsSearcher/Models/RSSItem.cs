using System;
using System.IO;
using System.Xml.Serialization;

namespace NewsSearcher.Models
{
    public class RSSItem
    {
        public string Title { set; get; }

        public string Link { set; get; }

        public string PubDate { set; get; }

    }
}

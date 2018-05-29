using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsSearcher.Models
{
    public class News
    {
        public RSSItem Information { get; set; }
        public string Content { get; set; }
    }
}
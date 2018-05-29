using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsSearcher.Models;
using NewsSearcher.App;

namespace NewsSearcher.Controllers
{
    public class NewsController : ApiController
    {
        // POST api/values
        public News[] Post(Params param)
        {
            Searcher newsSearcher = new Searcher(param);
            return newsSearcher.Search();
        }
    }
}

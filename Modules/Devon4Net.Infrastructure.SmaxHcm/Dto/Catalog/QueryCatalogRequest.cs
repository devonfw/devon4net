using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Catalog
{
    public class QueryCatalogRequest
    {
        public int returnNum { get; set; }
        public string searchQuery { get; set; }

        public string categoryId { get; set; }
        public int offset { get; set; }
        public bool includeArticles { get; set; }
        public bool includeOfferings { get; set; }

        public QueryCatalogRequest()
        {
            returnNum = 100;
            offset = 0;
        }

    }
}

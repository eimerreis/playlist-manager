using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class CosmosDbConfiguration
    {
        public string Endpoint { get; set; }
        public string DatabaseName { get; set; }
        public string AccountKey { get; set; }
    }
}

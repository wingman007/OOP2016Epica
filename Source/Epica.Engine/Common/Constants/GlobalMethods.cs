using System.Collections.Generic;
using System.Linq;
using Epica.Data.Models;
using Epica.Data.Repositories;

namespace Epica.Engine.Common.Constants
{
    public class GlobalMethods
    {
        static IRepository<Market> market = new EfRepository<Market>();
        public static ICollection<Item> GetMarketItems()
        {
            List<Market> globalMarket = market.All().ToList();
            return globalMarket[0].Item;
        }
    }
}

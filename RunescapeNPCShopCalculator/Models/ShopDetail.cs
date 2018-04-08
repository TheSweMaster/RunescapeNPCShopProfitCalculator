using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunescapeNPCShopCalculator.Models
{
    public class ShopDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Members { get; set; }
        public string Shopkeeper { get; set; }
        public string Notes { get; set; }
        public List<ShopItem> ShopItems { get; set; }

    }
}

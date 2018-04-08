using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunescapeNPCShopCalculator.Models;
using RunescapeNPCShopCalculator.StoreData;

namespace RunescapeNPCShopCalculator.Controllers
{
    public class ShopProfitCalculatorController : Controller
    {
        private HttpClient _client = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BestItems()
        {
            var url = "https://rsbuddy.com/exchange/summary.json";

            var responds = await _client.GetStringAsync(url);

            var result = RsExchangeData.FromJson(responds);


            //var url2 = "https://rsbuddy.com/exchange/names.json";

            //var responds2 = await _client.GetStringAsync(url2);

            string path = @"./Data/RSStorePriceDetials.json";

            //var data = JsonConvert.DeserializeObject<StorePriceData>(System.IO.File.ReadAllText(path));

            var data = System.IO.File.ReadAllText(path);

            var result2 = StorePriceData.FromJson(data);

            return View();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using RunescapeNPCShopCalculator.Data;
using RunescapeNPCShopCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace RunescapeNPCShopCalculator.Controllers
{
    public class DataAccess : Controller
    {
        private readonly AppDbContext _context;
        private HttpClient _client = new HttpClient();

        public DataAccess(AppDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllData()
        {
            //Url id 1-298 (292 items)
            for (int id = 1; id <= 300; id++)
            {
                var url = "http://2007rshelp.com/shops.php?id=" + id;

                var web = new HtmlWeb();
                var doc = await web.LoadFromWebAsync(url);

                var htmlNodesContent = doc.DocumentNode.SelectNodes("//div[@id='content']/div[@class='boxbottom']//td");

                var htmlNodesItemData = htmlNodesContent.First().SelectNodes("//td[@class='tablebottom']");
                var htmlTableColum = htmlNodesContent.First().SelectNodes("//th[@class='tabletop']");

                if (htmlNodesItemData == null || htmlTableColum == null)
                {
                    continue;
                }

                //var htmltd = htmlNodesContent.First().SelectNodes("//td");
                //var nodes3 = htmlNodes.CssSelect("div#wrapper").ToList();

                var itemDataList = new List<string>();
                foreach (var node in htmlNodesItemData)
                {
                    itemDataList.Add(node.InnerText);
                }

                var shopItemList = new List<ShopItem>();
                if (htmlTableColum.Count == 3)
                {
                    for (int i = 0; i < itemDataList.Count; i += 3)
                    {
                        int price = 0;
                        string priceString = itemDataList[i + 1];
                        char[] trimList = { 'g', 'p', ' ', ',', '.' };
                        if (priceString.Contains("gp"))
                        {
                            var testPrice = priceString.Trim(trimList).Replace(",","");
                            price = Convert.ToInt32(testPrice);
                        }

                        var shopItem = new ShopItem()
                        {
                            Id = id * 100 + (i / 3),
                            Item = itemDataList[i],
                            DisplayPrice = itemDataList[i + 1],
                            Price = price,
                            DefaultStock = Convert.ToInt32(itemDataList[i + 2].Trim(' ')),
                        };
                        shopItemList.Add(shopItem);

                        if (!_context.ShopItems.Any(x => x.Id == id * 100 + (i / 3)))
                        {
                            _context.Add(shopItem);
                        }
                        else
                        {
                            _context.Update(shopItem);
                        }

                        _context.Database.OpenConnection();
                        try
                        {
                            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.ShopItems ON");
                            _context.SaveChanges();
                            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.ShopItems OFF");
                        }
                        finally
                        {
                            _context.Database.CloseConnection();
                        }
                    }
                }
                else if (htmlTableColum.Count == 2)
                {
                    for (int i = 0; i < itemDataList.Count; i += 2)
                    {
                        int price = 0;
                        string priceString = itemDataList[i + 1];
                        char[] trimList = { ',', 'g', 'p', ' ' };
                        if (priceString.Contains("gp"))
                        {
                            var testPrice = priceString.Trim(trimList).Replace(",", "");
                            price = Convert.ToInt32(testPrice);
                        }

                        var shopItem = new ShopItem()
                        {
                            Id = id * 100 + (i / 2),
                            Item = itemDataList[i],
                            DisplayPrice = itemDataList[i + 1],
                            Price = price,
                            DefaultStock = 0,
                        };
                        shopItemList.Add(shopItem);

                        if (!_context.ShopItems.Any(x => x.Id == id * 100 + (i / 2)))
                        {
                            _context.Add(shopItem);
                        }
                        else
                        {
                            _context.Update(shopItem);
                        }

                        _context.Database.OpenConnection();
                        try
                        {
                            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.ShopItems ON");
                            _context.SaveChanges();
                            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.ShopItems OFF");
                        }
                        finally
                        {
                            _context.Database.CloseConnection();
                        }
                    }
                }
                else
                {
                    continue;
                }

                var shopDetails = new ShopDetail()
                {
                    Id = id,
                    Name = htmlNodesContent[0].InnerText.Remove(htmlNodesContent[0].InnerText.Count() - 6),
                    Location = htmlNodesContent[3].InnerText,
                    Members = (htmlNodesContent[5].InnerText == "Yes") ? true : false,
                    Shopkeeper = htmlNodesContent[7].InnerText,
                    Notes = htmlNodesContent[9].InnerText,
                    ShopItems = shopItemList,
                };

                if (!_context.ShopDetails.Any(x => x.Id == id))
                {
                    _context.Add(shopDetails);
                }
                else
                {
                    _context.Update(shopDetails);
                }

                _context.Database.OpenConnection();
                try
                {
                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.ShopDetails ON");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.ShopDetails OFF");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }

            }

            return Ok("Data has been saved to the database!");
        }

        public IActionResult ClearDatabase()
        {
            var shopItems = _context.ShopItems.Select(x => x);
            _context.ShopItems.RemoveRange(shopItems);
            _context.SaveChanges();

            var shopDetials = _context.ShopDetails.Select(x => x);
            _context.ShopDetails.RemoveRange(shopDetials);
            _context.SaveChanges();

            return Ok("Database has been cleared!");
        }

    }
}
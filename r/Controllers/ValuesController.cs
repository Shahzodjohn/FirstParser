using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using r.Context;
using r.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace r.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbcontext _context;

        public ValuesController(AppDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task Index()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", " _ga=GA1.2.221135562.1624701188; e572d25e2f77b01121404f0eef127450=a4e3b2a720d2725035c4bb0e4ffb8b10; _gid=GA1.2.651348839.1626343046; _gat_gtag_UA_132313057_1=1; XSRF-TOKEN=eyJpdiI6IlRnRTZcL29hVnJRcU9iQ2d5ZEpwM0p3PT0iLCJ2YWx1ZSI6Im5VTGlLbHZzc1BXbmVqQUFLVk5nUmNhMmRxQVNoRGU2cUFkNE13Y25leFpvUW51SVhrb09mcTBIb0xkQmVOdFUiLCJtYWMiOiI4MzEyYjNlNmI2YzNjY2IyMmIwYTFiZGZjYzIxMWFkYzFkYTdhZTc1YWQ5YjAxZDUxYmU2NzBiNDU5NGM5YmNmIn0%3D; laravel_session=eyJpdiI6IjlFcWJZZTVOMVwvUDFaRmN3d1J6SGZ3PT0iLCJ2YWx1ZSI6IkI5dkwyWXF5YVwvNk54OW50SGJkOEtyV0RXMzAyR0ZaSUtjU2lkN3F1d3BndlptMGd0bDZjRko3ZndLK3E1cFVLIiwibWFjIjoiMTNlNzM2M2ZjMzcxNWIyOTdhZmI1NmViNDZjMjIyNDVmMjNmYzMwOWJkZjkzZDEyMzZiM2U4NWYzNGI3YjMzOSJ9");
            var content = client.GetAsync($"https://mojegs1.pl/moje-produkty/sortowanie/nazwa/kierunek/rosnaco/1?amountPerPage=1311&searchText=&isPublic=").Result;

            for (int i = 1; i < 10; i++)
            {
                content = client.GetAsync($"https://mojegs1.pl/moje-produkty/sortowanie/nazwa/kierunek/rosnaco/{i}?amountPerPage=2000&searchText=&isPublic=").Result;
                var result = content.Content.ReadAsStringAsync().Result;
                HtmlAgilityPack.HtmlDocument web = new HtmlAgilityPack.HtmlDocument();
                web.LoadHtml(result);

                var xpath = "//tr";

                var nocategory = _context.CategorySSS.FirstOrDefault(x => x.CategoryName == "BRAK");
                if (nocategory == null)
                {
                    var rt = new Categories()
                    {
                        CategoryName = "BRAK",
                    };

                    nocategory = _context.CategorySSS.Add(rt).Entity;
                    _context.SaveChanges();
                }
               
                var classicCategory = _context.CategorySSS.FirstOrDefault(x => x.CategoryName == "CLASSIC");
                if (classicCategory == null)
                {
                    var ry = new Categories()
                    {
                        CategoryName = "CLASSIC",
                    };

                    classicCategory = _context.CategorySSS.Add(ry).Entity;
                    _context.SaveChanges();
                }

                var fasterCategory = _context.CategorySSS.FirstOrDefault(x => x.CategoryName == "FASTER");

                if (fasterCategory == null)
                {
                    var ru = new Categories()
                    {
                        CategoryName = "FASTER",
                    };

                    fasterCategory = _context.CategorySSS.Add(ru).Entity;
                    _context.SaveChanges();
                }
                foreach (var item in web.DocumentNode.SelectNodes(xpath).ToList().Skip(1))
                {
                    Console.WriteLine($"{item.InnerText.Trim().Split("\n")},");
                    var items = item.ChildNodes;
                    var isClassicCategory = items[1].InnerText.Trim().Split(" ").ToList().Any(x => x.ToLower() == "classic");
                    var isFasterCategory = items[1].InnerText.Trim().Split(" ").ToList().Any(x => x.ToLower() == "faster");




                    var productName = isClassicCategory ? items[1].InnerText.Trim().Replace("CLASSIC", " ") :
                                      isFasterCategory ? items[1].InnerText.Trim().Replace("FASTER", " ") :
                                      items[1].InnerText.Trim();
                    //var contains = productName.Contains("rozm.");

                    #region Deviding by regex
                    //var data = Regex.sele(productName, @"[\0-9]").Value;


                    //if (productName.Contains($"rozm.") && productName.LastIndexOf(' ') != 0)
                    //{
                    //    await _context.DRTS.AddAsync(new DRT
                    //    {
                    //        Size = $"rozm. + {productName.Replace(productName.Substring(productName.LastIndexOf(' ')), "rozm.")}"
                    //    });
                    //}
                    //if (!productName.Contains($"rozm.") && productName.LastIndexOf(' ') != 0)
                    //{
                    //    await _context.DRTS.AddAsync(new DRT
                    //    {
                    //        Size = $"No Size"
                    //    });

                    //}


                    //productName = productName.Replace("rozm.", " ");
                    //var output = Regex.Replace(productName, @"[\0-9]", " ");
                    #endregion

                    // Regex.Replace(productName, "[^0-9]", "");
                    var ProdName = productName.Replace("rozm.", " ");
                    var output = Regex.Replace(ProdName, @"[\0-9]", " ");


                    var rr = (isClassicCategory ? classicCategory.Id :
                            (isFasterCategory ? fasterCategory.Id : nocategory.Id));


                    //   var value = productName.Contains($"rozm.") && productName.LastIndexOf(' ') !=null;

                    _context.DRTS.Add(new DRT
                    {
                        NAZWAPRODUKTU = output,
                        CategorySSSId = rr,
                        GTIN = items[3].InnerText.Trim(),
                        KLASYFIKACJAGPC = items[5].InnerText.Trim(), //productname.Substring(productname.LastIndexOf('.')+1, 2)
                        DATA = items[7].InnerText.Trim(),
                        Size = $"rozm. № {(productName.Contains(".") ? productName.Substring(productName.LastIndexOf('.')): "0" )}"
                    });
                }
                await _context.SaveChangesAsync();
            }


        }
    }
}

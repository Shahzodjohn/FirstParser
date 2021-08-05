using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using r.Context;
using r.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
            //client.DefaultRequestHeaders.Add("Cookie", "ElS4TqDdrI5g96loAR61laSN0llwrbQkvLAXuzba");

            var GetClient = new RestClient("https://mojegs1.pl/logowanie");
            GetClient.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "XSRF-TOKEN=eyJpdiI6IkZmdGk4SFJ0cUV6b08rZkxOa1ZwbWc9PSIsInZhbHVlIjoibU5yXC9ka2tka0xiVzVuWHdDVktPSDhoMUNhM2xHdG9HNWxrakZLOU5vQ3cwNklFOHZwT3k1Yk1XNmlZOUQwdysiLCJtYWMiOiJlZWM3YjY1NjQ1Yjg0NmVmYTkxNGFkYzdjNThhNmE4NmQ1YmMzMGJiMGY3ZGYzMjNjYmFhNjdkYTZkYzMxZmEwIn0%3D; e572d25e2f77b01121404f0eef127450=6c59d20d6edf5cab42e47435612d8098; laravel_session=eyJpdiI6Ik5CS0NMNzAweGY3aFdtWlRZbEFLOFE9PSIsInZhbHVlIjoiZFBvT0c4WUdSYTh1NUE5M3RUTjFCWDFpaDlkbDk3Q0tBZUViSnBEdFE1SVltMGh1ZDdHdVgyelBCa3ZKYkFpUiIsIm1hYyI6IjgxMjk2MzYwNjI2NGY2NDUxMDgwOGUxYWFkYTE2YTcyNzQ1Yjk1M2E3MDQ4NDQ3ZDhhYzY3MmIzZmE4MTlhNjYifQ%3D%3D");
            IRestResponse response = GetClient.Execute(request);
            

            var PostClient = new RestClient("https://mojegs1.pl/logowanie/zaloguj-uzytkownika");
            PostClient.Timeout = -1;
            var PostRequest = new RestRequest(Method.POST);
            PostRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            PostRequest.AddHeader("Cookie", "XSRF-TOKEN=eyJpdiI6ImdVZnBTa25xekswUlNsXC9FcTI5TmRBPT0iLCJ2YWx1ZSI6ImR5K1hQcWNwVk9vRlRnSDJkTW1US25MQWszVjNyY1R6SFwvUGJwOVFmVXZya090aDZsQ2I5QnhHRUFPTE9GdzI2IiwibWFjIjoiMTcxNzExMGI0MTI3NmU0NDliMGUwZTYwNDhjYTA3MDc5Zjk3Y2EzN2VjZWVjYzRhNWQyYTBkNDU3NjI2OTMzNyJ9; e572d25e2f77b01121404f0eef127450=6c59d20d6edf5cab42e47435612d8098; laravel_session=eyJpdiI6InFhZHF2OUN1Q3g3NmE5cWlNbVZ1K0E9PSIsInZhbHVlIjoiTjFLYldvVExzcXlqbWxYRFNUaERZc0o1R280cGhSQ2ROWVpjZlRNOEcyVjdxMGY4Z1JvdnREQmhjR3N0VHRjQSIsIm1hYyI6ImEzMjUwMGE4ZTRmNTFiN2ExYzA2YTY4ZGYxOTQwYzdjN2Y0ZDAxNmMyOWQ5NjczNmY2ZjQ4OGY2OWQzZDVhZjMifQ%3D%3D");
            PostRequest.AddParameter("email", "tomek@fiorino.eu");
            PostRequest.AddParameter("password", "Epoka1-wsx");
            IRestResponse PostResponse = PostClient.Execute(PostRequest);



            var RstClient = new RestClient("https://mojegs1.pl/moje-produkty/sortowanie/nazwa/kierunek/rosnaco/1?searchText=&isPublic=&amountPerPage=1000");
            RstClient.Timeout = -1;
            var RestRequest = new RestRequest(Method.GET);
            RestRequest.AddHeader("Cookie", "XSRF-TOKEN=eyJpdiI6ImdVZnBTa25xekswUlNsXC9FcTI5TmRBPT0iLCJ2YWx1ZSI6ImR5K1hQcWNwVk9vRlRnSDJkTW1US25MQWszVjNyY1R6SFwvUGJwOVFmVXZya090aDZsQ2I5QnhHRUFPTE9GdzI2IiwibWFjIjoiMTcxNzExMGI0MTI3NmU0NDliMGUwZTYwNDhjYTA3MDc5Zjk3Y2EzN2VjZWVjYzRhNWQyYTBkNDU3NjI2OTMzNyJ9; e572d25e2f77b01121404f0eef127450=6c59d20d6edf5cab42e47435612d8098; laravel_session=eyJpdiI6InFhZHF2OUN1Q3g3NmE5cWlNbVZ1K0E9PSIsInZhbHVlIjoiTjFLYldvVExzcXlqbWxYRFNUaERZc0o1R280cGhSQ2ROWVpjZlRNOEcyVjdxMGY4Z1JvdnREQmhjR3N0VHRjQSIsIm1hYyI6ImEzMjUwMGE4ZTRmNTFiN2ExYzA2YTY4ZGYxOTQwYzdjN2Y0ZDAxNmMyOWQ5NjczNmY2ZjQ4OGY2OWQzZDVhZjMifQ%3D%3D");
            IRestResponse RstResponse = RstClient.Execute(RestRequest);


            var linkCount = 1;

            for (int i = 0; ;i++)
            {
                var LinkClient = new RestClient($"https://mojegs1.pl/moje-produkty/{linkCount}");
                LinkClient.Timeout = -1;
                var LinkRequest = new RestRequest(Method.GET);
                LinkRequest.AddHeader("Cookie", "XSRF-TOKEN=eyJpdiI6ImdVZnBTa25xekswUlNsXC9FcTI5TmRBPT0iLCJ2YWx1ZSI6ImR5K1hQcWNwVk9vRlRnSDJkTW1US25MQWszVjNyY1R6SFwvUGJwOVFmVXZya090aDZsQ2I5QnhHRUFPTE9GdzI2IiwibWFjIjoiMTcxNzExMGI0MTI3NmU0NDliMGUwZTYwNDhjYTA3MDc5Zjk3Y2EzN2VjZWVjYzRhNWQyYTBkNDU3NjI2OTMzNyJ9; e572d25e2f77b01121404f0eef127450=6c59d20d6edf5cab42e47435612d8098; laravel_session=eyJpdiI6InFhZHF2OUN1Q3g3NmE5cWlNbVZ1K0E9PSIsInZhbHVlIjoiTjFLYldvVExzcXlqbWxYRFNUaERZc0o1R280cGhSQ2ROWVpjZlRNOEcyVjdxMGY4Z1JvdnREQmhjR3N0VHRjQSIsIm1hYyI6ImEzMjUwMGE4ZTRmNTFiN2ExYzA2YTY4ZGYxOTQwYzdjN2Y0ZDAxNmMyOWQ5NjczNmY2ZjQ4OGY2OWQzZDVhZjMifQ%3D%3D");
                IRestResponse LinkResponse = LinkClient.Execute(LinkRequest);
                linkCount++;
                
                
                HtmlAgilityPack.HtmlDocument web = new HtmlAgilityPack.HtmlDocument();
                web.LoadHtml(LinkResponse.Content);

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

                try
                {
                    foreach (var item in web.DocumentNode.SelectNodes(xpath).ToList().Skip(1))
                    {
                        Console.WriteLine($"{item.InnerText.Trim().Split("\n")},");
                        var items = item.ChildNodes;
                        var gtin = _context.DRTS.FirstOrDefault(x => x.GTIN == items[3].InnerText.Trim());
                        

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

                        var gting = new List<string>();
                        gting.Add(items[3].InnerText.Trim());
                        foreach (var gtinImages in gting)
                        {
                            using (HttpClient cl = new HttpClient())
                            {
                                cl.DefaultRequestHeaders.Add("Cookie", "XSRF-TOKEN=eyJpdiI6ImdVZnBTa25xekswUlNsXC9FcTI5TmRBPT0iLCJ2YWx1ZSI6ImR5K1hQcWNwVk9vRlRnSDJkTW1US25MQWszVjNyY1R6SFwvUGJwOVFmVXZya090aDZsQ2I5QnhHRUFPTE9GdzI2IiwibWFjIjoiMTcxNzExMGI0MTI3NmU0NDliMGUwZTYwNDhjYTA3MDc5Zjk3Y2EzN2VjZWVjYzRhNWQyYTBkNDU3NjI2OTMzNyJ9; e572d25e2f77b01121404f0eef127450=6c59d20d6edf5cab42e47435612d8098; laravel_session=eyJpdiI6InFhZHF2OUN1Q3g3NmE5cWlNbVZ1K0E9PSIsInZhbHVlIjoiTjFLYldvVExzcXlqbWxYRFNUaERZc0o1R280cGhSQ2ROWVpjZlRNOEcyVjdxMGY4Z1JvdnREQmhjR3N0VHRjQSIsIm1hYyI6ImEzMjUwMGE4ZTRmNTFiN2ExYzA2YTY4ZGYxOTQwYzdjN2Y0ZDAxNmMyOWQ5NjczNmY2ZjQ4OGY2OWQzZDVhZjMifQ%3D%3D"
                                );
                                string url = ($"https://mojegs1.pl/moje-produkty/pobierz-etykiete?gtin={gtinImages}&label_text=ekoTuptusie+APLIKACJA+PAN+SAMOCHODZIK+CLASSIC+rozm.+32&size=1&extension=png");
                                using (HttpResponseMessage MessageResponse = await cl.GetAsync(url))
                                using (Stream streamToReadFrom = await MessageResponse.Content.ReadAsStreamAsync())
                                {
                                    string fileToWriteTo = Path.GetFullPath($"PdfCodes/{gtinImages}.png");
                                    using (Stream streamToWriteTo = System.IO.File.Open(fileToWriteTo, FileMode.Create))
                                    {
                                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                                    }
                                }
                            }
                        }
                        _context.DRTS.Add(new DRT
                        {
                            NAZWAPRODUKTU = output,
                            CategorySSSId = rr,
                            GTIN = items[3].InnerText.Trim(),
                            KLASYFIKACJAGPC = items[5].InnerText.Trim(), //productname.Substring(productname.LastIndexOf('.')+1, 2)
                            DATA = items[7].InnerText.Trim(),
                            Size = $"rozm. № {(productName.Contains(".") ? productName.Substring(productName.LastIndexOf('.')) : "0")}"
                        });
                    }
                }
                catch (Exception)
                {

                    break;
                }
                await _context.SaveChangesAsync();
            }

        }
    }
}

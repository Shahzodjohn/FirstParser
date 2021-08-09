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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

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
            HtmlAgilityPack.HtmlDocument web = new HtmlAgilityPack.HtmlDocument();
            //var GetClient = new RestClient("https://mojegs1.pl/logowanie");
            //GetClient.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Cookie", "XSRF-TOKEN=eyJpdiI6IlNpQlhpaEl6MlFHMTF1YkZET1hWS3c9PSIsInZhbHVlIjoiektJalBWK2VlZHo2UVJlcm1MaDRhNjJwVGhVUFVmOGM4MDI0eU5YRGVqV2dwUjJDZHNTSWpURERBR0FPVXF4ViIsIm1hYyI6IjI4YWY2NWRlMTRhOGZmZGY5NTU1ZGViYTJmOThmNzQ5YjNkMzI0Y2M0ZDNlYjdjYTI2ZjQ2Yzk1MGM2YWNiMzgifQ%3D%3D; e572d25e2f77b01121404f0eef127450=7af44baa71dff9a37418e0d617d510fa; laravel_session=eyJpdiI6Imo0aWxNRFI1aktBenBneDdtUE5JQnc9PSIsInZhbHVlIjoiRHp5MmY1cEpHRHczU21jXC9KanZQMnkyUnVUa2RhNUNnTmJjS1J2cDRpVVpnbU1zQUxzVWNcLzN0UElBXC9uWThkaSIsIm1hYyI6IjcyMTgxMTIyZTU3MGEyMDY3MTc2Y2U4OWMyM2NkYWYyMDQ1OWMyNGFjYjY4MDkwM2NmNzY5NGE0ZTZmZDY1ODUifQ%3D%3D");
            //IRestResponse response = GetClient.Execute(request);
            //web.LoadHtml(response.Content);

            var PostClient = new RestClient("https://mojegs1.pl/logowanie/zaloguj-uzytkownika");
            PostClient.Timeout = -1;
            var PostRequest = new RestRequest(Method.POST);
            PostRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            PostRequest.AddHeader("Cookie", "XSRF-TOKEN=eyJpdiI6IjNtbUp4OE51aXU2Yjk1Rk14MkNSMGc9PSIsInZhbHVlIjoiMm9tVU96ZlBLOWZXcEpKY040UXhid1ZHRzBTeVBMNHZYVUpoaCt5MjE5alVDOVVBSWphK0RoN1BHMFNaRmRQciIsIm1hYyI6IjZiNDFkYzEzZDIwNDE5NzhjZmJhNmQ0MGU2YmZkODQzZDJlODZiY2QzNjZmZTY1YjI3Mjg1Nzg3N2Y4YjVkNGEifQ%3D%3D; e572d25e2f77b01121404f0eef127450=7af44baa71dff9a37418e0d617d510fa; laravel_session=eyJpdiI6ImtXdVhmekJnZVZyNXVKeTZvc3VzeWc9PSIsInZhbHVlIjoiQXNES1RXS216Nmw1S2R4OGUxaTBEcXYzQ3VhcCtWcFd4ZVlhNUlHMERzRlJscWNQaGdKTXByTWw4eTZIM01rcCIsIm1hYyI6IjFhYzMyYjQ0NDNkOTFjN2Q2ZDI4NDUzYzQyZmNkYTAzY2JjODQxYWEwN2M0NWY4ZTE2OTFhZmEwYmFmNWM1NjEifQ%3D%3D");
            PostRequest.AddParameter("email", "tomek@fiorino.eu");
            PostRequest.AddParameter("password", "Epoka1-wsx");
            IRestResponse PostResponse = PostClient.Execute(PostRequest);
            web.LoadHtml(PostResponse.Content);

            var value = "";
            var cookieFirst = PostResponse.Cookies[0].Value;
            var cookieSecond = PostResponse.Cookies[1].Value;
            //var count2 = count.
            



            var RstClient = new RestClient("https://mojegs1.pl/moje-produkty/sortowanie/nazwa/kierunek/rosnaco/1?searchText=&isPublic=&amountPerPage=1000");
            RstClient.Timeout = -1;
            var RestRequest = new RestRequest(Method.GET);
            RestRequest.AddHeader("Cookie", "XSRF-TOKEN=eyJpdiI6IjNtbUp4OE51aXU2Yjk1Rk14MkNSMGc9PSIsInZhbHVlIjoiMm9tVU96ZlBLOWZXcEpKY040UXhid1ZHRzBTeVBMNHZYVUpoaCt5MjE5alVDOVVBSWphK0RoN1BHMFNaRmRQciIsIm1hYyI6IjZiNDFkYzEzZDIwNDE5NzhjZmJhNmQ0MGU2YmZkODQzZDJlODZiY2QzNjZmZTY1YjI3Mjg1Nzg3N2Y4YjVkNGEifQ%3D%3D; e572d25e2f77b01121404f0eef127450=7af44baa71dff9a37418e0d617d510fa; laravel_session=eyJpdiI6ImtXdVhmekJnZVZyNXVKeTZvc3VzeWc9PSIsInZhbHVlIjoiQXNES1RXS216Nmw1S2R4OGUxaTBEcXYzQ3VhcCtWcFd4ZVlhNUlHMERzRlJscWNQaGdKTXByTWw4eTZIM01rcCIsIm1hYyI6IjFhYzMyYjQ0NDNkOTFjN2Q2ZDI4NDUzYzQyZmNkYTAzY2JjODQxYWEwN2M0NWY4ZTE2OTFhZmEwYmFmNWM1NjEifQ%3D%3D");
            RestRequest.AddHeader("Cookie", cookieFirst);
            RestRequest.AddHeader("Cookie", cookieSecond);
            IRestResponse RstResponse = RstClient.Execute(RestRequest);
            web.LoadHtml(PostResponse.Content);



            var linkCount = 1;

            for (int i = 0; ;i++)
            {
                var LinkClient = new RestClient($"https://mojegs1.pl/moje-produkty/{linkCount}");
                LinkClient.Timeout = -1;
                var LinkRequest = new RestRequest(Method.GET);
                LinkRequest.AddHeader("Cookie", "XSRF-TOKEN=eyJpdiI6IjNtbUp4OE51aXU2Yjk1Rk14MkNSMGc9PSIsInZhbHVlIjoiMm9tVU96ZlBLOWZXcEpKY040UXhid1ZHRzBTeVBMNHZYVUpoaCt5MjE5alVDOVVBSWphK0RoN1BHMFNaRmRQciIsIm1hYyI6IjZiNDFkYzEzZDIwNDE5NzhjZmJhNmQ0MGU2YmZkODQzZDJlODZiY2QzNjZmZTY1YjI3Mjg1Nzg3N2Y4YjVkNGEifQ%3D%3D; e572d25e2f77b01121404f0eef127450=7af44baa71dff9a37418e0d617d510fa; laravel_session=eyJpdiI6ImtXdVhmekJnZVZyNXVKeTZvc3VzeWc9PSIsInZhbHVlIjoiQXNES1RXS216Nmw1S2R4OGUxaTBEcXYzQ3VhcCtWcFd4ZVlhNUlHMERzRlJscWNQaGdKTXByTWw4eTZIM01rcCIsIm1hYyI6IjFhYzMyYjQ0NDNkOTFjN2Q2ZDI4NDUzYzQyZmNkYTAzY2JjODQxYWEwN2M0NWY4ZTE2OTFhZmEwYmFmNWM1NjEifQ%3D%3D");
                IRestResponse LinkResponse = LinkClient.Execute(LinkRequest);
                linkCount++;
                
                
                
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
                        var PorductFullName = items[1].InnerText.Trim();
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

                        
                        var nazvaProductu = new List<string>();
                        nazvaProductu.Add(PorductFullName);


                        var gting = new List<string>();
                        gting.Add(items[3].InnerText.Trim());


                        foreach (var gtinImages in gting)
                        {
                            using (HttpClient cl = new HttpClient())
                            {
                                cl.DefaultRequestHeaders.Add("Cookie", "XSRF-TOKEN=eyJpdiI6ImdVZnBTa25xekswUlNsXC9FcTI5TmRBPT0iLCJ2YWx1ZSI6ImR5K1hQcWNwVk9vRlRnSDJkTW1US25MQWszVjNyY1R6SFwvUGJwOVFmVXZya090aDZsQ2I5QnhHRUFPTE9GdzI2IiwibWFjIjoiMTcxNzExMGI0MTI3NmU0NDliMGUwZTYwNDhjYTA3MDc5Zjk3Y2EzN2VjZWVjYzRhNWQyYTBkNDU3NjI2OTMzNyJ9; e572d25e2f77b01121404f0eef127450=6c59d20d6edf5cab42e47435612d8098; laravel_session=eyJpdiI6InFhZHF2OUN1Q3g3NmE5cWlNbVZ1K0E9PSIsInZhbHVlIjoiTjFLYldvVExzcXlqbWxYRFNUaERZc0o1R280cGhSQ2ROWVpjZlRNOEcyVjdxMGY4Z1JvdnREQmhjR3N0VHRjQSIsIm1hYyI6ImEzMjUwMGE4ZTRmNTFiN2ExYzA2YTY4ZGYxOTQwYzdjN2Y0ZDAxNmMyOWQ5NjczNmY2ZjQ4OGY2OWQzZDVhZjMifQ%3D%3D"
                                );
                                string url = $"https://mojegs1.pl/moje-produkty/pobierz-etykiete?gtin={gtinImages}&label_text={PorductFullName}&size=1&extension=png";
                               // string url = $"https://mojegs1.pl/moje-produkty/pobierz-etykiete?gtin={gtinImages}&label_text=ekoTuptusie+APLIKACJA+B%C5%81%C4%98KITNE+%C5%81APKI+NA+SZARYM+CLASSIC+rozm.40&size=1&extension=png";
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
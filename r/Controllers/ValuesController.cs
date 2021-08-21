using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using r.Context;
using r.Entities;
using RestSharp;
using RestSharp.Authenticators;
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
            
            HtmlAgilityPack.HtmlDocument web = new HtmlAgilityPack.HtmlDocument();
          
            var UrlAddressClient = new RestClient("https://mojegs1.pl/logowanie");
            UrlAddressClient.Timeout = -1;
            var UrlAddressClientRequest = new RestRequest(Method.GET);
            IRestResponse UrlAddressClientResponse = UrlAddressClient.Execute(UrlAddressClientRequest);
            web.LoadHtml(UrlAddressClientResponse.Content);
            var cookieId = UrlAddressClientResponse.Headers[4].Value;

            var cookieName = UrlAddressClientResponse.Cookies[2].Name;
            var CookieValue = UrlAddressClientResponse.Cookies[2].Value;




            var xtokenpath = "//div/form/input[@name='_token' and @type='hidden']";
            var itess = web.DocumentNode.SelectNodes(xtokenpath).ToList();

            var TokenValue = itess[0].OuterHtml;
            var CookieAuthorizingValue = TokenValue.Replace("<input name=\"_token\" type=\"hidden\" value=\"", "");
            CookieAuthorizingValue = CookieAuthorizingValue.Replace("\">", "");


            //"bYLa9wh6bZilzXlrQltym8kBxT8Ttuh3GNZsmlMq";

            

            var PostClient = new RestClient("https://mojegs1.pl/logowanie/zaloguj-uzytkownika");
            PostClient.Timeout = -1;  
            var PostRequest = new RestRequest(Method.POST);
            //PostRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
           // PostRequest.AddHeader("Cookies", );
            foreach (var cookie in UrlAddressClientResponse.Cookies)
            {
                PostRequest.AddCookie(cookie.Name, cookie.Value);
            }
            PostRequest.AddParameter("_token", CookieAuthorizingValue);
            PostRequest.AddParameter("email", "tomek@fiorino.eu");
            PostRequest.AddParameter("password", "Epoka1-wsx");
            
            IRestResponse PostResponse = PostClient.Execute(PostRequest);

            var xrf = PostResponse.Cookies[0].Value;
            var laravelsession = PostResponse.Cookies[1].Value;
            var rs = PostResponse.Headers[4].Value;



            web.LoadHtml(PostResponse.Content);

          
            var RstClient = new RestClient("https://mojegs1.pl/moje-produkty/sortowanie/nazwa/kierunek/rosnaco/1?searchText=&isPublic=&amountPerPage=100");
            RstClient.Timeout = -1;
            var RestRequest = new RestRequest(Method.GET);
            
            RestRequest.AddHeader("Cookie", $"XSRF-TOKEN={xrf}; laravel_session={laravelsession}");
            foreach (var cookie in PostResponse.Cookies)
            {
                PostRequest.AddCookie(cookie.Name, cookie.Value);
            }
            IRestResponse RstResponse = RstClient.Execute(RestRequest);
            web.LoadHtml(RstResponse.Content);


            var headerCookie = RstResponse.Headers[4].Value;

                
                var linkCount = 1;

                for (int i = 0; ; i++)
                {
                     var RstClientNew = new RestClient($"https://mojegs1.pl/moje-produkty/{linkCount}");
                    RstClientNew.Timeout = -1;
                    var RestRequestNew = new RestRequest(Method.GET);
                // RestRequest = new RestRequest(Method.GET);
                    RestRequestNew.AddHeader("Cookie", $"XSRF-TOKEN={xrf}; laravel_session={laravelsession}");
                    IRestResponse rsponseCookie = RstClientNew.Execute(RestRequestNew);

                    foreach (var cookie in RstResponse.Cookies)
                    {
                        PostRequest.AddCookie(cookie.Name, cookie.Value);
                    }
                    

                    web.LoadHtml(rsponseCookie.Content);
                    linkCount++;
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
                                    cl.DefaultRequestHeaders.Add("Cookie", $"XSRF-TOKEN={xrf}; laravel_session={laravelsession}");
                                    string url = $"https://mojegs1.pl/moje-produkty/pobierz-etykiete?gtin={gtinImages}&label_text={PorductFullName}&size=1&extension=png";
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
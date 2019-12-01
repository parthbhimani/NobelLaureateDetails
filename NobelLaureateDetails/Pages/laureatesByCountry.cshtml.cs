using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NobelLaureateDetails;

namespace NobelLaureatesAPI
{
    public class LaureatesByCountry : PageModel
    {
        public JsonResult OnGet()
        {
            string country = HttpContext.Request.Query["country"];

            string url = "http://api.nobelprize.org/v1/laureate.json?";
            if (country.Length == 2)
            {
                url = url + "bornCountryCode=" + country;
            }
            else
            {
                url = url + "bornCountry=" + country;
            }

            string laureateDetails = getData(url);

            LaureateDetails array = LaureateDetails.FromJson(laureateDetails);

            return new JsonResult(array);
        }

        private string getData(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(url);
            }
        }
    }
}
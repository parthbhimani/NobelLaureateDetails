using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NobelLaureateDetails.Pages
{
    [BindProperties]
    public class LaureatesModel : PageModel
    {
        public bool DataFetched { get; set; }
        private int paramCount = 0;
        public void OnPost()
        {
            string url = generateUrlFromRequest();
            string laureates = getData(url);
            Prizes.Laureates Laureates = Prizes.Laureates.FromJson(laureates);
            ViewData["ResultLaureates"] = Laureates.PrizesArray;
            DataFetched = true;
        }

        private string generateUrlFromRequest()
        {
            String Category = Request.Form["Category"];
            String YearFrom = Request.Form["YearFrom"];
            String YearTo = Request.Form["YearTo"];

            string url = "http://api.nobelprize.org/v1/prize.json";

            if (!Category.Equals("none"))
            {
                url = url + getParamSeperator() + "category=" + Category;
            }
            if (YearFrom.Length == 4)
            {
                url = url + getParamSeperator() + "year=" + YearFrom;
            }
            if (YearTo.Length == 4)
            {
                url = url + getParamSeperator() + "yearTo=" + YearTo;
            }

            return url;
        }

        

        private string getParamSeperator()
        {
            if (paramCount == 0)
            {
                paramCount++;
                return "?";
            }
            else {
                paramCount++;
                return "&";
            }
        }

        private string getData(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(url);
            }
        }
    }

    public static class Util
    {
        public static string FirstCharToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
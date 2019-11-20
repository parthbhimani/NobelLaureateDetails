using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NobelLaureateDetails.Pages
{
    public class IndexModel : PageModel
    {
        public JsonResult OnGet()
        {
            List<Laureate> filteredLaureates = new List<Laureate>();

            string physicsNinetyEightlaureates = getJsonFromUrl("http://api.nobelprize.org/v1/prize.json?category=physics&year=1998");
            Prizes.Laureates Laureates = Prizes.Laureates.FromJson(physicsNinetyEightlaureates);

            List<long> laureatesIds = new List<long>();

            Prizes.Prize prize = Laureates.PrizesArray[1];
            foreach (Prizes.Laureate laureate in prize.Laureates)
            {
                laureatesIds.Add(laureate.Id);
            }

            foreach (long id in laureatesIds)
            {
                string laureateDetailsJson = getJsonFromUrl("http://api.nobelprize.org/v1/laureate.json?id=" + id);

                LaureateDetails laureateDetails = LaureateDetails.FromJson(laureateDetailsJson);

                foreach (Laureate laureate in laureateDetails.Laureates)
                {
                    filteredLaureates.Add(laureate);
                }
            }

            return new JsonResult(filteredLaureates);
        }

        private string getJsonFromUrl(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(url);
            }
        }
    }
}

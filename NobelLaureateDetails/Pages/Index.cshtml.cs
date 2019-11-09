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

            using (WebClient webClient = new WebClient())
            {
                string PhysicsNinetyEight = webClient.DownloadString("http://api.nobelprize.org/v1/prize.json?category=physics&year=1998");
                Prizes.Laureates Laureates = Prizes.Laureates.FromJson(PhysicsNinetyEight);

                List<long> filteredIds = new List<long>();

                foreach (Prizes.Prize prize in Laureates.PrizesArray)
                {
                    foreach (Prizes.Laureate laureate in prize.Laureates) {
                        filteredIds.Add(laureate.Id);
                    }
                }

                foreach (long id in filteredIds) {
                    string laureateDetails = webClient.DownloadString("http://api.nobelprize.org/v1/laureate.json?id="+id);

                    LaureateDetails array = LaureateDetails.FromJson(laureateDetails);

                    foreach (Laureate laureate in array.Laureates) {
                        filteredLaureates.Add(laureate);
                    }
                }

                return new JsonResult(filteredLaureates);
            }
        }
    }
}

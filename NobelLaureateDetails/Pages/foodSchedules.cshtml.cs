using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FoodSchedules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NobelLaureateDetails.Pages
{
    public class foodSchedulesModel : PageModel
    {
        public void OnGet()
        {
            string output = getData("https://mobilefoodschedules.azurewebsites.net/api/SAApprovedFoodSchedules");
            var schedules = Schedules.FromJson(output);
            ViewData["schedules"] = schedules;

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

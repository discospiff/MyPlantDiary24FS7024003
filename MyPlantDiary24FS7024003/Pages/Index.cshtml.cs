using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPlantDiary24FS7024003.JSONFeeds.PlantPlacesSpeicmens;

namespace MyPlantDiary24FS7024003.Pages
{
    public class IndexModel : PageModel
    {
        HttpClient httpClient = new HttpClient();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Render our home page and show default specimens.
        /// </summary>
        public void OnGet()
        {

            Task<HttpResponseMessage> task = httpClient.GetAsync("https://raw.githubusercontent.com/discospiff/data/refs/heads/main/specimens.json");
            HttpResponseMessage specimenResult = task.Result;

            List<Specimen> specimens = new List<Specimen>();
            if (specimenResult.IsSuccessStatusCode)
            {
                Task<string> readString = specimenResult.Content.ReadAsStringAsync();
                string specimenJSON = readString.Result;
                // parse our json
                specimens = Specimen.FromJson(specimenJSON);
            }


            ViewData["Specimens"] = specimens;

            // Make our brand dynamic for white label
            String brand = "My Plant Diary";
            string inBrand = Request.Query["Brand"];
            if (inBrand != null && inBrand.Length >0) {
                brand = inBrand;
            }

            ViewData["Brand"] = brand;



            

        }
    }
}
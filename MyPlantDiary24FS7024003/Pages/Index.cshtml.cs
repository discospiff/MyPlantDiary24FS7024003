using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPlantDiary24FS7024003.JSONFeeds.PlantPlacesSpeicmens;
using PlantPlacesPlants;

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


            

            // https://plantplaces.com/perl/mobile/viewplantsjsonarray.pl?WetTolerant=on
            Task<HttpResponseMessage> plantTask = httpClient.GetAsync("https://raw.githubusercontent.com/discospiff/data/refs/heads/main/plants.md");
            HttpResponseMessage plantResult = plantTask.Result;

            Task<string> plantStringTask = plantResult.Content.ReadAsStringAsync();
            string plantJson = plantStringTask.Result;
            List<Plant> plants = Plant.FromJson(plantJson);

            IDictionary<long, Plant> waterLovingPlants = new Dictionary<long, Plant>(); 
            foreach(Plant plant in plants)
            {
                waterLovingPlants[plant.Id] = plant;
            }

            List<Specimen> waterLovingSpecimens = new List<Specimen>();
            foreach(Specimen specimen in specimens)
            {
                if(waterLovingPlants.ContainsKey(specimen.PlantId))
                {
                    waterLovingSpecimens.Add(specimen);
                }

            }

            // Make our brand dynamic for white label
            String brand = "My Plant Diary";
            string inBrand = Request.Query["Brand"];
            if (inBrand != null && inBrand.Length >0) {
                brand = inBrand;
            }
            ViewData["Specimens"] = waterLovingSpecimens;

            ViewData["Brand"] = brand;



            

        }
    }
}
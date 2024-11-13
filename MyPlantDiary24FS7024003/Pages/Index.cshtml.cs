using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPlantDiary24FS7024003.JSONFeeds.PlantPlacesSpeicmens;
using PlantPlacesPlants;
using WeatherFeed;

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

            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
            string weatherApiKey = config["weatherApiKey"];
            Task<HttpResponseMessage> task = httpClient.GetAsync("https://raw.githubusercontent.com/discospiff/data/refs/heads/main/specimens.json");
            // https://plantplaces.com/perl/mobile/viewplantsjsonarray.pl?WetTolerant=on
            Task<HttpResponseMessage> plantTask = httpClient.GetAsync("https://raw.githubusercontent.com/discospiff/data/refs/heads/main/thirstyplants.json");
            Task<HttpResponseMessage> weatherTask = httpClient.GetAsync("https://api.weatherbit.io/v2.0/current?city=Cincinnati,OH&key=" + weatherApiKey);


            HttpResponseMessage specimenResult =  task.Result;

            List<Specimen> specimens = new List<Specimen>();
            if (specimenResult.IsSuccessStatusCode)
            {
                Task<string> readString = specimenResult.Content.ReadAsStringAsync();
                string specimenJSON = readString.Result;
                // parse our json
                specimens = Specimen.FromJson(specimenJSON);
            }


            

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



            HttpResponseMessage weatherResult = weatherTask.Result;

            Task<string> weatherStringTask = weatherResult.Content.ReadAsStringAsync();
            string weatherJson = weatherStringTask.Result;
            Weather weather = Weather.FromJson(weatherJson);
            List<Datum> weatherData = weather.Data;

            long precip = 0;
            foreach (Datum datum in weatherData)
            {
                precip = datum.Precip;
            }
            if (precip < 1) {
                ViewData["weatherMessage"] = "It's dry!  Water these plants.";
            }
            else
            {
                ViewData["weatherMessage"] = "Rain Expected.  No need to water.";
            }
            ViewData["Specimens"] = waterLovingSpecimens;

            ViewData["Brand"] = brand;



            

        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MyPlantDiary24FS7024003.JSONFeeds.PlantPlacesSpeicmens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyPlantDiary24FS7024003
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Specimen> Get()
        {
            return SpecimenRepository.specimens;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Specimen Get(int id)
        {
            return SpecimenRepository.specimens[id];
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

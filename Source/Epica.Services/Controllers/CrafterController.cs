namespace Epica.Services.Controllers
{
    using Data;
    using Data.Repositories;
    using Data.Models;
    using System.Web.Http;
    using System.Linq;

    public class CrafterController : ApiController
    {
        private readonly EpicaDbContext db;

        public CrafterController()
        {
            this.db = new EpicaDbContext();
        }
        // if you want to use DATABASE USE  it with this.db

        public string Get()
        {
            IRepository<Crafter> craftersRepo = new EfRepository<Crafter>();
            var crafters = craftersRepo.All().ToList();
            string getCrafter = "";

            int i = 0;
            foreach (var crafter in crafters)
            {
                i++;
                getCrafter += i + " Crafter Name : " + crafter.CharacterName + "    /*---*******---*/    ";
            }
            return getCrafter;
        }

        public IHttpActionResult Get(int id)
        {
            IRepository<Crafter> craftersRepo = new EfRepository<Crafter>();
            var crafters = craftersRepo.All().ToList();
            string crafter = "";
            try
            {
                crafter = crafters[id - 1].CharacterName;
            }
            catch (System.Exception)
            {
                return BadRequest("There isn' any Crafter with id " + id);
            }

            return Ok(crafter);
        }
    }
}
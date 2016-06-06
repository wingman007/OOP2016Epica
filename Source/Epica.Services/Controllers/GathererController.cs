namespace Epica.Services.Controllers
{
    using Data;
    using Data.Repositories;
    using Data.Models;
    using System.Web.Http;
    using System.Linq;

    public class GathererController : ApiController
    {
        private readonly EpicaDbContext db;

        public GathererController()
        {
            this.db = new EpicaDbContext();
        }
        // if you want to use DATABASE USE  it with this.db
        public string Get()
        {
            IRepository<Gatherer> gatherersRepo = new EfRepository<Gatherer>();
            var gatherers = gatherersRepo.All().ToList();
            string getGatherer = "";

            int i = 0;
            foreach (var gatherer in gatherers)
            {
                i++;
                getGatherer += i + " Gatherer Name : " + gatherer.CharacterName + "    /*---*******---*/    ";
            }
            return getGatherer;
        }

        public IHttpActionResult Get(int id)
        {
            IRepository<Gatherer> gatherersRepo = new EfRepository<Gatherer>();
            var gatherers = gatherersRepo.All().ToList();
            string gatherer = "";
            try
            {
                gatherer = gatherers[id - 1].CharacterName;
            }
            catch (System.Exception)
            {
                return BadRequest("There isn' any Gatherer with id " + id);
            }

            return Ok(gatherer);
        }

    }
}
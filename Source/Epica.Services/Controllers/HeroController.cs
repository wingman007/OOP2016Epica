namespace Epica.Services.Controllers
{
    using Data;
    using System.Web.Http;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using System.Collections.Generic;

    public class HeroController : ApiController
    {
        private readonly EpicaDbContext db;

        public HeroController()
        {
            this.db = new EpicaDbContext();
        }
        // if you want to use DATABASE USE  it with this.db

        public IHttpActionResult Get()
        {
            IRepository<Hero> heroesRepo = new EfRepository<Hero>();
            var heroes = heroesRepo.All().ToList();
            string getHero = "";

            int i = 0;
            foreach (var hero in heroes)
            {
                i++;
                getHero += i + " Hero Name : " + hero.CharacterName + "    /*---*******---*/    ";
            }
            return Ok(getHero);
        }

        public IHttpActionResult Get(int id)
        {
            IRepository<Hero> heroesRepo = new EfRepository<Hero>();
            var heroes = heroesRepo.All().ToList();
            string hero = "";
            try
            {
                hero = heroes[id - 1].CharacterName;
            }
            catch (System.Exception)
            {
                return BadRequest("There isn' any Hero with id " + id);
            }

            return Ok(hero);
        }
    }
}
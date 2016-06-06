namespace Epica.Data.Tests
{
    using System.Data.Entity.Validation;
    using System.Linq;
    using Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Repositories;

    [TestClass]
    public class AddHeroTests
    {
        static IRepository<User> users;
        static IRepository<Hero> heros;

        [TestInitialize]
        public void Initilize()
        {
            users = new EfRepository<User>();
            heros = new EfRepository<Hero>();
        }

        [TestMethod]
        public void TestIfNewHeroIsCreated()
        {
            //Arange
            User user = new User
            {
                Username = "franko",
                Password = "123456",
                Email = "franko@abv.bg"
            };
            Hero hero = new Hero
            {
                CharacterName = "Franko",
                User = user
            };

            //Act
            users.Add(user);
            users.SaveChanges();

            heros.Add(hero);
            heros.SaveChanges();

            User uResult = users.All().First(x => x.Username.Equals("franko"));
            Hero result = heros.All().First(x => x.UserId.Equals(uResult.Id));

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
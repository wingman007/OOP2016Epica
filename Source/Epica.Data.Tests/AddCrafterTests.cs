namespace Epica.Data.Tests
{
    using System.Data.Entity.Validation;
    using System.Linq;
    using Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Repositories;

    [TestClass]
    public class AddCrafterTests
    {
        static IRepository<User> users;
        static IRepository<Crafter> crafters;

        [TestInitialize]
        public void Initilize()
        {
            users = new EfRepository<User>();
            crafters = new EfRepository<Crafter>();
        }

        [TestMethod]
        public void TestIfNewCrafterIsCreated()
        {
            //Arange
            User user = new User
            {
                Username = "misho",
                Password = "123456",
                Email = "misho@abv.bg"
            };
            Crafter crafter = new Crafter
            {
                CharacterName = "Misho",
                User = user
            };

            //Act
            users.Add(user);
            users.SaveChanges();

            crafters.Add(crafter);
            crafters.SaveChanges();

            User uResult = users.All().First(x => x.Username.Equals("misho"));
            Crafter result = crafters.All().First(x => x.UserId.Equals(uResult.Id));

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
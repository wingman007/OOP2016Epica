namespace Epica.Data.Tests
{
    using System.Data.Entity.Validation;
    using System.Linq;
    using Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Repositories;

    [TestClass]
    public class AddGathererTests
    {
        static IRepository<User> users;
        static IRepository<Gatherer> gatherers;

        [TestInitialize]
        public void Initilize()
        {
            users = new EfRepository<User>();
            gatherers = new EfRepository<Gatherer>();
        }

        [TestMethod]
        public void TestIfNewGathererIsCreated()
        {
            //Arange
            User user = new User
            {
                Username = "geri-nikol",
                Password = "123456",
                Email = "geri-nikol@abv.bg"
            };
            Gatherer gatherer = new Gatherer
            {
                CharacterName = "Geri-Nikol",
                User = user
            };

            //Act
            users.Add(user);
            users.SaveChanges();

            gatherers.Add(gatherer);
            gatherers.SaveChanges();

            User uResult = users.All().First(x => x.Username.Equals("geri-nikol"));
            Gatherer result = gatherers.All().First(x => x.UserId.Equals(uResult.Id));

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
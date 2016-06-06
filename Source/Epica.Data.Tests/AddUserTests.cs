namespace Epica.Data.Tests
{
    using System.Data.Entity.Validation;
    using System.Linq;
    using Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Repositories;
    [TestClass]
    public class AddUserTests
    {
        static IRepository<User> users;

        [TestInitialize]
        public void Initilize()
        {
            users = new EfRepository<User>();
        }

        [TestMethod]
        public void TestIfNewUserIsCreatedByUsername()
        {
            //Arange
            User user = new User
            {
                Username = "kon4e",
                Password = "123456",
                Email = "PishkoPishkov@abv.bg"
            };

            //Act
            users.Add(user);
            users.SaveChanges();

            User result = users.All().First(x => x.Username.Equals("kon4e"));

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
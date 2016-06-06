using Epica.Engine.Controllers;

namespace Epica.Engine.Tests
{
    using Data.Models;
    using Data.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CrafterControllerTests
    {
        private CrafterControllerLogic controller { get; set; }
        private IRepository<Crafter> Crafters { get; set; }
        private IRepository<Market> Markets { get; set; }
        private Crafter CurrentCrafter { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            controller = new CrafterControllerLogic();
            Crafters = new EfRepository<Crafter>();
            Markets = new EfRepository<Market>();

            CurrentCrafter = new Crafter();
        }

        public void TryEnquipItemShouldReturnZeroIfItemIsNotFoundInInventory()
        {
            User seller = new User();
            Item item = new Item()
            {
                Type = ItemType.MetalGatherTool,
                Name = ItemName.PickAxe,
                MetalGatherSkill = 2,
                Durability = 8
            };

            controller.TryEquipItem(item);
        }
    }
}
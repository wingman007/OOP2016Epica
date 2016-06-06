namespace Epica.ConsoleClient
{
    using System;
    using Data.Models;
    using Data.Repositories;
    using System.Data.Entity;
    using Data;
    using Data.Migrations;

    public class StartUp
    {
        public static void Main()
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EpicaDbContext, Configuration>());

            IRepository<User> users = new EfRepository<User>();

            Equipment equipment = new Equipment();
            CrafterStats crafterStats = new CrafterStats();
            Inventory inventory = new Inventory();
            Resources resources = new Resources()
            {
                Gold = 200
            };
            Crafter crafter1 = new Crafter()
            {
                CrafterEquipment = equipment,
                CrafterEquipmentId = equipment.Id,
                CrafterStats = crafterStats,
                CrafterStatsId = crafterStats.Id,
                Inventory = inventory,
                InventoryId = inventory.Id,
                Resources = resources,
                ResourcesId = resources.Id,
                CharacterName = "CrafterOne"
            };

            equipment = new Equipment();
            HeroStats heroStats = new HeroStats();
            inventory = new Inventory();
            resources = new Resources()
            {
                Gold = 200
            };
            Hero hero1 = new Hero()
            {
                HeroEquipment = equipment,
                HeroEquipmentId = equipment.Id,
                HeroStats = heroStats,
                HeroStatsId = heroStats.Id,
                Inventory = inventory,
                InventoryId = inventory.Id,
                Resources = resources,
                ResourcesId = resources.Id,
                CharacterName = "HeroOne"
            };

            equipment = new Equipment();
            GathererStats gatherStats = new GathererStats();
            inventory = new Inventory();
            resources = new Resources()
            {
                Gold = 200
            };
            Gatherer gatherer1 = new Gatherer()
            {
                GatherEquipment = equipment,
                GatherEquipmentId = equipment.Id,
                GatherStats = gatherStats,
                GatherStatsId = gatherStats.Id,
                Inventory = inventory,
                InventoryId = inventory.Id,
                Resources = resources,
                ResourcesId = resources.Id,
                CharacterName = "GathererOne"
            };
            User user1 = new User()
            {
                Username = "user1",
                Password = "adminpass",
                Email = "nqma@nqma.com",
                Crafter = crafter1,
                CrafterId = crafter1.Id,
                Hero = hero1,
                HeroId = hero1.Id,
                Gatherer = gatherer1,
                GathererId = gatherer1.Id
            };
            
            Equipment equipment2 = new Equipment();
            CrafterStats crafterStats2 = new CrafterStats();
            Inventory inventory2 = new Inventory();
            Resources resources2 = new Resources()
            {
                Gold = 222
            };
            Crafter crafter2 = new Crafter()
            {
                CrafterEquipment = equipment2,
                CrafterEquipmentId = equipment2.Id,
                CrafterStats = crafterStats2,
                CrafterStatsId = crafterStats2.Id,
                Inventory = inventory2,
                InventoryId = inventory2.Id,
                Resources = resources2,
                ResourcesId = resources2.Id,
                CharacterName = "CrafterTwo"
            };

             equipment2 = new Equipment();
             HeroStats heroStats2 = new HeroStats();
             inventory2 = new Inventory();
             resources2 = new Resources()
            {
                Gold = 222
            };
            Hero hero2 = new Hero()
            {
                HeroEquipment = equipment2,
                HeroEquipmentId = equipment2.Id,
                HeroStats = heroStats2,
                HeroStatsId = heroStats2.Id,
                Inventory = inventory2,
                InventoryId = inventory2.Id,
                Resources = resources2,
                ResourcesId = resources2.Id,
                CharacterName = "HeroTwo"
            };

            equipment2 = new Equipment();
            GathererStats gatherStats2 = new GathererStats();
            inventory2 = new Inventory();
            resources2 = new Resources()
            {
                Gold = 222
            };
            Gatherer gatherer2 = new Gatherer()
            {
                GatherEquipment = equipment2,
                GatherEquipmentId = equipment2.Id,
                GatherStats = gatherStats2,
                GatherStatsId = gatherStats2.Id,
                Inventory = inventory2,
                InventoryId = inventory2.Id,
                Resources = resources2,
                ResourcesId = resources2.Id,
                CharacterName = "GathererTwo"
            };
            User user2 = new User()
            {
                Username = "user2",
                Password = "adminpass",
                Email = "nqma@nqma.com",
                Crafter = crafter2,
                CrafterId = crafter2.Id,
                Hero = hero2,
                HeroId = hero2.Id,
                Gatherer = gatherer2,
                GathererId = gatherer2.Id
            };

            Equipment equipment3 = new Equipment();
            CrafterStats crafterStats3 = new CrafterStats();
            Inventory inventory3 = new Inventory();
            Resources resources3 = new Resources()
            {
                Gold = 300
            };
            Crafter crafter3 = new Crafter()
            {
                CrafterEquipment = equipment3,
                CrafterEquipmentId = equipment3.Id,
                CrafterStats = crafterStats3,
                CrafterStatsId = crafterStats3.Id,
                Inventory = inventory3,
                InventoryId = inventory3.Id,
                Resources = resources3,
                ResourcesId = resources3.Id,
                CharacterName = "CrafterThree"
            };

            equipment3 = new Equipment();
            HeroStats heroStats3 = new HeroStats();
            inventory3 = new Inventory();
            resources3 = new Resources()
            {
                Gold = 333
            };
            Hero hero3 = new Hero()
            {
                HeroEquipment = equipment3,
                HeroEquipmentId = equipment3.Id,
                HeroStats = heroStats3,
                HeroStatsId = heroStats3.Id,
                Inventory = inventory3,
                InventoryId = inventory3.Id,
                Resources = resources3,
                ResourcesId = resources3.Id,
                CharacterName = "HeroThree"
            };

            equipment3 = new Equipment();
            GathererStats gatherStats3 = new GathererStats();
            inventory3 = new Inventory();
            resources3 = new Resources()
            {
                Gold = 333
            };
            Gatherer gatherer3 = new Gatherer()
            {
                GatherEquipment = equipment3,
                GatherEquipmentId = equipment3.Id,
                GatherStats = gatherStats3,
                GatherStatsId = gatherStats3.Id,
                Inventory = inventory3,
                InventoryId = inventory3.Id,
                Resources = resources3,
                ResourcesId = resources3.Id,
                CharacterName = "GathererThree"
            };
            User user3 = new User()
            {
                Username = "user3",
                Password = "adminpass",
                Email = "nqma@nqma.com",
                Crafter = crafter3,
                CrafterId = crafter3.Id,
                Hero = hero3,
                HeroId = hero3.Id,
                Gatherer = gatherer3,
                GathererId = gatherer3.Id
            };



            users.Add(user1);
            users.Add(user2);
            users.Add(user3);

            users.SaveChanges();
            var usersList = users.All();
            foreach (var user in usersList)
            {
                Console.WriteLine("Id: {0} | Username: {1}", user.Id, user.Username);
            }

            Console.ReadKey();
        }
    }
}

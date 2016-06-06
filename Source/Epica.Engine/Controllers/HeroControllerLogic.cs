namespace Epica.Engine.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Epica.Data.Models;
    using Epica.Data.Repositories;
    using Epica.Engine.Common.Constants;

    public class HeroControllerLogic
    {
        IRepository<Hero> heroes = new EfRepository<Hero>();
        IRepository<Field> fields = new EfRepository<Field>();
        static IRepository<Market> markets = new EfRepository<Market>();
        private static Field CurrentField;
        private static Hero CurrentHero = new Hero();

        public HeroControllerLogic()
        {
            // Set Hero To Current Hero (Probably Should be done in User or somewhere before HeroController)
        }

        public IQueryable<Field> GetFieldsForClearing()
        {
            var fieldsforclearing = fields
                .All()
                .Where(field => field.IsUpForClearing == true);
            return fieldsforclearing;
        }

        public void SelectCurrentField(Guid fieldId)
        {
            CurrentField = fields.FindById(fieldId);
        }

        public ICollection<Item> GetInventory()
        {
            return CurrentHero.Inventory.InventoryItems;
        }

        /// <summary>
        /// Try Fight Return
        /// 0 - Field was empty, fight did not occur. Current field set to null.
        /// 1 - Fight occurred, field was cleared. Current field set to null.
        /// 2 - Fight occurred, field still populated.
        /// 3 - Fight occurred, field was cleared. Current field set to null. Item(s) Broken.
        /// 4 - Fight occurred, field still populated. Item(s) Broken.
        /// </summary>
        /// <returns></returns>
        public int TryFight()
        {
            int fightoccurred = 0;
            double monHp = GlobalConstants.MonsterHp;
            if (CurrentField.MonsterCount > 0)
            {
                while (monHp > 0)
                {
                    double herodmg = CurrentHero.HeroStats.Damage;
                    foreach (var equipedItem in CurrentHero.HeroEquipment.EquipedItems)
                    {
                        if (equipedItem.Type == ItemType.Weapon)
                        {
                            herodmg += equipedItem.Damage;
                        }
                    }
                    double heroarmor = CurrentHero.HeroStats.Armor;
                    foreach (var equipedItem in CurrentHero.HeroEquipment.EquipedItems)
                    {
                        if (equipedItem.Type == ItemType.ArmorSet)
                        {
                            heroarmor += equipedItem.Armor;
                        }
                    }
                    monHp -= herodmg - GlobalConstants.MonsterArmor;
                    double monDmg = GlobalConstants.MonsterDmg - heroarmor;
                    if (monDmg < 0)
                    {
                        monDmg = 0;
                    }
                    CurrentHero.HeroStats.Health -= monDmg; //WHAT HAPPENS IF HERO HP < 0 
                }
                if (CurrentHero.HeroEquipment.EquipedItems.Count > 0)
                {
                    foreach (var equipedItem in CurrentHero.HeroEquipment.EquipedItems)
                    {
                        equipedItem.Durability -= 1;
                    }
                    
                }
                CurrentField.MonsterCount--;

                if (CurrentField.MonsterCount == 0)
                {
                    CurrentField.IsUpForClearing = false;
                    CurrentField.IsCleared = true;
                    fields.Update(CurrentField);
                    fields.SaveChanges();
                    CurrentField = null;
                    fightoccurred = 1;
                    foreach (var equipedItem in CurrentHero.HeroEquipment.EquipedItems)
                    {
                        if (equipedItem.Durability <= 0)
                        {
                            fightoccurred = 3;
                            CurrentHero.HeroEquipment.EquipedItems.Remove(equipedItem);
                        }
                    }
                }
                else
                {
                    fields.Update(CurrentField);
                    fields.SaveChanges();
                    fightoccurred = 2;
                    foreach (var equipedItem in CurrentHero.HeroEquipment.EquipedItems)
                    {
                        if (equipedItem.Durability <= 0)
                        {
                            fightoccurred = 4;
                            CurrentHero.HeroEquipment.EquipedItems.Remove(equipedItem);
                        }
                    }
                }
                CurrentHero.Resources.Gold += GlobalConstants.MonsterGold;
                heroes.Update(CurrentHero);
                heroes.SaveChanges();
            }
            else
            {
                CurrentField = null;
            }
            return fightoccurred;
        }

        /// <summary>
        /// Try and equip item. Recieves Item to try equiping. Return: 0 - Item not found in Inventory. 1 - Item Type already equiped. 2 - Item Equiped.
        /// </summary>
        /// <param name="itemtoequip"></param>
        /// <returns></returns>
        public int TryEquipItem(Item itemtoequip)
        {
            int equipState = 0;
            if (CurrentHero.Inventory.InventoryItems.Contains(itemtoequip))
            {
                var equipedItems = CurrentHero.HeroEquipment.EquipedItems;
                bool itemTypeEquiped = false;
                foreach (var equipedItem in equipedItems)
                {
                    if (equipedItem.Type == itemtoequip.Type)
                    {
                        itemTypeEquiped = true;
                    }

                }
                if (!itemTypeEquiped)
                {
                    equipedItems.Add(itemtoequip);
                    CurrentHero.Inventory.InventoryItems.Remove(itemtoequip);
                    CurrentHero.HeroEquipment.EquipedItems = equipedItems;
                    heroes.Update(CurrentHero);
                    heroes.SaveChanges();
                    equipState = 2;
                }
                else
                {
                    equipState = 1;
                }
            }

            return equipState;
        }

        /// <summary>
        /// Try to Unequip an Item. Recieves item to try Unequiping. Returns: 0 - Item not found in Equipment. 1 - Item Unequiped.
        /// </summary>
        /// <param name="itemtounequip"></param>
        /// <returns></returns>
        public int TryUnequipItem(Item itemtounequip)
        {
            int unequipState = 0;
            if (CurrentHero.HeroEquipment.EquipedItems.Contains(itemtounequip))
            {
                CurrentHero.HeroEquipment.EquipedItems.Remove(itemtounequip);
                CurrentHero.Inventory.InventoryItems.Add(itemtounequip);
                heroes.Update(CurrentHero);
                heroes.SaveChanges();
                unequipState = 1;
            }
            return unequipState;
        }

        public ICollection<Item> GetHeroMarketItems()
        {
            ICollection<Item> allMarketItems = GlobalMethods.GetMarketItems();
            ICollection<Item> heroMarketItems = null;
            foreach (var marketItem in allMarketItems)
            {
                if (marketItem.Type.Equals(ItemType.Weapon) || marketItem.Type.Equals(ItemType.ArmorSet))
                {
                    heroMarketItems.Add(marketItem);
                }
            }
            return heroMarketItems;
        }

        /// <summary>
        /// Try Buying Item form Hero from Market. Recieves item to Try Buying Returns: 0 - Item not for Hero. 1 - Item found in market, not enough gold. 2 - Item bought. 3 - Item not found in Market.
        /// </summary>
        /// <param name="itemtobuy"></param>
        /// <returns></returns>
        public int TryBuyHeroMarketItem(Item itemtobuy)
        {
            int buyState = 0;
            if (itemtobuy.Type == ItemType.ArmorSet || itemtobuy.Type == ItemType.Weapon)
            {
                List<Market> globalMarket = markets.All().ToList();
                if (globalMarket[0].Item.Contains(itemtobuy))
                {
                    if (CurrentHero.Resources.Gold > itemtobuy.Price)
                    {
                        CurrentHero.Resources.Gold -= itemtobuy.Price;
                        itemtobuy.Seller.Crafter.Resources.Gold += itemtobuy.Price;
                        globalMarket[0].Item.Remove(itemtobuy);
                        CurrentHero.Inventory.InventoryItems.Add(itemtobuy);
                        heroes.Update(CurrentHero);
                        heroes.SaveChanges();
                        IRepository<Crafter> crafters = new EfRepository<Crafter>();
                        crafters.Update(itemtobuy.Seller.Crafter);
                        crafters.SaveChanges();
                        markets.Update(globalMarket[0]);
                        markets.SaveChanges();
                        buyState = 2;
                    }
                    else
                    {
                        buyState = 1;
                    }
                }
                else
                {
                    buyState = 3;
                }
            }
            return buyState;
        }
    }
}

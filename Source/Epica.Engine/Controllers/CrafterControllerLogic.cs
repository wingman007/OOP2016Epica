namespace Epica.Engine.Controllers
{
    using System.Collections.Generic;
    using Data.Models;
    using Data.Repositories;
    using Common.Constants;
    using System;
    using System.Linq;

    public class CrafterControllerLogic
    {
        IRepository<Crafter> crafters = new EfRepository<Crafter>();
        static IRepository<Market> markets = new EfRepository<Market>();

        private static Crafter CurrentCrafter = new Crafter();

        /// <summary>
        /// Try and equip item. Return: 0 - Item not found in Inventory. 1 - Item Type already equiped. 2 - Item Equiped.
        /// </summary>
        /// <param name="itemtoequip"></param>
        /// <returns></returns>
        public int TryEquipItem(Item itemtoequip)
        {
            int equipState = 0;
            if (CurrentCrafter.Inventory.InventoryItems.Contains(itemtoequip))
            {
                var equipedItems = CurrentCrafter.CrafterEquipment.EquipedItems;
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
                    CurrentCrafter.Inventory.InventoryItems.Remove(itemtoequip);
                    CurrentCrafter.CrafterEquipment.EquipedItems = equipedItems;
                    crafters.Update(CurrentCrafter);
                    crafters.SaveChanges();
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
        /// Try to Unequip an Item. Returns: 0 - Item not found in Equipment. 1 - Item Unequiped.
        /// </summary>
        /// <param name="itemtounequip"></param>
        /// <returns></returns>
        public int TryUnequipItem(Item itemtounequip)
        {
            int unequipState = 0;
            if (CurrentCrafter.CrafterEquipment.EquipedItems.Contains(itemtounequip))
            {
                CurrentCrafter.CrafterEquipment.EquipedItems.Remove(itemtounequip);
                CurrentCrafter.Inventory.InventoryItems.Add(itemtounequip);
                crafters.Update(CurrentCrafter);
                crafters.SaveChanges();
                unequipState = 1;
            }
            return unequipState;
        }

        /// <summary>
        /// Try selling items. Recieves: 1 - type of item to sell. 2- number of items to sell. 3 - Sell Price of item. Returns: 0 - Not enough items. 1 - Item Sold.
        /// </summary>
        /// <param name="typeToSell"></param>
        /// <param name="numberOfReourcePacks"></param>
        /// <param name="sellPrice"></param>
        /// <returns></returns>
        public int TrySellItem(ItemType typeToSell, int numberOfItems, int sellPrice)
        {
            int sellState = 0;
            List<Market> globalMarket = markets.All().ToList();
            List<Item> itemCollection = null;
            foreach (var item in CurrentCrafter.Inventory.InventoryItems)
            {
                if (item.Type == typeToSell)
                {
                    itemCollection.Add(item);
                }
            }
            if (itemCollection.Count >= numberOfItems)
            {
                for (int i = 0; i < numberOfItems; i++)
                {
                    CurrentCrafter.Inventory.InventoryItems.Remove(itemCollection[i]);
                    itemCollection[i].Price = sellPrice;
                    itemCollection[i].Seller = CurrentCrafter.User;
                    itemCollection[i].UserId = CurrentCrafter.UserId;
                    globalMarket[0].Item.Add(itemCollection[i]);
                }
                markets.Update(globalMarket[0]);
                markets.SaveChanges();
                sellState = 1;
            }
            return sellState;
        }

        /// <summary>
        /// Try Buying Item form Hero from Market. Recieves item to Try Buying Returns: 0 - ResourcePack not found. 1 - Item found in market, not enough gold. 2 - Item bought.
        /// </summary>
        /// <param name="itemtobuy"></param>
        /// <returns></returns>
        public int TryBuyResourcePack(ResourcePack packToBuy)
        {
            int buyState = 0;
            List<Market> globalMarket = markets.All().ToList();
            if (globalMarket[0].ResourcePacks.Contains(packToBuy))
            {
                if (CurrentCrafter.Resources.Gold > packToBuy.Price)
                {
                    CurrentCrafter.Resources.Gold -= packToBuy.Price;
                    packToBuy.Seller.Gatherer.Resources.Gold += packToBuy.Price;
                    globalMarket[0].ResourcePacks.Remove(packToBuy);
                    CurrentCrafter.Resources.ResourcePacks.Add(packToBuy);
                    crafters.Update(CurrentCrafter);
                    crafters.SaveChanges();
                    IRepository<Gatherer> gatherers = new EfRepository<Gatherer>();
                    gatherers.Update(packToBuy.Seller.Gatherer);
                    gatherers.SaveChanges();
                    markets.Update(globalMarket[0]);
                    markets.SaveChanges();
                    buyState = 2;
                }
                else
                {
                    buyState = 1;
                }
            }

            return buyState;
        }

        public Array GetItemTypesToCraft() // NO IDEA WHAT IT RETURNS. NEEDS TO RETURN ITEM TYPES SO THE USER CAN SELECT WHAT TO CRAFT
        {
            var values = Enum.GetValues(typeof(ItemName));
            return values;
        }

        /// <summary>
        /// Try crafting item. Recieves Item Name of the Item To Craft. Returns: 0 - Not Enough ResourcePacks. 1 - Item Crafted.
        /// </summary>
        /// <param name="itemToCraft"></param>
        public int TryCraftItem(ItemName itemToCraft)
        {
            int craftState = 0;
            List <ResourcePack>  resourcePacks;
            int woodCraftSkill = CurrentCrafter.CrafterStats.WoodCraftSkill;
            foreach (var equipedItem in CurrentCrafter.CrafterEquipment.EquipedItems)
            {
                if (equipedItem.Type == ItemType.WoodCraftTool)
                {
                     woodCraftSkill += equipedItem.WoodCraftSkill;
                }
            }
            int woodCraftCost = Convert.ToInt32(Math.Round(CrafterConstants.ChoppingAxeBaseCost /woodCraftSkill, 0));
            int metalCraftSkill = CurrentCrafter.CrafterStats.MetalCraftSkill;
            foreach (var equipedItem in CurrentCrafter.CrafterEquipment.EquipedItems)
            {
                if (equipedItem.Type == ItemType.MetalCraftTool)
                {
                    metalCraftSkill += equipedItem.MetalCraftSkill;
                }
            }
            int metalCraftCost = Convert.ToInt32(Math.Round(CrafterConstants.ChoppingAxeBaseCost / metalCraftSkill, 0));

            switch (itemToCraft)
            {
                case ItemName.ChoppingAxe:
                    
                    resourcePacks = GetCrafterWoodResourcePacks();
                    if (resourcePacks.Count >= woodCraftCost)
                    {
                        for (int i = 0; i < woodCraftCost; i++)
                        {
                            CurrentCrafter.Resources.ResourcePacks.Remove(resourcePacks[i]);
                        }
                        CurrentCrafter.Inventory.InventoryItems.Add(new Item()
                        {
                            Type = ItemType.WoodGatherTool,
                            Name = ItemName.ChoppingAxe,
                            WoodGatherSkill = 2,
                            Durability = 8
                        });
                        crafters.Update(CurrentCrafter);
                        crafters.SaveChanges();
                        craftState = 1;
                    }
                    break;
                case ItemName.Saw:
                    resourcePacks = GetCrafterWoodResourcePacks();
                    if (resourcePacks.Count >= woodCraftCost)
                    {
                        for (int i = 0; i < woodCraftCost; i++)
                        {
                            CurrentCrafter.Resources.ResourcePacks.Remove(resourcePacks[i]);
                        }
                        CurrentCrafter.Inventory.InventoryItems.Add(new Item()
                        {
                            Type = ItemType.WoodCraftTool,
                            Name = ItemName.Saw,
                            WoodCraftSkill = 2,
                            Durability = 8
                        });
                        crafters.Update(CurrentCrafter);
                        crafters.SaveChanges();
                        craftState = 1;
                    }
                    break;
                case ItemName.WoodenAxe:
                    resourcePacks = GetCrafterWoodResourcePacks();
                    if (resourcePacks.Count >= woodCraftCost)
                    {
                        for (int i = 0; i < woodCraftCost; i++)
                        {
                            CurrentCrafter.Resources.ResourcePacks.Remove(resourcePacks[i]);
                        }
                        CurrentCrafter.Inventory.InventoryItems.Add(new Item()
                        {
                            Type = ItemType.Weapon,
                            Name = ItemName.WoodenAxe,
                            Damage = 4,
                            Durability = 8
                        });
                        crafters.Update(CurrentCrafter);
                        crafters.SaveChanges();
                        craftState = 1;
                    }
                    break;
                case ItemName.PickAxe:
                    resourcePacks = GetCrafterMetalResourcePacks();
                    if (resourcePacks.Count >= metalCraftCost)
                    {
                        for (int i = 0; i < metalCraftCost; i++)
                        {
                            CurrentCrafter.Resources.ResourcePacks.Remove(resourcePacks[i]);
                        }
                        CurrentCrafter.Inventory.InventoryItems.Add(new Item()
                        {
                            Type = ItemType.MetalGatherTool,
                            Name = ItemName.PickAxe,
                            MetalGatherSkill = 2,
                            Durability = 8
                        });
                        crafters.Update(CurrentCrafter);
                        crafters.SaveChanges();
                        craftState = 1;
                    }
                    break;
                case ItemName.CraftingHammer:
                    resourcePacks = GetCrafterMetalResourcePacks();
                    if (resourcePacks.Count >= metalCraftCost)
                    {
                        for (int i = 0; i < metalCraftCost; i++)
                        {
                            CurrentCrafter.Resources.ResourcePacks.Remove(resourcePacks[i]);
                        }
                        CurrentCrafter.Inventory.InventoryItems.Add(new Item()
                        {
                            Type = ItemType.MetalCraftTool,
                            Name = ItemName.CraftingHammer,
                            MetalCraftSkill = 2,
                            Durability = 8
                        });
                        crafters.Update(CurrentCrafter);
                        crafters.SaveChanges();
                        craftState = 1;
                    }
                    break;
                case ItemName.MetalArmorSet:
                    resourcePacks = GetCrafterMetalResourcePacks();
                    if (resourcePacks.Count >= metalCraftCost)
                    {
                        for (int i = 0; i < metalCraftCost; i++)
                        {
                            CurrentCrafter.Resources.ResourcePacks.Remove(resourcePacks[i]);
                        }
                        CurrentCrafter.Inventory.InventoryItems.Add(new Item()
                        {
                            Type = ItemType.ArmorSet,
                            Name = ItemName.MetalArmorSet,
                            Armor = 3,
                            Durability = 8
                        });
                        crafters.Update(CurrentCrafter);
                        crafters.SaveChanges();
                        craftState = 1;
                    }
                    break;
            }
            return craftState;
        }

        private List<ResourcePack> GetCrafterWoodResourcePacks()
        {
            List<ResourcePack> woodResourcePackCollection = null;
            foreach (var resourcePack in CurrentCrafter.Resources.ResourcePacks)
            {
                if (resourcePack.Type == ResourcePackType.Wood)
                {
                    woodResourcePackCollection.Add(resourcePack);
                }
            }
            return woodResourcePackCollection;
        }

        private List<ResourcePack> GetCrafterMetalResourcePacks()
        {
            List<ResourcePack> metalResourcePackCollection = null;
            foreach (var resourcePack in CurrentCrafter.Resources.ResourcePacks)
            {
                if (resourcePack.Type == ResourcePackType.Metal)
                {
                    metalResourcePackCollection.Add(resourcePack);
                }
            }
            return metalResourcePackCollection;
        }
    }
}

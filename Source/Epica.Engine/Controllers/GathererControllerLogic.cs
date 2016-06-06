namespace Epica.Engine.Controllers
{
    using Data.Repositories;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Epica.Engine.Common.Constants;

    public class GathererControllerLogic
    {
        IRepository<Gatherer> gatherers = new EfRepository<Gatherer>();
        static IRepository<Market> markets = new EfRepository<Market>();

        private static Gatherer CurrentGatherer = new Gatherer();

        /// <summary>
        /// Try gathering Resources. Recives Resource Type to gather. Returns: 0 - Field is not cleared. 1 - Field Cleared, No Resources of Type. 2 - All resources left gathered. 3 - Resources gathere, more left. 4 - All resources left gathered. Gathering Tool Broken. 5 - Resources gathere, more left. Gathering Tool Broken.
        /// </summary>
        /// <returns></returns>

        public int TryGatherResources(ResourcePackType typeToGather)
        {
            int gatherState = 0;
            if (CurrentGatherer.Field.IsCleared)
            {

                int resCount = 0;
                switch (typeToGather)
                {
                    case ResourcePackType.Metal:
                        resCount = CurrentGatherer.Field.MetalCount;
                        break;
                    case ResourcePackType.Wood:
                        resCount = CurrentGatherer.Field.WoodCount;
                        break;
                }
                if (resCount > 0)
                {
                    int skill = 0;
                    switch (typeToGather)
                    {
                        case ResourcePackType.Metal:
                            skill = CurrentGatherer.GatherStats.MetalGatherSkill;
                            foreach (var equipedItem in CurrentGatherer.GatherEquipment.EquipedItems)
                            {
                                if (equipedItem.Type == ItemType.MetalGatherTool)
                                {
                                    skill += equipedItem.MetalGatherSkill;
                                }
                            }
                            break;
                        case ResourcePackType.Wood:
                            skill = CurrentGatherer.GatherStats.WoodGatherSkill;
                            foreach (var equipedItem in CurrentGatherer.GatherEquipment.EquipedItems)
                            {
                                if (equipedItem.Type == ItemType.WoodGatherTool)
                                {
                                    skill += equipedItem.WoodGatherSkill;
                                }
                            }
                            break;
                    }
                    if (skill > resCount)
                    {
                        for (int i = 0; i < resCount; i++)
                        {
                            CurrentGatherer.Resources.ResourcePacks.Add(new ResourcePack()
                            {
                                Type = typeToGather
                            });
                        }
                        resCount = 0;
                        switch (typeToGather)
                        {
                            case ResourcePackType.Metal:
                                CurrentGatherer.Field.MetalCount = resCount;
                                gatherState = 2;
                                foreach (var equipedItem in CurrentGatherer.GatherEquipment.EquipedItems)
                                {
                                    if (equipedItem.Type == ItemType.MetalGatherTool)
                                    {
                                        equipedItem.Durability -= 1;
                                        if (equipedItem.Durability <= 0)
                                        {
                                            CurrentGatherer.GatherEquipment.EquipedItems.Remove(equipedItem);
                                            gatherState = 4;
                                        }
                                    }
                                }
                                if (CurrentGatherer.Field.WoodCount == 0)
                                {
                                    CurrentGatherer.Field.IsCleared = false;
                                }
                                break;
                            case ResourcePackType.Wood:
                                CurrentGatherer.Field.WoodCount = resCount;
                                gatherState = 2;
                                foreach (var equipedItem in CurrentGatherer.GatherEquipment.EquipedItems)
                                {
                                    if (equipedItem.Type == ItemType.WoodGatherTool)
                                    {
                                        equipedItem.Durability -= 1;
                                        if (equipedItem.Durability <= 0)
                                        {
                                            CurrentGatherer.GatherEquipment.EquipedItems.Remove(equipedItem);
                                            gatherState = 4;
                                        }
                                    }
                                }
                                if (CurrentGatherer.Field.MetalCount == 0)
                                {
                                    CurrentGatherer.Field.IsCleared = false;
                                }
                                break;
                        }
                        gatherers.Update(CurrentGatherer);
                        gatherers.SaveChanges();
                    }
                    else
                    {
                        for (int i = 0; i < skill; i++)
                        {
                            CurrentGatherer.Resources.ResourcePacks.Add(new ResourcePack()
                            {
                                Type = typeToGather
                            });
                        }
                        switch (typeToGather)
                        {
                            case ResourcePackType.Metal:
                                foreach (var equipedItem in CurrentGatherer.GatherEquipment.EquipedItems)
                                {
                                    if (equipedItem.Type == ItemType.MetalGatherTool)
                                    {
                                        equipedItem.Durability -= 1;
                                        if (equipedItem.Durability <= 0)
                                        {
                                            CurrentGatherer.GatherEquipment.EquipedItems.Remove(equipedItem);
                                            gatherState = 5;
                                        }
                                    }
                                }
                                break;
                            case ResourcePackType.Wood:
                                foreach (var equipedItem in CurrentGatherer.GatherEquipment.EquipedItems)
                                {
                                    if (equipedItem.Type == ItemType.WoodGatherTool)
                                    {
                                        equipedItem.Durability -= 1;
                                        if (equipedItem.Durability <= 0)
                                        {
                                            CurrentGatherer.GatherEquipment.EquipedItems.Remove(equipedItem);
                                            gatherState = 5;
                                        }
                                    }
                                }
                                break;
                        }
                        CurrentGatherer.Field.MetalCount -= skill;
                        gatherers.Update(CurrentGatherer);
                        gatherers.SaveChanges();
                        gatherState = 3;
                    }
                }
                else
                {
                    gatherState = 1;
                }
            }
            return gatherState;
        }

        /// <summary>
        /// Try and equip item. Recieves Item to try equiping. Return: 0 - Item not found in Inventory. 1 - Item Type already equiped. 2 - Item Equiped.
        /// </summary>
        /// <param name="itemtoequip"></param>
        /// <returns></returns>
        public int TryEquipItem(Item itemtoequip)
        {
            int equipState = 0;
            if (CurrentGatherer.Inventory.InventoryItems.Contains(itemtoequip))
            {
                var equipedItems = CurrentGatherer.GatherEquipment.EquipedItems;
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
                    CurrentGatherer.Inventory.InventoryItems.Remove(itemtoequip);
                    CurrentGatherer.GatherEquipment.EquipedItems = equipedItems;
                    gatherers.Update(CurrentGatherer);
                    gatherers.SaveChanges();
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
        /// Try to Unequip an Item. Recieves Item to try unequiping. Returns: 0 - Item not found in Equipment. 1 - Item Unequiped.
        /// </summary>
        /// <param name="itemtounequip"></param>
        /// <returns></returns>
        public int TryUnequipItem(Item itemtounequip)
        {
            int unequipState = 0;
            if (CurrentGatherer.GatherEquipment.EquipedItems.Contains(itemtounequip))
            {
                CurrentGatherer.GatherEquipment.EquipedItems.Remove(itemtounequip);
                CurrentGatherer.Inventory.InventoryItems.Add(itemtounequip);
                gatherers.Update(CurrentGatherer);
                gatherers.SaveChanges();
                unequipState = 1;
            }
            return unequipState;
        }

        public ICollection<Item> GetGathererMarketItems()
        {
            ICollection<Item> allMarketItems = GlobalMethods.GetMarketItems();
            ICollection<Item> gathererMarketItems = null;
            foreach (var marketItem in allMarketItems)
            {
                if (marketItem.Type.Equals(ItemType.WoodGatherTool) || marketItem.Type.Equals(ItemType.MetalGatherTool))
                {
                    gathererMarketItems.Add(marketItem);
                }
            }
            return gathererMarketItems;
        }

        /// <summary>
        /// Try Buying Item form Hero from Market. Recieves Item to try Buying. Returns: 0 - Item not found in market. 1 - Item found in market, not enough gold. 2 - Item bought.
        /// </summary>
        /// <param name="itemtobuy"></param>
        /// <returns></returns
        public int TryBuyGathererMarketItem(Item itemtobuy)
        {
            int buyState = 0;
            List<Market> globalMarket = markets.All().ToList();
            if (globalMarket[0].Item.Contains(itemtobuy))
            {
                if (CurrentGatherer.Resources.Gold > itemtobuy.Price)
                {
                    CurrentGatherer.Resources.Gold -= itemtobuy.Price;
                    itemtobuy.Seller.Crafter.Resources.Gold += itemtobuy.Price;
                    globalMarket[0].Item.Remove(itemtobuy);
                    CurrentGatherer.Inventory.InventoryItems.Add(itemtobuy);
                    gatherers.Update(CurrentGatherer);
                    gatherers.SaveChanges();
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
            return buyState;
        }
        /// <summary>
        /// Try selling resources. Recieves: 1 - ResourcePackType to sell. 2 - number of packs to sell. 3 - Sell Price for pack Returns: 0 - Not enough Resources. 1 - Resources Sold.
        /// </summary>
        /// <param name="typeToSell"></param>
        /// <param name="numberOfReourcePacks"></param>
        /// <param name="sellPrice"></param>
        /// <returns></returns>
        public int TrySellResources(ResourcePackType typeToSell, int numberOfReourcePacks, int sellPrice)
        {
            int sellState = 0;
            List<Market> globalMarket = markets.All().ToList();
            List<ResourcePack> resourcePackCollection = null;
            foreach (var resourcePack in CurrentGatherer.Resources.ResourcePacks)
            {
                if (resourcePack.Type == typeToSell)
                {
                    resourcePackCollection.Add(resourcePack);
                }
            }
            if (resourcePackCollection.Count >= numberOfReourcePacks)
            {
                for (int i = 0; i < numberOfReourcePacks; i++)
                {
                    CurrentGatherer.Resources.ResourcePacks.Remove(resourcePackCollection[i]);
                    resourcePackCollection[i].Price = sellPrice;
                    resourcePackCollection[i].Seller = CurrentGatherer.User;
                    resourcePackCollection[i].UserId = CurrentGatherer.UserId;
                    globalMarket[0].ResourcePacks.Add(resourcePackCollection[i]);
                }
                markets.Update(globalMarket[0]);
                markets.SaveChanges();
                sellState = 1;
            }
            return sellState;
        }

        /// <summary>
        /// Try Activating Filed. Returns: 0 - Field Already Up for clearing. 1 - Field is Cleared. 2 - Not enough Gold. Field Up for Clearing.
        /// </summary>
        /// <returns></returns>
        public int TryActivateField()
        {
            int activationState = 0;
            if (!CurrentGatherer.Field.IsUpForClearing)
            {
                if (!CurrentGatherer.Field.IsCleared)
                {
                    if (CurrentGatherer.Resources.Gold >= GlobalConstants.FieldActivationCost)
                    {
                        CurrentGatherer.Resources.Gold -= GlobalConstants.FieldActivationCost;
                        CurrentGatherer.Field.IsUpForClearing = true;
                        CurrentGatherer.Field.MonsterCount = GlobalConstants.FieldActivationCost / GlobalConstants.MonsterGold;
                        CurrentGatherer.Field.MetalCount = GlobalConstants.NewFieldMetalCount;
                        CurrentGatherer.Field.WoodCount = GlobalConstants.NewFieldWoodCount;
                        gatherers.Update(CurrentGatherer);
                        gatherers.SaveChanges();
                        activationState = 3;
                    }
                    else
                    {
                        activationState = 2;
                    }
                }
                else
                {
                    activationState = 1;
                }
            }
            return activationState;
        }
    }
}

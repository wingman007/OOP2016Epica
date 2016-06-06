namespace Epica.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        public Item()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public ItemType Type { get; set; }
        public ItemName Name { get; set; }
        public int Durability { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int WoodCraftSkill { get; set; }
        public int MetalCraftSkill { get; set; }
        public int WoodGatherSkill { get; set; }
        public int MetalGatherSkill { get; set; }
        public int Price { get; set; }
        public User Seller { get; set; }
        public Guid UserId { get; set; }
    }
}
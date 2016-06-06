namespace Epica.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Crafter
    {
        public Crafter()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public virtual Resources Resources { get; set; }
        [ForeignKey("Resources")]
        public Guid ResourcesId { get; set; }

        public virtual CrafterStats CrafterStats { get; set; }
        [ForeignKey("CrafterStats")]
        public Guid CrafterStatsId { get; set; }

        public virtual Equipment CrafterEquipment { get; set; }
        [ForeignKey("CrafterEquipment")]
        public Guid CrafterEquipmentId { get; set; }

        public virtual Inventory Inventory { get; set; }
        [ForeignKey("Inventory")]
        public Guid InventoryId { get; set; }

        public string CharacterName { get; set; }
        public int Level { get; set; }

        public virtual User User { get; set; }
        [Required, Key, ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}
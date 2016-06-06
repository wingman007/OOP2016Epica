namespace Epica.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Hero
    {
        public Hero()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public virtual Resources Resources { get; set; }
        public Guid ResourcesId { get; set; }

        public virtual HeroStats HeroStats { get; set; }
        public Guid HeroStatsId { get; set; }

        public virtual Equipment HeroEquipment { get; set; }
        public Guid HeroEquipmentId { get; set; }

        public virtual Inventory Inventory { get; set; }
        public Guid InventoryId { get; set; }

        public string CharacterName { get; set; }
        public int Level { get; set; }

        //Return to User
        public virtual User User { get; set; }
        [Key, ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}

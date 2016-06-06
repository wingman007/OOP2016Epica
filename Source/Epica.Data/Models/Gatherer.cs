namespace Epica.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Gatherer
    {
        public Gatherer()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public virtual Resources Resources { get; set; }
        public Guid ResourcesId { get; set; }

        public virtual GathererStats GatherStats { get; set; }
        public Guid GatherStatsId { get; set; }

        public virtual Equipment GatherEquipment { get; set; }
        public Guid GatherEquipmentId { get; set; }

        public virtual Inventory Inventory { get; set; }
        public Guid InventoryId { get; set; }

        public virtual Field Field { get; set; }
        public Guid FieldId { get; set; }

        public string CharacterName { get; set; }
        public int Level { get; set; }
        //public int Pesho { get; set; }
        public virtual User User { get; set; }
        [Required, Key, ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}

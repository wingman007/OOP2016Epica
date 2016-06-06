namespace Epica.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Field
    {
        public Field()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public int MonsterCount { get; set; }
        public int MetalCount { get; set; }
        public int WoodCount { get; set; }
        public bool IsUpForClearing { get; set; }
        public bool IsCleared { get; set; }

        public virtual Gatherer Gatherer { get; set; }
        [Key]
        [ForeignKey("Gatherer")]
        public Guid GathererId { get; set; }
    }
}

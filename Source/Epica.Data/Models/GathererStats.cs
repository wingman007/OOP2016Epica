namespace Epica.Data.Models
{
    using System;

    public class GathererStats
    {
        public GathererStats()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int WoodGatherSkill { get; set; }
        public int MetalGatherSkill { get; set; }
    }
}
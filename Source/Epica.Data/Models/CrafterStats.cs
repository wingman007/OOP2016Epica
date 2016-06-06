namespace Epica.Data.Models
{
    using System;

    public class CrafterStats
    {
        public CrafterStats()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public int WoodCraftSkill { get; set; }
        public int MetalCraftSkill { get; set; }
    }
}
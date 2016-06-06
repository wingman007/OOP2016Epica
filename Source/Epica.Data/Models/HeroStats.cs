namespace Epica.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class HeroStats
    {
        public HeroStats()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public double Health { get; set; }
        public int Armor { get; set; }
        public int Damage { get; set; }
    }
}
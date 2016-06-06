namespace Epica.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Market
    {
        private ICollection<Item> items;
        private ICollection<ResourcePack> resourcePacks;

        public Market()
        {
            this.Id = Guid.NewGuid();
            this.items = new HashSet<Item>();
            this.resourcePacks = new HashSet<ResourcePack>();
        }

        public Guid Id { get; set; }

        public virtual ICollection<Item> Item
        {
            get { return this.items;  }
            set { this.items = value; }
        }

        public virtual ICollection<ResourcePack> ResourcePacks
        {
            get { return this.resourcePacks; }
            set { this.resourcePacks = value; }
        }
    }
}

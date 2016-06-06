namespace Epica.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Resources
    {
        private ICollection<ResourcePack> resourcePacks;

        public Resources()
        {
            this.Id = Guid.NewGuid();
            this.resourcePacks = new HashSet<ResourcePack>();
        }

        public Guid Id { get; set; }

        public int Gold { get; set; }
        
        public virtual ICollection<ResourcePack> ResourcePacks
        {
            get { return this.resourcePacks; }
            set { this.resourcePacks = value; }
        }
    }
}

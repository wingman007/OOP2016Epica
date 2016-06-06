namespace Epica.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Inventory
    {
        private ICollection<Item> inventoryitems;
        private ICollection<Resources> inventoryresources;

        public Inventory()
        {
            this.Id = Guid.NewGuid();
            this.inventoryitems = new HashSet<Item>();
            this.inventoryresources = new HashSet<Resources>();
        }
        public Guid Id { get; set; }

        public virtual ICollection<Item> InventoryItems
        {
            get { return this.inventoryitems; }
            set { this.inventoryitems = value; }
        }
    }
}

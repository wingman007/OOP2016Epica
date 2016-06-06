namespace Epica.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Equipment
    {
        private ICollection<Item> equipeditems;

        public Equipment()
        {
            this.Id = Guid.NewGuid();
            this.equipeditems = new HashSet<Item>();
        }
        public Guid Id { get; set; }

        public virtual ICollection<Item> EquipedItems
        {
            get { return this.equipeditems; }
            set { this.equipeditems = value; }
        }
    }
}

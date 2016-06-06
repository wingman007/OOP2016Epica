namespace Epica.Data.Models
{
    using System;

    public class ResourcePack
    {
        public ResourcePack()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public ResourcePackType Type { get; set; }
        public int Price { get; set; }
        public User Seller { get; set; }
        public Guid UserId { get; set; }
    }
}

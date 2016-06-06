namespace Epica.Data
{
    using System.Data.Entity;
    using Epica.Data.Models;

    public class EpicaDbContext : DbContext, IEpicaDbContext
    {
        public EpicaDbContext()
            : base("Database")
        {
        }

        public virtual IDbSet<Crafter> Crafters { get; set; }
        public virtual IDbSet<CrafterStats> CrafterStats { get; set; }
        public virtual IDbSet<Equipment> Equipments { get; set; }
        public virtual IDbSet<Field> Fields { get; set; }
        public virtual IDbSet<Gatherer> Gatherers { get; set; }
        public virtual IDbSet<GathererStats> GathererStats { get; set; }
        public virtual IDbSet<Hero> Heroes { get; set; }
        public virtual IDbSet<HeroStats> HeroStats { get; set; }
        public virtual IDbSet<Inventory> Inventories { get; set; }
        public virtual IDbSet<Item> Items { get; set; }
        public virtual IDbSet<Market> Markets { get; set; }
        public virtual IDbSet<ResourcePack> ResourcePacks { get; set; }
        public virtual IDbSet<Resources> Resources { get; set; }
        public virtual IDbSet<User> Users { get; set; }

        public new IDbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public static EpicaDbContext Create()
        {
            return new EpicaDbContext();
        }
    }
}

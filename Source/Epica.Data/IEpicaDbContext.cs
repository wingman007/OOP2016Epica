namespace Epica.Data
{
    using Epica.Data.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IEpicaDbContext
    {
        IDbSet<Crafter> Crafters { get; set; }
        IDbSet<CrafterStats> CrafterStats { get; set; }
        IDbSet<Equipment> Equipments { get; set; }
        IDbSet<Field> Fields { get; set; }
        IDbSet<Gatherer> Gatherers { get; set; }
        IDbSet<GathererStats> GathererStats { get; set; }
        IDbSet<Hero> Heroes { get; set; }
        IDbSet<HeroStats> HeroStats { get; set; }
        IDbSet<Inventory> Inventories { get; set; }
        IDbSet<Item> Items { get; set; }
        IDbSet<Market> Markets { get; set; }
        IDbSet<ResourcePack> ResourcePacks { get; set; }
        IDbSet<Resources> Resources { get; set; }
        IDbSet<User> Users { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }
}

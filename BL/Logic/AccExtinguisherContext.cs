using System.Data.Entity;

namespace BL
{
    public class BLContext : DbContext
    {
        public BLContext() : base("AccExtinguisherConnection")
        { }

        public DbSet<Extinguisher> Extinguishers { get; set; }
        public DbSet<FireCabinet> FireCabinets { get; set; }
        public DbSet<Hose> Hoses { get; set; }
        public DbSet<Hydrant> Hydrants { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<StatusExtinguisher> StatusExtinguishers { get; set; }
        public DbSet<TypeExtinguisher> TypeExtinguishers { get; set; }
        public DbSet<TypeHose> TypeHoses { get; set; }
        public DbSet<TypeFireCabinet> TypeFireCabinets { get; set; }


        //static BLContext()
        //{
        //    Database.SetInitializer(new MyContextInitializer());
        //}
    }

    class MyContextInitializer : DropCreateDatabaseAlways<BLContext>
    {
        protected override void Seed(BLContext db)
        {
            db.SaveChanges();//может ли убрать
        }
    }
}

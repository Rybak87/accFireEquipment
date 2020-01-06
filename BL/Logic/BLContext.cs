using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Reflection;

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
        public DbSet<KindExtinguisher> KindExtinguishers { get; set; }
        public DbSet<KindHose> KindHoses { get; set; }
        public DbSet<KindFireCabinet> KindFireCabinets { get; set; }
        public DbSet<History> Histories { get; set; }
        //public DbSet<FireCabinetHistory> HistoriesFireCabinet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Location>()
            //    .HasMany(l => l.FireCabinets)
            //    .WithRequired(l => l.Location)
            //    .HasForeignKey(s => s.LocationId)
            //    .WillCascadeOnDelete(true);
            //    modelBuilder.Entity<FireCabinet>()
            //.HasMany(c => c.Extinguishers)
            //.WithRequired(o => o.FireCabinet)
            //.HasForeignKey(s => s.FireCabinetId)
            //.WillCascadeOnDelete(true);
            //modelBuilder.Entity<History>().HasAlternateKey(u => new { u.Type, u.Id });
        }
        static BLContext()
        {
            //Database.SetInitializer(new MyContextInitializer());
            Database.SetInitializer(new MyContextInitializer2());
            //Database.SetInitializer(new MyContextInitializer3());
        }
    }

    internal class MyContextInitializer : DropCreateDatabaseAlways<BLContext>
    {
        protected override void Seed(BLContext db)
        {
            InitDatabaseHelper.Seed(db);
        }
    }

    internal class MyContextInitializer2 : DropCreateDatabaseIfModelChanges<BLContext>
    {
        protected override void Seed(BLContext db)
        {
            InitDatabaseHelper.Seed(db);
        }
    }

    internal class MyContextInitializer3 : CreateDatabaseIfNotExists<BLContext>
    {
        protected override void Seed(BLContext db)
        {
            InitDatabaseHelper.Seed(db);
        }
    }

    public static class InitDatabaseHelper
    {
        public static void Seed(BLContext db)
        {
            var tf = InitDefaultTypes<KindFireCabinet>(Properties.TypesCSV.typesFireCabinet);
            db.KindFireCabinets.AddRange(tf);
            var te = InitDefaultTypes<KindExtinguisher>(Properties.TypesCSV.typesExtinguisher);
            db.KindExtinguishers.AddRange(te);
            var th = InitDefaultTypes<KindHose>(Properties.TypesCSV.typesHose);
            db.KindHoses.AddRange(th);

            Location l1 = new Location { Name = "Блок 1", Number = 1 };
            Location l2 = new Location { Name = "Блок 2", Number = 2 };
            Location l3 = new Location { Name = "Блок 3", Number = 3 };
            Location l4 = new Location { Name = "Блок 4", Number = 4 };
            db.Locations.Add(l1);
            db.Locations.Add(l2);
            db.Locations.Add(l3);
            db.Locations.Add(l4);

            FireCabinet f1 = new FireCabinet { Location = l1, Number = 1, KindFireCabinet = tf[0] };
            FireCabinet f2 = new FireCabinet { Location = l1, Number = 2, KindFireCabinet = tf[0] };
            FireCabinet f3 = new FireCabinet { Location = l2, Number = 1, KindFireCabinet = tf[0] };
            db.FireCabinets.Add(f1);
            db.FireCabinets.Add(f2);
            db.FireCabinets.Add(f3);

            Extinguisher e1 = new Extinguisher { FireCabinet = f1, Number = 1, KindExtinguisher = te[0] };
            Extinguisher e2 = new Extinguisher { FireCabinet = f2, Number = 1, KindExtinguisher = te[1] };
            db.Extinguishers.Add(e1);
            db.Extinguishers.Add(e2);

            Hose h1 = new Hose { FireCabinet = f1, Number = 1, KindHose = th[0] };
            db.Hoses.Add(h1);

            db.SaveChanges();
        }
        private static List<T> InitDefaultTypes<T>(byte[] fileBinary) where T : class, new()
        {
            Stream stream = new MemoryStream(fileBinary);
            var result = new List<T>();
            using (StreamReader sr = new StreamReader(stream, System.Text.Encoding.Default))
            {
                string line;
                T curr = null;
                string headersLine = sr.ReadLine();
                var headers = headersLine.Split(';');
                var properties = new List<PropertyInfo>();
                foreach (var head in headers)
                {
                    var property = typeof(T).GetProperty(head);
                    properties.Add(property);
                }
                while ((line = sr.ReadLine()) != null)
                {
                    curr = new T();
                    var values = line.Split(';');
                    for (int i = 0; i < properties.Count; i++)
                    {
                        if (properties[i].PropertyType == typeof(string))
                            properties[i].SetValue(curr, values[i]);
                        else if (properties[i].PropertyType == typeof(double))
                            properties[i].SetValue(curr, Double.Parse(values[i]));
                    }
                    result.Add(curr);
                }
            }
            return result;
        }
    }
}

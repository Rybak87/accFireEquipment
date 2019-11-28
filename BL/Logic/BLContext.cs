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
        public DbSet<TypeExtinguisher> TypeExtinguishers { get; set; }
        public DbSet<TypeHose> TypeHoses { get; set; }
        public DbSet<TypeFireCabinet> TypeFireCabinets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Location>()
            //    .HasMany(l => l.FireCabinets)
            //    .WithRequired(l => l.Location)
            //    .HasForeignKey(s => s.LocationId)
            //    .WillCascadeOnDelete(true);

            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ImageLocation>()
            //            .HasOptional(x=>)
            //            .WithOptional(x => x.Parent)
            //            .WillCascadeOnDelete(true);

        }
        static BLContext()
        {
            //Database.SetInitializer(new MyContextInitializer());
            Database.SetInitializer(new MyContextInitializer2());
            //Database.SetInitializer(new MyContextInitializer3());
        }
    }

    class MyContextInitializer : DropCreateDatabaseAlways<BLContext>
    {
        protected override void Seed(BLContext db)
        {
            InitDatabaseHelper.Seed(db);
        }
    }
    class MyContextInitializer2 : DropCreateDatabaseIfModelChanges<BLContext>
    {
        protected override void Seed(BLContext db)
        {
            InitDatabaseHelper.Seed(db);
        }
    }
    class MyContextInitializer3 : CreateDatabaseIfNotExists<BLContext>
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
            TypeFireCabinet tf1 = new TypeFireCabinet { Name = "ШКП" };
            db.TypeFireCabinets.Add(tf1);

            var te = InitDatabaseTypeExtinguishers(db);
            db.TypeExtinguishers.AddRange(te);


            TypeHose th1 = new TypeHose { Name = "РПК" };
            db.TypeHoses.Add(th1);

            Location l1 = new Location { Name = "Блок 1", Number = 1 };
            Location l2 = new Location { Name = "Блок 2", Number = 2 };
            Location l3 = new Location { Name = "Блок 3", Number = 3 };
            Location l4 = new Location { Name = "Блок 4", Number = 4 };
            db.Locations.Add(l1);
            db.Locations.Add(l2);
            db.Locations.Add(l3);
            db.Locations.Add(l4);

            FireCabinet f1 = new FireCabinet { Location = l1, Number = 1, TypeFireCabinet = tf1 };
            FireCabinet f2 = new FireCabinet { Location = l1, Number = 2, TypeFireCabinet = tf1 };
            FireCabinet f3 = new FireCabinet { Location = l2, Number = 1, TypeFireCabinet = tf1 };
            db.FireCabinets.Add(f1);
            db.FireCabinets.Add(f2);
            db.FireCabinets.Add(f3);

            Extinguisher e1 = new Extinguisher { FireCabinet = f1, Number = 1, TypeExtinguisher = te[0] };
            Extinguisher e2 = new Extinguisher { FireCabinet = f2, Number = 1, TypeExtinguisher = te[1] };
            db.Extinguishers.Add(e1);
            db.Extinguishers.Add(e2);

            Hose h1 = new Hose { FireCabinet = f1, Number = 1, TypeHose = th1 };
            db.Hoses.Add(h1);

            db.SaveChanges();
        }
        private static List<TypeExtinguisher> InitDatabaseTypeExtinguishers(BLContext db)
        {
            var result = new List<TypeExtinguisher>();
            using (StreamReader sr = new StreamReader("..\\typesExtinguishers.csv", System.Text.Encoding.Default))
            {
                string line;
                TypeExtinguisher curr = null;
                string headersLine = sr.ReadLine();
                var headers = headersLine.Split(';');
                var properties = new List<PropertyInfo>();
                foreach (var head in headers)
                {
                    var property = typeof(TypeExtinguisher).GetProperty(head);
                    properties.Add(property);
                }
                while ((line = sr.ReadLine()) != null)
                {
                    curr = new TypeExtinguisher();
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

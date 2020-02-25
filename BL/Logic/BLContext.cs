using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;

namespace BL
{
    /// <summary>
    /// Контекст.
    /// </summary>
    public class BLContext : DbContext
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public BLContext() : base("AccExtinguisherConnection")
        { }

        /// <summary>
        /// Статический конструктор.
        /// </summary>
        static BLContext()
        {
            Database.SetInitializer(new MyContextInitializer());
        }

        /// <summary>
        /// Пожарный инвентарь.
        /// </summary>
        public DbSet<Equipment> Equipments { get; set; }

        /// <summary>
        /// Огнетушители.
        /// </summary>
        public DbSet<Extinguisher> Extinguishers { get; set; }

        /// <summary>
        /// Пожарные шкафы.
        /// </summary>
        public DbSet<FireCabinet> FireCabinets { get; set; }

        /// <summary>
        /// Рукава.
        /// </summary>
        public DbSet<Hose> Hoses { get; set; }

        /// <summary>
        /// Пожарные краны.
        /// </summary>
        public DbSet<Hydrant> Hydrants { get; set; }

        /// <summary>
        /// Помещения.
        /// </summary>
        public DbSet<Location> Locations { get; set; }

        /// <summary>
        /// Виды огнетушителей.
        /// </summary>
        public DbSet<KindExtinguisher> KindExtinguishers { get; set; }

        /// <summary>
        /// Виды рукавов.
        /// </summary>
        public DbSet<KindHose> KindHoses { get; set; }

        /// <summary>
        /// Виды пожарных шкафов.
        /// </summary>
        public DbSet<KindFireCabinet> KindFireCabinets { get; set; }

        /// <summary>
        /// Изменения пожарного инвентаря.
        /// </summary>
        public DbSet<History> Histories { get; set; }

        /// <summary>
        /// Инициализатор связей, настроек БД.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>().HasOptional(h => h.PrevHistory).WithOptionalDependent().Map(m => m.MapKey("PrevHistoryId"));
        }
    }

    internal class MyContextInitializer : DropCreateDatabaseIfModelChanges<BLContext>
    {
        protected override void Seed(BLContext db)
        {
            InitDatabaseHelper.Seed(db);
        }
    }

    /// <summary>
    /// Инициализатор БД.
    /// </summary>
    public static class InitDatabaseHelper
    {
        /// <summary>
        /// Инициализатор БД.
        /// </summary>
        /// <param name="db">Контекст БД.</param>
        public static void Seed(BLContext db)
        {
            ///В связи с тем что EF6 не поддерживает создания каскадного удаления в ТРТ подходе
            ///внешние ключи обновляются посредством SQL команд.
            var commandDeleteForeignKey = @"ALTER TABLE {0}
                DROP CONSTRAINT ""FK_{0}_dbo.ПожарныеШкафы_FireCabinetId""; ";

            var commandAddForeignKey = @"ALTER TABLE {0}
                ADD CONSTRAINT ""FK_{0}_dbo.ПожарныеШкафы_FireCabinetId"" FOREIGN KEY(FireCabinetId)
                REFERENCES dbo.ПожарныеШкафы(Id)
                ON DELETE CASCADE;";

            db.Database.ExecuteSqlCommand(string.Format(commandDeleteForeignKey, "dbo.Огнетушители"));
            db.Database.ExecuteSqlCommand(string.Format(commandAddForeignKey, "dbo.Огнетушители"));

            db.Database.ExecuteSqlCommand(string.Format(commandDeleteForeignKey, "dbo.Рукава"));
            db.Database.ExecuteSqlCommand(string.Format(commandAddForeignKey, "dbo.Рукава"));

            db.Database.ExecuteSqlCommand(string.Format(commandDeleteForeignKey, "dbo.ПожарныеКраны"));
            db.Database.ExecuteSqlCommand(string.Format(commandAddForeignKey, "dbo.ПожарныеКраны"));

            var tf = InitDefaultTypes<KindFireCabinet>(Properties.TypesCSV.typesFireCabinet);
            db.KindFireCabinets.AddRange(tf);
            var te = InitDefaultTypes<KindExtinguisher>(Properties.TypesCSV.typesExtinguisher);
            db.KindExtinguishers.AddRange(te);
            var th = InitDefaultTypes<KindHose>(Properties.TypesCSV.typesHose);
            db.KindHoses.AddRange(th);

            db.SaveChanges();
        }

        /// <summary>
        /// Возвращает коллекцию видов пожарного инвентаря.
        /// </summary>
        /// <typeparam name="T">Вид пожарного инвентаря.</typeparam>
        /// <param name="fileBinary">Файл для чтения.</param>
        private static List<T> InitDefaultTypes<T>(byte[] fileBinary) where T : KindBase, new()
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

using System.Data.Entity;
using System.Data.SQLite;

namespace Grundfos.WG.PostCalc.SQLiteEf
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(string dataSource) :
            base(new SQLiteConnection() { ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = dataSource, ForeignKeys = true }.ConnectionString }, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Model.Result> Results { get; set; }
        public DbSet<Model.ResultTimestamp> ResultTimestamps { get; set; }
    }
}

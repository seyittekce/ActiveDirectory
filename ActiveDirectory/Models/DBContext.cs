using System.Configuration;
using System.Data.Common;
using System.Data.Entity;

namespace ActiveDirectory.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("ActiveDirectoryBaseApp")
        {
        }

        public DbSet<UserProperties> UserInformations { get; set; }
        public DbSet<StandartLog> StandartLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<DeletedPerson> DeletedPersons { get; set; }

        public static DbConnection GetConnection()
        {
            ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["SQLiteConnection"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connection.ProviderName);
            DbConnection dbCon = factory.CreateConnection();
            dbCon.ConnectionString = connection.ConnectionString;
            return dbCon;
        }
    }
}
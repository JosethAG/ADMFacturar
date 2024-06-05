using System.Data.SqlClient;

namespace ADM.API.Conn
{
    public class Connection
    {
        private string sql = string.Empty;

        public Connection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            sql = builder.GetSection("ConnectionStrings:Conn").Value;
        }

        public string GetConSQL()
        {
        return sql; 
        }

    }
}

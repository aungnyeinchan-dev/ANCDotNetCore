using System.Data.SqlClient;

namespace ANCDotNetCore.PizzaApi
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-GCNGMKN",
            InitialCatalog = "DotNetTraingBatch4",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true,
        };
    }
}

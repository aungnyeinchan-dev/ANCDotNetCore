﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANCDotNetCore.ConsoleApp.Services
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

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOIS.Shared.Core.Helper.Connections
{

    public interface IDatabaseConnectionFactory
    {
        IDbConnection createConnection(int dbIndex);
        IDbConnection createConnection(string conName);
        IDbConnection createConnectionAsync();
        void closeConnection(int dbIndex);
        void closeConnection(string conName);
    }
    public class SqlConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;
        private readonly string _connectionString2;
        private readonly string _connectionString3;
        private readonly IConfiguration _connectionConfig;
        private static readonly string Server = "Server=";
        private static readonly string DataBase = ";Database=";
        private static readonly string UserId = ";User ID=";
        private static readonly string Password = ";Password=";
        private static readonly string MinPoolSize = "; Min Pool Size=";
        private static readonly string MaxPoolSize = "; Max Pool Size=";
        private static readonly string Pooling = "; Pooling=";
        private static readonly string Timeout = ";Connection Timeout=";

        public SqlConnectionFactory(IConfiguration _connections)
        {
            _connectionConfig = _connections;
        }

        public IDbConnection createConnection(int dbIndex)
        {

            {
                //string test = _connectionString;
                // dbIndex == 1 (paiMGGIdentity) : 2 (PAI_RiskTarget) : 3 (PAI_RMCore)

                var conn = dbIndex switch
                {
                    1 => new SqlConnectionStringBuilder(_connectionString),
                    2 => new SqlConnectionStringBuilder(_connectionString2),
                    3 => new SqlConnectionStringBuilder(_connectionString3),
                    _ => throw new NotImplementedException()
                };
                string strUserId = conn.UserID;
                string strPassword = conn.Password;
                string strIP = conn.DataSource;
                string strDb = conn.InitialCatalog;
                int strMinPoolSize = conn.MinPoolSize;
                int strMaxPoolSize = conn.MaxPoolSize;
                bool strPool = conn.Pooling;
                int strTimeout = conn.ConnectTimeout;
                string result = Server + (strIP, "") + DataBase + (strDb, "") + UserId + (strUserId, "") + Password + (strPassword, "") + Timeout + strTimeout;// +MinPoolSize + "100" + "" + MaxPoolSize + "1000" + "" + Pooling + strPool + "";

                var sqlConnection = new SqlConnection(result);
                sqlConnection.Open();
                return sqlConnection;
            }

        }

        public IDbConnection createConnection(string conName)
        {
            // appsettings ကနေ connection string ကို ယူခြင်း
            var connectionStringValue = _connectionConfig.GetRequiredSection(conName).Value;

            // Builder ကို သုံးပြီး string ကို parse လုပ်ခြင်း
            var connBuilder = new SqlConnectionStringBuilder(connectionStringValue);

            // လိုအပ်တဲ့ property တွေကို builder ထဲမှာ တိုက်ရိုက်ပြင်ဆင်နိုင်ပါတယ်
            // manual string ပေါင်းစပ်ခြင်းထက် ဒါက ပိုစိတ်ချရပါတယ်
            connBuilder.TrustServerCertificate = true;

            // builder.ConnectionString က သတ်မှတ်ထားတဲ့ format အမှန်ကို ထုတ်ပေးပါလိမ့်မယ်
            var sqlConnection = new SqlConnection(connBuilder.ConnectionString);

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            return sqlConnection;
        }

        public IDbConnection createConnectionAsync()
        {
            {
                //string test = _connectionString;

                var conn = new SqlConnectionStringBuilder(_connectionString);
                string strUserId = conn.UserID;
                string strPassword = conn.Password;
                string strIP = conn.DataSource;
                string strDb = conn.InitialCatalog;
                int strMinPoolSize = conn.MinPoolSize;
                int strMaxPoolSize = conn.MaxPoolSize;
                bool strPool = conn.Pooling;
                string result = Server + (strIP, "") + DataBase + (strDb, "") + UserId + (strUserId, "") + Password + (strPassword, "") + MinPoolSize + strMinPoolSize + "" + MaxPoolSize + strMaxPoolSize + "" + Pooling + strPool + "";

                var sqlConnection = new SqlConnection(result);
                sqlConnection.OpenAsync();
                return sqlConnection;
            }

        }

        public void closeConnection(int dbIndex) => createConnection(dbIndex).Dispose();
        public void closeConnection(string conName) => createConnection(conName).Dispose();



    }
}

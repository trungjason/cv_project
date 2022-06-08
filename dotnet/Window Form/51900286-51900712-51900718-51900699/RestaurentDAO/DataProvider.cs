using System.Data;
using System.Data.SqlClient;

namespace RestaurentDAO
{
    public class DataProvider
    {
        private const string SQLConnectionString = "Data Source=ADMIN;Initial Catalog=quanlynhahang;Integrated Security=True";
        private SqlConnection sqlConnection;
        private SqlCommand command;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;

        private static DataProvider uniqueInstance;

        private DataProvider()
        {
            this.sqlConnection = new SqlConnection(SQLConnectionString);
        }

        // Singleton Pattern
        public static DataProvider GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new DataProvider();
            };

            return uniqueInstance;
        }
     
        private void Connect()
        {
            this.sqlConnection.Open();
        }

        private void Disconnect()
        {
            this.sqlConnection.Close();
        }

        public void CreateCommand(string cmd)
        {
            this.command = new SqlCommand(cmd, this.sqlConnection);
        }

        public void AddParams(string param, object value)
        {
            this.command.Parameters.AddWithValue(param, value);
        }

        public DataTable ExecuteQueryWithParams()
        {
            Connect();

            dataTable = new DataTable();
            this.sqlDataAdapter = new SqlDataAdapter(this.command);
            this.sqlDataAdapter.Fill(dataTable);

            this.command.Parameters.Clear();
            Disconnect();

            return dataTable;
        }

        public bool ExecuteNonQueryWithParams()
        {
            Connect();

            int row_Affected = this.command.ExecuteNonQuery();

            this.command.Parameters.Clear();
            Disconnect();

            return row_Affected > 0;
        }

        // There function should be use for SQL Command that don't include params
        public DataTable ExecuteQuery(string queryCommand)
        {
            Connect();

            this.command = new SqlCommand(queryCommand, this.sqlConnection);

            dataTable = new DataTable();
            this.sqlDataAdapter = new SqlDataAdapter(this.command);
            this.sqlDataAdapter.Fill(dataTable);

            Disconnect();

            return dataTable;
        }

        public bool ExecuteNonQuery(string queryCommand)
        {
            Connect();

            this.command = new SqlCommand(queryCommand, this.sqlConnection);
            int row_Affected = this.command.ExecuteNonQuery();
            Disconnect();

            return row_Affected > 0;
        }

        public object ExecuteScalar(string queryCommand)
        {
            Connect();

            this.command = new SqlCommand(queryCommand, this.sqlConnection);
            object result = this.command.ExecuteScalar();

            Disconnect();

            return result;
        }
    }
}

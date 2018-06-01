using System.Data.SqlClient;

namespace SqlConnector
{
    public class SqlServerConnector:Common
    {
        private SqlConnection cn = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        public void OpenConnection()
        {
            cn.ConnectionString = this.ConnectionString;
            cn.Open();
        }

        public void CloseConnection()
        {
            cn.Dispose();
            cn.Close();
        }

        public SqlServerConnector ExecQuery()
        {
            OpenConnection();

            cmd = new SqlCommand(Procedure, cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (var Item in Parameters)
            {
                cmd.Parameters.AddWithValue(Item.Key, Item.Value);
            }

            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //retornaremos finalmente 
            //DataSet ds = new DataSet();
            ds.Dispose();
            ds.Clear();
            adapter.Fill(ds);

            CloseConnection();
            return this;
        }

    }
}

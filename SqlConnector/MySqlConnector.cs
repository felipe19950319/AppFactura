using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SqlConnector
{
    public class MySqlConnector : Common
    {
        //private Common c = new Common();
        private MySqlConnection cn = new MySqlConnection();
        private MySqlCommand cmd = new MySqlCommand();
        // public string StoredProcedure = string.Empty;

        public void OpenConnection()
        {
            cn.ConnectionString = this.ConnectionString;
            cn.Open();
        }

        public void CloseConnection() {
            cn.Dispose();
            cn.Close();
        }

        public MySqlConnector ExecQuery()
        {
            OpenConnection();

            cmd = new MySqlCommand(Procedure, cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (var Item in Parameters)
            {
                cmd.Parameters.AddWithValue(Item.Key, Item.Value);
            }

            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable dtSchema = dr.GetSchemaTable();
            DataTable dt = new DataTable();
            // You can also use an ArrayList instead of List<> 
            List<DataColumn> listCols = new List<DataColumn>();
            if (dtSchema != null)
            {
                foreach (DataRow drow in dtSchema.Rows)
                {
                    string columnName = System.Convert.ToString(drow["ColumnName"]);
                    DataColumn column = new DataColumn(columnName, (Type)(drow["DataType"]));
                    column.Unique = (bool)drow["IsUnique"];
                    column.AllowDBNull = (bool)drow["AllowDBNull"];
                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
                    listCols.Add(column);
                    dt.Columns.Add(column);
                }

            }

            // Read rows from DataReader and populate the DataTable 
            while (dr.Read())
            {
                DataRow dataRow = dt.NewRow();
                for (int i = 0; i < listCols.Count; i++)
                {
                    dataRow[((DataColumn)listCols[i])] = dr[i];
                }

                dt.Rows.Add(dataRow);
            }

           /*ds.Clear();
            ds.Dispose();*/
            ds.Tables.Clear();
            ds.Tables.Add(dt);
 

            Parameters.Clear();
            CloseConnection();
            return this;
        }

    }

}

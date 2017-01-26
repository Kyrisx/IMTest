using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace IMtest
{
    static class DAL
    {
        private static string _CHEMX_USER = "";

        public static byte UserLogin(clsLogin credentials)
        {
            MySqlConnection conn = AppUserConnect();

            string sql = @"
Select UserId
From lbim.users
Where UserName = @UserName
    And pw = @Password";

            MySqlCommand cmd = new MySqlCommand(sql , conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserName", credentials.UserName);
            cmd.Parameters.AddWithValue("@Password", credentials.Password);

            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.FillSchema(dt, SchemaType.Mapped);
            adapter.Fill(dt);

            conn.Close();

            if (dt.Rows.Count == 1)
            {
                return (byte)dt.Rows[0][0];
            }

            return 0;    
        }

        public static void UserLoginUpdate(int UserId)
        {
            MySqlConnection conn = AppUserConnect();

            string sql = @"
Update lbim.users
Set firstlogin = Case When firstlogin Is Null Then NOW() Else firstlogin End,
lastlogin = NOW()
Where UserId = @UserId";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.ExecuteNonQuery();

            conn.Close();
            return;
        }

        public static DataSet LoadUser(int UserId, int stages)
        {
            MySqlConnection conn = AppUserConnect();
            DataSet ds = new DataSet();

            if (stages <= 1) getContacts(conn, UserId, ds);

            conn.Close();
            return ds;               
        }

        private static void getContacts(MySqlConnection conn, int UserId, DataSet ds)
        {
            DataTable dt = new DataTable();
            string sql = @"
Select cl.ContactId, u.UserName, u.FirstName, u.LastName
From lbim.contactlist cl
Join lbim.users u On cl.ContactId = u.UserId
Where cl.UserId = @UserId
    And DateAdded Is Not Null";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserId", UserId);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.FillSchema(dt, SchemaType.Mapped);
            adapter.Fill(dt);

            ds.Tables.Add(dt);

            return;
        }

        private static MySqlConnection AppUserConnect()
        {
            MySqlConnectionStringBuilder connStr = new MySqlConnectionStringBuilder();
            connStr.Server = "chemicalx.ddns.net";
            connStr.Database = "lbim";
            connStr.UserID = "appuser";
            connStr.Password = "Ge7JdHmQ3r";
            connStr.ConnectionTimeout = 30;
            connStr.PersistSecurityInfo = false;

            MySqlConnection conn = new MySqlConnection(connStr.ConnectionString);

            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }

            return conn;
        }
    }
}

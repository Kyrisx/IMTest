using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;

namespace IMtest
{
    static class DAL
    {
        private static string _CHEMX_USER = "";

        public static byte UserLogin(string UserName, string Password)
        {
            MySqlConnection conn = AppUserConnect();

            string sql = @"
Select UserId
From lbim.users
Where UserName = @UserName
    And pw = @Password";
      
            MySqlCommand cmd = new MySqlCommand(sql , conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);

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

        public static DataTable GetContacts(int UserId)
        {
            MySqlConnection conn = AppUserConnect();

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

            conn.Close();

            return dt;             
        }

        public static byte GetUserId(string UserName)
        {
            MySqlConnection conn = AppUserConnect();

            string sql = @"
Select UserId
From lbim.users
Where UserName = @UserName";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserName", UserName);
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.FillSchema(dt, SchemaType.Mapped);
            adapter.Fill(dt);

            conn.Close();

            if (dt.Rows.Count == 1)
            {
                return (byte)dt.Rows[0][0];
            }

            return 0;
        }

        private static bool AddContacts(int UserId, string ContactId)
        {
            MySqlConnection conn = AppUserConnect();

            Tuple<bool, bool> result = ExistingContactRequest(conn, UserId, ContactId);

            //DataTable dt = new DataTable();
            string sql = @"
Select cl.ContactId
From lbim.contactlist cl
Join lbim.users u On cl.ContactId = u.UserId
    And
Where (cl.UserId = @UserId
    And DateAdded Is Null";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserId", UserId);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.FillSchema(dt, SchemaType.Mapped);
            adapter.Fill(dt);

            ds.Tables.Add(dt);

            return;
        }

        private static Tuple<bool, bool> ExistingContactRequest(MySqlConnection Conn, int UserId, string ContactId)
        {
            string sql = @"
Select cl.DateAdded
From lbim.contactlist cl
Where (cl.UserId = @UserId And cl.ContactId = @ContactId)
    Or (cl.UserId = @ContactId And cl.ContactId = @UserId)";

            MySqlCommand cmd = new MySqlCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ContactId", ContactId);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.FillSchema(dt, SchemaType.Mapped);
            adapter.Fill(dt);

            // Request already exists and has been accepted
            if (dt.Rows.Count == 2)
            {
                return new Tuple<bool, bool>(true, true);
            }
            // Request already exists and has not be accepted
            else if (dt.Rows.Count == 1)
            {
                return new Tuple<bool, bool>(true, false);
            }
            // Request does not exists and has not been accepted
            else if (dt.Rows.Count == 0)
            {
                return new Tuple<bool, bool>(false, false);
            }
            // Failsafe: Theoretically unreachable
            else
            {
                Console.WriteLine("ExistingContactRequest has detected more requests than acceptable. Verify database integrity.");
                return new Tuple<bool, bool>(true, true);
            }
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

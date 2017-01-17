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

        public static bool UserLogin(clsLogin credentials)
        {
            MySqlConnection conn = AppUserConnect();
            MySqlCommand cmd = new MySqlCommand("Select UserId From lbim.users Where UserName = @UserName And pw = @Password", conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserName", credentials.UserName);
            cmd.Parameters.AddWithValue("@Password", credentials.Password);

            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            conn.Close();

            if (dt.Rows.Count < 1)
            {
                return false;
            }

            return true;    
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

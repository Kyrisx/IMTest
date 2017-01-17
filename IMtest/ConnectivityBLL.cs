using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using System.Windows.Forms;

namespace IMtest
{
    class ConnectivityBLL
    {
        private clsLogin _credentials;

        public ConnectivityBLL(clsLogin value)
        {
            _credentials = value;
        }

        public void UserLogin()
        {
            if (_credentials.UserNameLength == 0 || _credentials.UserNameLength > 20)
            {
                throw new LoginException(0);
            }

            if (_credentials.PasswordLength == 0 || _credentials.PasswordLength > 30)
            {
                throw new LoginException(1);
            }

            if (!DAL.UserLogin(_credentials))
            {
                throw new LoginException(2);
            }

            //return 99;
        }
    }
}

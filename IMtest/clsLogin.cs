using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMtest
{
    class clsLogin
    {
        private string _username;
        private string _password;

        public clsLogin(string UserName, string Password)
        {
            _username = UserName;
            _password = Password;
        }

        public string UserName
        {
            set
            {
                _username = value;
            }

            get
            {
                return _username;
            }
        }

        public string Password
        {
            set
            {
                _password = value;
            }
            
            get
            {
                return _password;
            }
        }

        public int UserNameLength
        {
            get
            {
                return UserName.Length;
            }
        }

        public int PasswordLength
        {
            get
            {
                return Password.Length;
            }
        }
    }
}

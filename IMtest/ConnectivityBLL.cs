

namespace IMtest
{
    class ConnectivityBLL
    {
        private clsLogin _credentials;

        public ConnectivityBLL(clsLogin value)
        {
            _credentials = value;
        }

        public int UserLogin()
        {
            if (_credentials.UserNameLength == 0 || _credentials.UserNameLength > 20)
            {
                throw new LoginException(0);
            }

            if (_credentials.PasswordLength == 0 || _credentials.PasswordLength > 30)
            {
                throw new LoginException(1);
            }

            int UserId = DAL.UserLogin(_credentials);
            if (UserId == 0)
            {
                throw new LoginException(2);
            }
            else
            {
                DAL.UserLoginUpdate(UserId);
                return UserId;
            }

            //return 99;
        }
    }
}

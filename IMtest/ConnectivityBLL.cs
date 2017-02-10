

namespace IMtest
{
    class ConnectivityBLL
    {
        private clsUser _credentials;

        public ConnectivityBLL(clsUser value)
        {
            _credentials = value;
        }

        public int UserLogin()
        {
            if (_credentials.UserName.Length == 0 || _credentials.UserName.Length > 20)
            {
                throw new UserException(0);
            }

            if (_credentials.Password.Length == 0 || _credentials.Password.Length > 30)
            {
                throw new UserException(1);
            }

            _credentials.Id = DAL.UserLogin(_credentials.UserName, _credentials.Password);
            if (_credentials.Id == 0)
            {
                throw new UserException(2);
            }
            else
            {
                DAL.UserLoginUpdate(_credentials.Id);
                return _credentials.Id;
            }

            //return 99;
        }
    }
}

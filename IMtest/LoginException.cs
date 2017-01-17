using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IMtest
{
    [Serializable]
    public class LoginException : Exception
    {
        /*  0: Invalid User Name length
            1: Invalid Password length
            2: Invalid User Name or Password    */
            
              
        private int _number;

        public LoginException() : base()
        {
            
        }

        public LoginException(int number)
        {
            _number = number;

        }

        public LoginException(string message) : base(message)
        {
            
        }

        public LoginException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public LoginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public int Number
        {
            get
            {
                return _number;
            }
        }
    }
}

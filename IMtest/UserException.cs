using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IMtest
{
    [Serializable]
    public class UserException : Exception
    {
        /*  0: Invalid User Name length
            1: Invalid Password length
            2: Invalid User Name or Password    */
            
              
        private int _number;

        public UserException() : base()
        {
            
        }

        public UserException(int number)
        {
            _number = number;

        }

        public UserException(string message) : base(message)
        {
            
        }

        public UserException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public UserException(SerializationInfo info, StreamingContext context) : base(info, context)
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

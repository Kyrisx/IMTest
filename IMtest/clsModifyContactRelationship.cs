using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMtest
{
    public class clsModifyContactRelationship
    {
        private int _clientId;
        private string _clientUserName;
        private int _contactId;
        private string _contactUserName;

        public clsModifyContactRelationship()
        {
            _clientId = 0;
            _clientUserName = "";
            _contactId = 0;
            _contactUserName = "";
        }

        public int ClientId
        {
            set
            {
                _clientId = value;
            }
            get
            {
                return _clientId;
            }
        }

        public string ClientUserName
        {
            set
            {
                _clientUserName = value;
            }
            get
            {
                return _clientUserName;
            }
        }

        public int ContactId
        {
            set
            {
                _contactId = value;
            }
            get
            {
                return _contactId;
            }
        }

        public string ContactUserName
        {
            set
            {
                _contactUserName = value;
            }
            get
            {
                return _contactUserName;
            }
        }

    }
}


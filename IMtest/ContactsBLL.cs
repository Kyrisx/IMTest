using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IMtest
{
    public class ContactsBLL
    {
        private int _userId;

        public ContactsBLL(int UserId)
        {
            _userId = UserId;
        }

        public void LoadContacts(List <clsUser> Cb)
        {
            DataTable dt = DAL.GetContacts(_userId);

            foreach (DataRow row in dt.Rows)
            {
                clsUser temp = new clsUser((byte)row[0], row[1].ToString(), row[2].ToString(), row[3].ToString());
                Cb.Add(temp);
            }
            
            return;
        }

        public bool AddContact(clsUser Contact)
        {
            if (Contact.UserName.Length == 0 || Contact.UserName.Length > 20)
            {
                throw new UserException(0);
            }

            int ContactUserId = DAL.GetUserId(Contact.UserName);
            if (ContactUserId > 0)
            {

            }
        }
    }
}

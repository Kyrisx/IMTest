using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IMtest
{
    public class LoadUserBLL
    {
        private int _UserId;

        public LoadUserBLL(int UserId)
        {
            _UserId = UserId;
        }
        public void LoadUserContacts(List <clsContactBasic> cb)
        {
            DataSet ds = DAL.LoadUser(_UserId, 1);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                clsContactBasic temp = new clsContactBasic((byte)row[0], row[1].ToString(), row[2].ToString(), row[3].ToString());
                cb.Add(temp);
            }
            
            return;
        }
    }
}

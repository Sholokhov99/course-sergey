using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SecurityModule;

namespace Course.Models.Data.Temp
{
    class UserData
    {
        public static String Login { get; set; }
        public static String Password { get; set; }
        public static String Question { get; set; }
        public static String Answer { get; set; }
        public static Int32 Access { get; set; }

        public static void SetUserData(List<Byte[]> data)
        {
            Login = Encoding.UTF8.GetString(data[0]);
            Password = Encryption.Decryption(data[1]);
            Question = Encoding.UTF8.GetString(data[2]);
            Answer = Encoding.UTF8.GetString(data[3]);
            Access = Convert.ToInt32(Encoding.UTF8.GetString(data[4]));
        }

        public static void ClearData()
        {
            Login = null;
            Password = null;
            Question = null;
            Answer = null;
            Access = -1;
        }

        public static Boolean Authenication(String login, String password)
        {
            SQLiteQuery.SQL.Select select = new SQLiteQuery.SQL.Select();
            var result = select.Authenication(login, password);
            if (result.Count == 1)
            {
                SetUserData(result[0]);
                return true;
            }
            else
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SecurityModule;

namespace SQLiteQuery
{
    public class Config
    {
        private static String _path { get; } = @".\\database.db";
        private static String _password { get; } = ";Password=Lfybbk123ru";
        private static Byte _version { get; } = 3;
        private static String _connect = $"Data Source={_path}{_password}";

        private static String _closeState { get; } = "Closed";
        private static String _openState { get; } = "Open";


        private static SQLiteConnection _connectLocalDB
        {
            get { return ConnectLocalDB; }
            set
            {
                ConnectLocalDB = value;
            }
        }

        public static SQLiteConnection ConnectLocalDB { get; private set; } = null;

        //
        //  Подключение к БД
        //
        public static Boolean Connect()
        {
            if (File.Exists(_path))
            {
                if (_connectLocalDB == null || _connectLocalDB.State.ToString() == _closeState)
                {

                    _connectLocalDB = new SQLiteConnection(_connect);
                    _connectLocalDB.Open();
                    return true;
                }

                return false;
            }
            else
            {
                Backup.ErrorDatabase(_path);
                return false;
            }
        }

        public static void CreateBackupDB()
        {
            Backup.Start(_path);
        }

        //
        //  Отключение от БД
        //
        public static Boolean Disconnect()
        {
            if (_connectLocalDB != null && _connectLocalDB.State.ToString() == _openState)
            {
                _connectLocalDB.Close();
                _connectLocalDB = null;
            }

            return true;
        }

        public static void Error(Exception ex)
        {
            Config.Disconnect();
            Backup.ErrorDatabase(_path);
            Logs.SetError(ex);
        }

    }
}

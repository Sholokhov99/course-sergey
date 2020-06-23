using System;
using System.IO;
using System.Threading;

namespace SecurityModule
{
    public class Backup
    {
        private static String _pathTemp = @".\\data\temp\";
        private static string _pathBackup = @".\\data\backup";
        private static Int32 _interval { get; } = 600000;
        private static String _pathDb;

        public static Boolean IsStart { get; private set; }
        private static Boolean _isStart
        {
            get { return IsStart; }
            set
            {
                IsStart = value;
            }
        }

        private static Byte[] _backup;

        //
        //  Генерация ключа
        //
        private static string GenerateName(string name)
        {
            Random rand = new Random();
            string str = "";

            string date = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            string time = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

            for (int index = 0; index < 6; index++)
            {
                char letter = (char)rand.Next(48, 57);
                str += letter;
            }

            str += $"_{date}_{time}_{name}";

            return str;
        }

        //
        //  Копирование БД
        //
        public static void Start(String path)
        {
            if (!_isStart)
            {
                _pathDb = path;
                new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            // Файл базы данных отсутствует
                            if (!File.Exists(path))
                                CreateDatabase(path);
                            else
                            {
                                
                                using (FileStream fileStream = File.OpenRead(path))
                                {
                                    _backup = new byte[fileStream.Length];
                                    fileStream.Read(_backup, 0, _backup.Length);
                                }
                                

                                if (Directory.Exists(_pathBackup + @"/db/") == false)
                                    Directory.CreateDirectory(_pathBackup + @"/db/");
                                File.WriteAllBytes($"{_pathBackup}/db/{GenerateName("db")}.backup",  _backup);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logs.SetError(ex);
                        }
                        Thread.Sleep(_interval);
                    }
                })
                {
                    IsBackground = true,
                    Priority = ThreadPriority.AboveNormal
                }.Start();
            }
        }

        //
        //  Создание БД
        //
        private static Int16 CreateDatabase(String path)
        {
            /*
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    if (_backup != null && _backup.Length > 0)
                    {
                        File.Delete(path);
                        File.WriteAllBytes(path, _backup);
                        return 1;
                    }
                    else
                    {
                        throw new Exception("Резервная копия базы данных отсутствует");
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.SetError(ex);
                return -1;
            }*/
            return 1;
        }

        public static Boolean ErrorDatabase(String path)
        {
            try
            {
                return (CreateDatabase(path) == 1) ? true : false;

            }
            catch (Exception ex)
            {
                Logs.SetError(ex);
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityModule
{
    public class Logs
    {
        private static String _pathErrorLog { get; } = @".\\error.log";

        public static void SetError(Exception ex)
        {
            using (FileStream file = new FileStream(_pathErrorLog, FileMode.Append, FileAccess.Write))
            {
                String date = DateTime.Now.ToLongDateString();
                String time = DateTime.Now.ToShortTimeString();

                // Время ошибки
                String upLine = $"\nDate: {date} {time}\n";

                var underLine = GenerateUnderLine();

                string error = $"{upLine} {ex.Message}\n{ex.TargetSite}\n{ex.StackTrace}{underLine}\n";

                // Преобразование строки в байты
                byte[] array = Encoding.Default.GetBytes(error);
                // Запись массива байтов в файл
                file.Write(array, 0, array.Length);
                file.Close();
            }
        }

        private static String GenerateUnderLine()
        {
            String underLine = "\n";

            for (Int32 index = 0; index < 60; index++)
                underLine += '-';

            return underLine;
        }
    }
}

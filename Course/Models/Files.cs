using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Models
{
    public class Files
    {

        //
        //  Создание файла
        //
        public static void CreateFile(String path, String text)
        {

            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate))
            {
                // Преобразование строки в байты
                byte[] array = Encoding.Default.GetBytes(text);
                // Запись массива байтов в файл
                file.Write(array, 0, array.Length);
                file.Close();
            }
        }
        //
        //  Создание папки
        //
        public static void CrateFolder(String path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                dir.Create();
            }
        }
        //
        //  Преобразование файла в массив байтов
        //
        public static Byte[] GetBytesFromFile(String path)
        {
            Byte[] bytes = File.ReadAllBytes(path);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
                str += Convert.ToString(bytes[i], 2);

            return bytes; 
        }
    }
}

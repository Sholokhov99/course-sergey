using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SecurityModule
{
    public class Encryption
    {
        private static Byte[] _key { get; } = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        #region Шифрование
        //
        //  Шифрование
        //
        public static byte[] Encrypt(string value) =>
            SourceEncrypt(value);


        //
        //  Логика шифрования
        //
        private static byte[] SourceEncrypt(string str)
        {
            try
            {
                //Объявляем объект класса AES
                Aes aes = Aes.Create();
                //Генерируем соль
                aes.GenerateIV();
                //Присваиваем ключ. aeskey - переменная (массив байт), сгенерированная методом GenerateKey() класса AES
                aes.Key = _key;
                byte[] encrypted;
                ICryptoTransform crypt = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(str);
                        }
                    }
                    //Записываем в переменную encrypted зашиврованный поток байтов
                    encrypted = ms.ToArray();

                    var bytes = encrypted.Concat(aes.IV).ToArray();
                    return bytes;
                }
            }
            catch (Exception ex)
            {
                Logs.SetError(ex);
                return new byte[] { };
            }
        }
        #endregion

        //
        //  Дешифрование
        //
        public static string Decryption(byte[] shifr)
        {
            try
            {
                GC.Collect();
                Byte[] bytesIv = new byte[16];
                Byte[] mess = new byte[shifr.Length - 16];
                Byte[] bytes = shifr;


                //Списываем соль
                for (int i = shifr.Length - 16, j = 0; i < shifr.Length; i++, j++)
                    bytesIv[j] = bytes[i];
                //Списываем оставшуюся часть сообщения
                for (int i = 0; i < shifr.Length - 16; i++)
                    mess[i] = bytes[i];
                //Объект класса Aes
                Aes aes = Aes.Create();
                //Задаем тот же ключ, что и для шифрования
                aes.Key = _key;
                //Задаем соль
                aes.IV = bytesIv;
                //Строковая переменная для результата
                string text = "";
                byte[] data = mess;
                ICryptoTransform crypt = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            //Результат записываем в переменную text в вие исходной строки
                            text = sr.ReadToEnd();
                        }
                    }
                }
                return text;
                //return buffer;
            }
            catch (Exception ex)
            {
                Logs.SetError(ex);
                return string.Empty;
            }
        }

    }
}

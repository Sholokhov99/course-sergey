using SecurityModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteQuery.SQL
{
    public class Select
    {
        //
        //  Функция Select sql запроса
        //
        public List<List<String>> Post(String sql)
        {
            // Создание массива в который будут занесены данные

           try
            {
                List<List<String>> data = new List<List<string>>();
                Config.Connect();
                using (SQLiteCommand qLiteCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    // Отправка запроса к БД
                    using (SQLiteDataReader qLiteDataReader = qLiteCommand.ExecuteReader())
                    {

                        // Проверка на удачный запрос (бд вернула данные)
                        if (qLiteDataReader.HasRows)
                        {
                            // Сдвиг на следующую строку (первая пустая)
                            while (qLiteDataReader.Read())
                            {

                                List<String> temp = new List<string>();
                                for (Int32 column = 0; column < qLiteDataReader.FieldCount; column++)
                                {
                                    temp.Add(qLiteDataReader[column].ToString());
                                }
                                data.Add(temp);
                            }
                        }
                        Config.Disconnect();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                Config.Error(ex);
                return new List<List<String>> { };
            }
        }

        public List<List<Byte[]>> PostUsers(String sql)
        {
            try
            {
                List<List<Byte[]>> data = new List<List<Byte[]>>();
                Config.Connect();
                using (SQLiteCommand qLiteCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    // Отправка запроса к БД
                    using (SQLiteDataReader qLiteDataReader = qLiteCommand.ExecuteReader())
                    {
                        // Проверка на удачный запрос (бд вернула данные)
                        if (qLiteDataReader.HasRows)
                        {
                            // Сдвиг на следующую строку (первая пустая)
                            while (qLiteDataReader.Read())
                            {

                                List<Byte[]> temp = new List<Byte[]>();
                                for (Int32 column = 0; column < qLiteDataReader.FieldCount; column++)
                                {
                                    try
                                    {
                                        temp.Add(new MemoryStream((Byte[])qLiteDataReader[column]).ToArray());
                                    }
                                    catch
                                    {
                                        temp.Add(Encoding.UTF8.GetBytes(qLiteDataReader[column].ToString()));
                                    }
                                }
                                data.Add(temp);
                            }
                        }
                        Config.Disconnect();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                Config.Error(ex);
                return new List<List<Byte[]>> { };
            }
        }

        //
        //  Восстановление пароля
        //
        public List<List<String>> GetQuestion(String login) => Post($"SELECT Question, Answer FROM Users WHERE Login='{login}'");

        //
        // Поиск пользователя по логину
        //
        public Boolean SelectUser(String login)
        {
            var result = Post($"SELECT * FROM Users WHERE Login='{login}';");

            if (result.Count == 1)
            {
                if (result[0].Count != 0) return true;
                return false;
            }
            return false;

            //return (result.Count == 1) ? true : false;
        }

        //
        //  Авторизация пользователя
        //
        public List<List<Byte[]>> Authenication(String login, String password)
        {
            var result = PostUsers($"SELECT * FROM Users WHERE Login='{login}';");

            if (result.Count > 0 && Encryption.Decryption(result[0][1]) == password)
                return result;
            else
                return new List<List<Byte[]>> { };
        }

        //
        //  Получение всех пользователей
        //
        public List<List<byte[]>> GetUsers()
        {
            var result = PostUsers("SELECT * FROM Users");
            List<List<string>> data = new List<List<string>>();

            for(int row = 0; row < result.Count; row++)
            {
                List<string> temp = new List<string>();
                for (int column = 0; column < result.Count; column++)
                {
                    string str;

                    str = (column == 1) ? Encryption.Decryption(result[row][column]) : Encoding.UTF8.GetString(result[row][column]);

                    temp.Add(str);
                }
            }

            return result;
        }

        //
        //  Загрузка глав
        //
        public List<List<String>> GetChapters()
        {
            String sql = "SELECT * FROM Chapters ORDER BY Number ASC;";
            return Post(sql);
        }

        //
        //  Получение главы
        //
        public List<List<String>> GetChapter(Int32 id) => Post($"SELECT Chapters.Title FROM Chapters LEFT JOIN Topics ON Chapters.Id = Topics.Id_chapter WHERE Topics.Id = '{id}';");

        //
        //  Загрузка тем
        //
        public List<List<String>> GetTopics(Int32 idChapter = -1)
        {

            String sql = $"SELECT * FROM Topics WHERE Id_chapter='{idChapter}' ORDER BY Position ASC;";
            return Post(sql);
        }
        //
        //  Получение слайдов
        //
        public List<List<byte[]>> GetSlides(Int32 IdTopic = -1)
        {
            String sql = (IdTopic == -1) ? "SELECT * FROM Slides ORDER BY Number_slide ASC;" :
                $"SELECT * FROM Slides WHERE Id_topic='{IdTopic}' ORDER BY Number_slide ASC;";


            /*try
{*/
            List<List<Byte[]>> data = new List<List<Byte[]>>();
            Config.Connect();
            using (SQLiteCommand qLiteCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
            {
                // Отправка запроса к БД
                using (SQLiteDataReader qLiteDataReader = qLiteCommand.ExecuteReader())
                {
                    // Проверка на удачный запрос (бд вернула данные)
                    if (qLiteDataReader.HasRows)
                    {
                        // Сдвиг на следующую строку (первая пустая)
                        while (qLiteDataReader.Read())
                        {

                            List<Byte[]> temp = new List<Byte[]>();
                            for (Int32 column = 0; column < qLiteDataReader.FieldCount; column++)
                            {
                                try
                                {
                                    temp.Add(new MemoryStream((Byte[])qLiteDataReader[column]).ToArray());
                                }
                                catch
                                {
                                    temp.Add(Encoding.UTF8.GetBytes(qLiteDataReader[column].ToString()));
                                }
                            }
                            data.Add(temp);
                        }
                    }
                    Config.Disconnect();
                    return data;
                }
            }
            /*}
            catch (Exception ex)
            {
                Config.Error(ex);
                return new List<List<Byte[]>> { };
            }*/
        }

        //
        //  Загрузка меню
        //
        public List<List<String>> LoadMenu()
        {
            String sql = "SELECT Chapters.Id, Chapters.Title, Chapters.Number," +
                            " Topics.Id, Topics.Id_chapter, Topics.Name, Topics.Position FROM" +
                            " Chapters LEFT JOIN Topics ON" +
                            " Chapters.Id = Topics.Id_chapter" +
                            " ORDER BY Chapters.Number ASC;";
            return Post(sql);
        }

        //
        //  Загрузка слайда
        //
        public BindingList<List<Byte[]>> LoadSlides(Int32 topicId)
        {
            try
            {
                BindingList<List<Byte[]>> data = new BindingList<List<Byte[]>>();
                Config.Connect();
                using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM Slides WHERE Id_topic={topicId} ORDER BY Number_slide ASC", Config.ConnectLocalDB))
                {
                    using (SQLiteDataReader qLiteDataReader = command.ExecuteReader())
                    {
                        if (qLiteDataReader.HasRows)
                        {
                            while (qLiteDataReader.Read())
                            {
                                List<Byte[]> temp = new List<Byte[]>();
                                for (Int32 column = 0; column < qLiteDataReader.FieldCount; column++)
                                {
                                    if (column == 2)
                                    {
                                        temp.Add((byte[])qLiteDataReader[column]);
                                    }
                                    else
                                    {
                                        temp.Add(Encoding.UTF8.GetBytes(qLiteDataReader[column].ToString()));
                                    }
                                }
                                data.Add(temp);
                            }
                        }
                        Config.Disconnect();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                Config.Error(ex);
                return new BindingList<List<Byte[]>> { };
            }
        }
        //
        //  Получение контента
        //
        public Byte[] GetContent(Int32 id)
        {
            try
            {
                Config.Connect();
                String sql = $"SELECT Content FROM Slides WHERE Id='{id}';";
                using (SQLiteCommand qLiteCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    using (SQLiteDataReader qLiteDataReader = qLiteCommand.ExecuteReader())
                    {
                        qLiteDataReader.Read();
                        var result = (byte[])qLiteDataReader[0];
                        Config.Disconnect();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Config.Error(ex);
                return new byte[] { };
            }
        }
    }
}

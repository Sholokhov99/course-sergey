using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using SecurityModule;

namespace SQLiteQuery.SQL
{
    public class Insert
    {
        public Int32 Post(String sql)
        {
            try
            {
                Config.Connect();
                Int32 codeOperation = -1;
                using (SQLiteCommand qLiteCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    codeOperation = qLiteCommand.ExecuteNonQuery();
                }
                Config.Disconnect();
                return codeOperation;
            }
            catch (Exception ex)
            {
                Config.Error(ex);
                return -1;
            }
        }

        //
        //  Создание нового слайда
        //
        public Int32 InsertNewSlide(Int32 idTopic, Byte[] content, Int32 numberSlide)
        {
            Select select = new Select();
            Update update = new Update();

            // Поиск всех слайдов относящихся к данной теме
            String sql = $"SELECT {Tables.Slides.Id}, {Tables.Slides.Position} FROM {Tables.Slides.TableName()} WHERE {Tables.Slides.Position} >= '{numberSlide}' AND {Tables.Slides.IdTopic}='{idTopic}';";
            var data = select.Post(sql);
            select = null;
            sql = String.Empty;


            // Сдвиг всех слайдов на 1
            foreach (List<String> value in data)
            {
                sql += $"UPDATE {Tables.Slides.TableName()} SET {Tables.Slides.Position} = '{Convert.ToInt32(value[1]) + 1}' WHERE {Tables.Slides.Id}='{value[0]}';";
            }
            update.Post(sql);
            update = null;

            // Добавление новго слайда в базу данных
            data.Clear();
            try
            {
                sql = $"INSERT INTO Slides ('{Tables.Slides.IdTopic}', '{Tables.Slides.Content}', '{Tables.Slides.Position}') VALUES (@id,@content, @position);";
                Config.Connect();
                using (SQLiteCommand command = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    command.Parameters.Add("@id", DbType.Int32, 255).Value = idTopic;
                    command.Parameters.Add("@content", DbType.Binary, 255).Value = content;
                    command.Parameters.Add("@position", DbType.Int32, 255).Value = numberSlide;

                    int code = command.ExecuteNonQuery();
                    Config.Disconnect();
                    return code;
                }
            }
            catch (Exception ex)
            {
                Config.Disconnect();
                Config.Error(ex);
                return -1;
            }
        }

        //
        //  Создание новой темы
        //
        public Int32 InsertNewTopic(Int32 idChapter, String name, Int32 position)
        {
            Select select = new Select();
            Update update = new Update();

            String sql = $"SELECT {Tables.Topics.Id}, {Tables.Topics.Position} FROM {Tables.Topics.TableName()} WHERE {Tables.Topics.Position} >= '{position}' AND {Tables.Topics.IdChapter} = '{idChapter}';";
            var data = select.Post(sql);

            select = null;

            sql = String.Empty;
            foreach (List<String> value in data)
            {
                sql += $"UPDATE {Tables.Topics.TableName()} SET {Tables.Topics.Position} = '{Convert.ToInt32(value[1]) + 1}' WHERE {Tables.Topics.Id}='{value[0]}';";
            }
            update.Post(sql);

            update = null;

            data.Clear();

            sql = $"INSERT INTO {Tables.Topics.TableName()} ('{Tables.Topics.IdChapter}', '{Tables.Topics.Name}', '{Tables.Topics.Position}') VALUES" +
                $"('{idChapter}','{name}','{position}');";
            return Post(sql);
        }

        //
        //  Создание новой главы
        //
        public Int32 InsertNewChapter(String title, Int32 number)
        {
            Select select = new Select();
            Update update = new Update();

            // Получить все главы равные и больше numberSlide и изменяются на 1 шаг вперед
            String sql = $"SELECT {Tables.Chapters.Id}, {Tables.Chapters.Position} FROM {Tables.Chapters.TableName()} WHERE {Tables.Chapters.Position} >= {number};";
            var data = select.Post(sql);
            select = null;


            sql = String.Empty;
            foreach (List<String> value in data)
            {
                sql += $"UPDATE {Tables.Chapters.TableName()} SET {Tables.Chapters.Position} = '{Convert.ToInt32(value[1]) + 1}' WHERE {Tables.Chapters.Id}='{value[0]}';";
            }
            update.Post(sql);
            update = null;


            data.Clear();
            sql = $"INSERT INTO {Tables.Chapters.TableName()}('{Tables.Chapters.Title}','{Tables.Chapters.Position}') VALUES ('{title}','{number}');";
            return Post(sql);
        }
        //
        //  Создание нового пользователя
        //
        public Int32 InsertNewUser(String login, String password, String question, String answer, Int32 access)
        {
            try
            {
                var pass = Encryption.Encrypt(password);
                String sql = "INSERT INTO Users ('Login','Password','Question','Answer','Access') VALUES (@login, @password, @Question, @Answer, @Access)";

                Config.Connect();
                using (SQLiteCommand qLiteCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    qLiteCommand.Parameters.Add("@login", DbType.String, 255).Value = login;
                    qLiteCommand.Parameters.Add("@password", DbType.Binary, 255).Value = pass;
                    qLiteCommand.Parameters.Add("@Question", DbType.String, 255).Value = question;
                    qLiteCommand.Parameters.Add("@Answer", DbType.String, 255).Value = answer;
                    qLiteCommand.Parameters.Add("@Access", DbType.Int32, 255).Value = access;

                    var result = qLiteCommand.ExecuteNonQuery();
                    Config.Disconnect();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Config.Error(ex);
                return -1;
            }
        }
    }
}

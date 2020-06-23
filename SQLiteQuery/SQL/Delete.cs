using SecurityModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace SQLiteQuery.SQL
{
    public class Delete
    {
        private Int32 Post(String sql)
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
        //  Удаление темы
        //
        public Int32 DeleteChapter(int id, int position)
        {
            Select select = new Select();
            Update update = new Update();

            // Загрузка всех глав, которые имеют позицию больше
            string sql = $"SELECT {Tables.Chapters.Id}, {Tables.Chapters.Position} FROM {Tables.Chapters.TableName()} WHERE {Tables.Chapters.Position} > '{position}';";
            var data = select.Post(sql);
            sql = string.Empty;

            if (data.Count > 0)
            {
                // Обновление всех позиций кроме нашей
                foreach (List<string> value in data)
                {
                    sql += $"UPDATE {Tables.Chapters.TableName()} SET {Tables.Chapters.Position} ='{Convert.ToInt32(value[1]) - 1}' WHERE {Tables.Chapters.Id}='{value[0]}';";
                }
                update.Post(sql);
            }

            // Удаление необходимой позиции
            select = null;
            update = null;
            return Post($"DELETE FROM {Tables.Chapters.TableName()} WHERE {Tables.Chapters.Id}='{id}';");
        }

        //
        //  Удаление темы
        //
        public Int32 DeleteTopic(int id, int position, int idChapter)
        {
            Select select = new Select();
            Update update = new Update();

            //string sql = $"SELECT"

            // Загрузка всех глав, которые имеют позицию больше
            string sql = $"SELECT {Tables.Topics.Id}, {Tables.Topics.Position} FROM {Tables.Topics.TableName()} WHERE {Tables.Topics.Position} > '{position}' AND {Tables.Topics.IdChapter}='{idChapter}';";
            var data = select.Post(sql);
            sql = string.Empty;

            if (data.Count > 0)
            {
                // Обновление всех позиций кроме нашей
                foreach (List<string> value in data)
                {
                    sql += $"UPDATE {Tables.Topics.TableName()} SET {Tables.Topics.Position}='{Convert.ToInt32(data[1]) - 1}' WHERE  {Tables.Topics.Id}='{value[0]}';";
                }
                update.Post(sql);
            }

            // Удаление необходимой позиции
            return Post($"DELETE FROM {Tables.Topics.TableName()} WHERE {Tables.Topics.Id}='{id}';");
        }

        //
        //  Удачение слайда
        //
        public Int32 DeleteSlide(int id, int position, int idTopic)
        {
            Select select = new Select();
            Update update = new Update();

            string sql = $"SELECT {Tables.Slides.Id}, {Tables.Slides.Position} FROM {Tables.Slides.TableName()} WHERE {Tables.Slides.Position} > '{position}' AND {Tables.Slides.IdTopic}='{idTopic}';";
            var data = select.Post(sql);
            sql = string.Empty;
            
            if(data.Count > 0)
            {
                foreach (List<string> value in data)
                {
                    sql += $"UPDATE {Tables.Slides.TableName()} SET {Tables.Slides.Position}='{Convert.ToInt32(value[1]) - 1}' WHERE {Tables.Slides.Id}='{value[0]}';";          
                }
                update.Post(sql);
            }

            return Post($"DELETE FROM {Tables.Slides.TableName()} WHERE {Tables.Slides.Id}='{id}';");
        }

    }
}

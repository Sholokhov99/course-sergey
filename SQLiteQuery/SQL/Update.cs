using SecurityModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteQuery.SQL
{
    public class Update
    {
        public Int32 Post(String sql)
        {
            try
            {
                Int32 codeOperation = -1;
                Config.Connect();
                using (SQLiteCommand sqlCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    codeOperation = sqlCommand.ExecuteNonQuery();
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

        public Int32 UpdateSlide(Int32 idSlide, Int32 idTopic, Byte[] content, Int32 numberSlide)
        {
            String sql = "UPDATE Slides SET Id_topic=@idTopic, Content=@content, Number_slide=@position WHERE Id=@id;";
            try
            {
                Config.Connect();
                using (SQLiteCommand qLiteCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    qLiteCommand.Parameters.Add("@idTopic", DbType.Int32, 255).Value = idTopic;
                    qLiteCommand.Parameters.Add("@content", DbType.Binary, 255).Value = content;
                    qLiteCommand.Parameters.Add("@position", DbType.Int32, 255).Value = numberSlide;
                    qLiteCommand.Parameters.Add("@id", DbType.Int32, 255).Value = idSlide;

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

        public Int32 UpdatePassword(String login, String newPassword)
        {
            try
            {
                String sql = "UPDATE Users SET Password=@newPassword WHERE Login=@login";

                var pass = Encryption.Encrypt(newPassword);

                Config.Connect();
                using (SQLiteCommand qLiteCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    qLiteCommand.Parameters.Add("@login", DbType.String, 255).Value = login;
                    qLiteCommand.Parameters.Add("@newPassword", DbType.Binary, 255).Value = pass;

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

        public Int32 UpdateDataUser(String login, String oldLogin, String password, String question, String answer, Int32 access)
        {
            try
            {
                Config.Connect();
                var pass = Encryption.Encrypt(password);

                string sql = "UPDATE Users SET Login=@newlogin, Password=@password, Question=@question, Answer=@answer, Access=@access WHERE Login=@oldlogin";
                using (SQLiteCommand qLiteCommand = new SQLiteCommand(sql, Config.ConnectLocalDB))
                {
                    qLiteCommand.Parameters.Add("@newlogin", DbType.String, 255).Value = login;
                    qLiteCommand.Parameters.Add("@oldlogin", DbType.String, 255).Value = oldLogin;
                    qLiteCommand.Parameters.Add("@password", DbType.Binary, 255).Value = pass;
                    qLiteCommand.Parameters.Add("@question", DbType.String, 255).Value = question;
                    qLiteCommand.Parameters.Add("@answer", DbType.String, 255).Value = answer;
                    qLiteCommand.Parameters.Add("@access", DbType.Int32, 255).Value = access;

                    var result = qLiteCommand.ExecuteNonQuery();
                    Config.Disconnect();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logs.SetError(ex);
                return -1;
            }
        }
    }
}

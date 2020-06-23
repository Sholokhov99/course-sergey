using System;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;

namespace SQLiteQueryProcessing
{
    public class SQL
    {
        #region Variables
        private const String _path = ".\\Data/database.db";
        private const String _password = "";
        private const Byte _version = 3;

        private Byte _maxLengthCell { get; } = 255;

        // private String _connect { get; } = $"Data Source={_path};Version={_version};Password={_password}";
        private String _connect = $"Data Source={_path};Version={_version};";

        private SQLiteConnection _connectLocalDB;
        #endregion

        //
        //  Подключение к БД
        //
        public Boolean Connect()
        {
            try
            {
                _connectLocalDB = new SQLiteConnection(_connect);
                _connectLocalDB.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //
        //  Отключение от БД
        //
        public Boolean Disconnect()
        {
            try
            {
                _connectLocalDB.Close();
                _connectLocalDB = null;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region SELECT
        //
        //  Функция Select sql запроса
        //
        public List<String> Select(SQLiteCommand qLiteCommand)
        {
            // Создание массива в который будут занесены данные
            List<String> data = new List<string>();

            try
            {
                Connect();
                // Отправка запроса к БД
                using (SQLiteDataReader qLiteDataReader = qLiteCommand.ExecuteReader())
                {
                    // Проверка на удачный запрос (бд вернула данные)
                    if(qLiteDataReader.HasRows)
                    {
                        // Сдвиг на следующую строку (первая пустая)
                        qLiteDataReader.Read();

                        // Считываем все данные
                        for(Int32 index = 0; index < qLiteDataReader.FieldCount; index++)
                        {
                            // Заносим данные в массив
                            data.Add(qLiteDataReader[index].ToString());
                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                // Error
                return new List<String> { };
            }
        }

        //
        //  Авторизация пользователя
        //
        public List<String> Authenication(String login, String password)
        {
            String sql = "SELECT * FROM Users WHERE Login=@login AND Password=@password";
            using(SQLiteCommand qLiteCommand = new SQLiteCommand(sql, _connectLocalDB))
            {
                // Подстановка данных в sql
                qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthCell).Value = login;
                qLiteCommand.Parameters.Add("@password", DbType.String, _maxLengthCell).Value = password;

                return Select(qLiteCommand);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Course.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        public Boolean newUser = false;
        public CreateUser()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text != String.Empty && password.Password != String.Empty && question.Text != String.Empty && answer.Password != String.Empty)
            {
                // Проверка логина на уникальность
                SQLiteQuery.SQL.Insert insertSQL = new SQLiteQuery.SQL.Insert();
                SQLiteQuery.SQL.Select selectSQL = new SQLiteQuery.SQL.Select();


                if (!selectSQL.SelectUser(login.Text))
                {
                    // Создание пользователя
                    var result = insertSQL.InsertNewUser(login.Text, password.Password, question.Text, answer.Password, access.SelectedIndex);
                    if (result != 0)
                    {
                        newUser = true;
                        Views.Alert.Create.SuccessfullyUser();
                    }
                    else
                    {
                        Views.Alert.Create.SuccessfullyUser();
                    }

                }
                else
                {
                    Alert("Пользователь с таким логином существует");
                }
            }
        }

        private void Alert(String text)
        {
            errorLabel.Visibility = Visibility.Visible;
            errorLabel.Content = text;
        }
    }
}

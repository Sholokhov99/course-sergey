using SQLiteQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Course.Models.Data.Temp;
using SecurityModule;
using SQLiteQuery.SQL;

namespace Course.Views.UsersControls
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccount.xaml
    /// </summary>
    public partial class PersonalAccount : UserControl
    {
        //private List<List<String>> _dataUsers = new List<List<string>>();
        private List<PersonalsAccount> _personalsAccounts = new List<PersonalsAccount>();
        private List<String> _tempLogin = new List<string>();
        public PersonalAccount()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserData.Login != String.Empty)
            {
                //  Настройка возможностей пользователя
                if (UserData.Access == 0)
                    UserAccess();
                else
                    AdminAccess();
            }
        }

        public void LoadData()
        {
            login.Text = Models.Data.Temp.UserData.Login;
            passwordPassBox.Password = Models.Data.Temp.UserData.Password;
            passwordTxtBox.Text = Models.Data.Temp.UserData.Password;
            secretQuestion.Text = Models.Data.Temp.UserData.Question;
            answerQuestionPassBox.Password = Models.Data.Temp.UserData.Answer;
            answerQuestionTxtBox.Text = Models.Data.Temp.UserData.Answer;
            accessTxtBox.Text = Models.Data.Temp.UserData.Access.ToString();
            if (Models.Data.Temp.UserData.Access == 1)
                AdminAccess();
            else
                UserAccess();
        }

        //
        //  Личный кабинет пользователя
        //
        public void UserAccess()
        {
            login.IsEnabled = false;
            accessTxtBox.IsEnabled = false;
            driveAccounts.Visibility = Visibility.Collapsed;
            driveProgram.Visibility = Visibility.Collapsed;
            SaveDriveAccount.Visibility = Visibility.Collapsed;
        }

        //
        //  Личный кабинет администратора
        //
        public void AdminAccess()
        {
            login.IsEnabled = true;
            accessTxtBox.IsEnabled = true;
            driveAccounts.Visibility = Visibility.Visible;
            driveProgram.Visibility = Visibility.Visible;
            SaveDriveAccount.Visibility = Visibility.Visible;

            // Загрузка всех аккаунтов
            CreateDriveUsers();
        }

        //
        //  Отображение зарегистрированных пользователей
        //
        private async void CreateDriveUsers()
        {
            _personalsAccounts.Clear();
            wrapPanel.Children.Clear();

            // Тут запрос
            SQLiteQuery.SQL.Select sql = new SQLiteQuery.SQL.Select();
            var data = sql.GetUsers();
            for (Int32 index = 0; index < data.Count; index++)
            {
                _personalsAccounts.Add(new PersonalsAccount
                {
                    Login = Encoding.UTF8.GetString(data[index][0]),
                    Password = Encryption.Decryption(data[index][1]),
                    Question = Encoding.UTF8.GetString(data[index][2]),
                    Answer = Encoding.UTF8.GetString(data[index][3]),
                    Access = Encoding.UTF8.GetString(data[index][4]),
                });

                _tempLogin.Add(_personalsAccounts.Last().Login);


                //  grid
                //
                Grid grid = new Grid();
                grid.Margin = new Thickness(10);
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                //
                // rectangle
                //
                Rectangle rectangle = new Rectangle()
                {
                    Stroke = (Brush)this.FindResource("DefaultColor"),
                    RadiusX = 7,
                    RadiusY = 7,
                    Width = 300,
                    MinHeight = 200,
                };
                Grid.SetRowSpan(rectangle, 5);
                Grid.SetColumnSpan(rectangle, 2);
                //
                //  labelLogin
                //
                var labelLogin = await CreateLabel("Логин:");
                labelLogin.Tag = index;
                Grid.SetRow(labelLogin, 0);
                Grid.SetColumn(labelLogin, 0);
                //
                //  loginTxtBox
                //
                var loginTxtBox = await CreateTextBox(_personalsAccounts[index].Login, "PA_textBox");
                loginTxtBox.Tag = index + "_0";
                Grid.SetRow(loginTxtBox, 0);
                Grid.SetColumn(loginTxtBox, 1);
                //
                //  labelPass
                //
                var labelPass = await CreateLabel("Пароль:");
                labelPass.Tag = index + "_1";
                Grid.SetRow(labelPass, 1);
                Grid.SetColumn(labelPass, 0);
                //
                //  passwordBoxPass
                //
                PasswordBox passwordBoxPass = new PasswordBox()
                {
                    Password = _personalsAccounts[index].Password,
                    Style = (Style)this.FindResource("PA_passwordBox"),
                    Tag = index + "_1",
                };
                passwordBoxPass.PasswordChanged += (s, e) => { SetDataUser(passwordBoxPass.Tag.ToString(), passwordBoxPass.Password); };
                Grid.SetRow(passwordBoxPass, 1);
                Grid.SetColumn(passwordBoxPass, 1);
                //
                //  questionLabel
                //
                var questionLabel = await CreateLabel("Секретный вопрос:");
                questionLabel.Tag = index + "_2";
                Grid.SetRow(questionLabel, 2);
                Grid.SetColumn(questionLabel, 0);
                //
                //  questionTxtBox
                //
                var questionTxtBox = await CreateTextBox(_personalsAccounts[index].Question, "PA_textBox");
                questionTxtBox.Tag = index + "_2";
                Grid.SetRow(questionTxtBox, 2);
                Grid.SetColumn(questionTxtBox, 1);
                //
                //  answerLabel
                //
                var answerLabel = await CreateLabel("Ответ на вопрос:");
                answerLabel.Tag = index + "_3";
                Grid.SetRow(answerLabel, 3);
                Grid.SetColumn(answerLabel, 0);
                //
                //  answerTxtBox
                //
                PasswordBox answerPassBox = new PasswordBox()
                {
                    Password = _personalsAccounts[index].Password,
                    Style = (Style)this.FindResource("PA_passwordBox"),
                    Tag = index + "_1",
                };
                answerPassBox.PasswordChanged += (s, e) => { SetDataUser(answerPassBox.Tag.ToString(), answerPassBox.Password); };
                answerPassBox.Tag = index + "_3";
                Grid.SetRow(answerPassBox, 3);
                Grid.SetColumn(answerPassBox, 1);
                //
                //  accessLabel
                //
                var accessLabel = await CreateLabel("Права доступа:");
                accessLabel.Tag = index + "_4";
                Grid.SetRow(accessLabel, 4);
                Grid.SetColumn(accessLabel, 0);
                //
                //  accessTxtBox
                //
                var accessTxtBox = await CreateTextBox(_personalsAccounts[index].Access, "PA_textBox");
                accessTxtBox.Tag = index + "_4";
                Grid.SetRow(accessTxtBox, 4);
                Grid.SetColumn(accessTxtBox, 1);


                grid.Children.Add(rectangle);

                grid.Children.Add(labelLogin);
                grid.Children.Add(loginTxtBox);

                grid.Children.Add(labelPass);
                grid.Children.Add(passwordBoxPass);

                grid.Children.Add(questionLabel);
                grid.Children.Add(questionTxtBox);

                grid.Children.Add(answerLabel);
                grid.Children.Add(answerPassBox);

                grid.Children.Add(accessLabel);
                grid.Children.Add(accessTxtBox);

                wrapPanel.Children.Add(grid);

            }

        }
        //
        //  Регулярные выражения
        //
        private String Reg(String text, Boolean positionInArrayUsers)
        {
            String temp = "";
            Regex regex = new Regex((positionInArrayUsers) ? @"_([0-9]+)" : @"([0-9]+)_");
            return regex.Replace(text, temp);
        }

        private async Task<Label> CreateLabel(String text) => new Label() { Content = text, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
        private async Task<TextBox> CreateTextBox(String text, String style)
        {
            TextBox textBox = new TextBox()
            {
                Text = text,
                Style = (Style)this.FindResource(style),
            };
            textBox.TextChanged += (s, e) =>
            {
                TextBox textbox = (TextBox)s;

                SetDataUser(textbox.Tag.ToString(), textbox.Text);

            };
            return textBox;
        }

        private void SetDataUser(String tag, String content)
        {
            // Номер коллекции в массиве
            Int32 position = Convert.ToInt32(Reg(tag, true));

            // номер элемента в коллекции
            Int32 numberElement = Convert.ToInt32(Reg(tag, false));

            switch (numberElement)
            {
                case 0:
                    _personalsAccounts[position].Login = content;
                    break;
                case 1:
                    _personalsAccounts[position].Password = content;
                    break;
                case 2:
                    _personalsAccounts[position].Question = content;
                    break;
                case 3:
                    _personalsAccounts[position].Answer = content;
                    break;
                case 4:
                    _personalsAccounts[position].Access = content;
                    break;
            }


        }



        #region Click change

        //
        //  Создание нового пароля
        //
        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateUser createUser = new CreateUser();
            createUser.ShowDialog();

            wrapPanel.Children.Clear();
            CreateDriveUsers();

        }

        //
        //  Показать пароль
        //
        private void EyePassword_Click(object sender, RoutedEventArgs e) => ChangeVisible(passwordTxtBox, passwordPassBox);

        private void ChangeVisible(TextBox textbox, PasswordBox passwordBox)
        {
            if (passwordBox.Visibility == Visibility.Hidden)
            {
                passwordBox.Visibility = Visibility.Visible;
                textbox.Visibility = Visibility.Hidden;
            }
            else
            {
                passwordBox.Visibility = Visibility.Hidden;
                textbox.Visibility = Visibility.Visible;

            }
        }

        //
        //  Показать ответ на вопрос
        //
        private void EyeSecretQuestion_Click(object sender, RoutedEventArgs e) => ChangeVisible(answerQuestionTxtBox, answerQuestionPassBox);

        //
        //  Создание новой главы
        //
        private void NewChapter_Click(object sender, RoutedEventArgs e)
        {
            CreateChapter createChapter = new CreateChapter(true);
            createChapter.ShowDialog();
            createChapter = null;
        }

        //
        //  Создание новой темы
        //
        private void NewTopic_Click(object sender, RoutedEventArgs e)
        {
            CreateTopic createTopic = new CreateTopic(true);
            createTopic.ShowDialog();
            createTopic = null;
        }

        //
        //  Создание нового слайда
        //
        private void NewSlide_Click(object sender, RoutedEventArgs e)
        {
            Course.Views.NewSlide newSlide = new Views.NewSlide(true);
            newSlide.ShowDialog();
            newSlide = null;
        }

        //
        //  Редактирование слайда
        //
        private void EditSlide_Click(object sender, RoutedEventArgs e)
        {
            Course.Views.NewSlide newSlide = new Views.NewSlide(false);
            newSlide.ShowDialog();
            newSlide = null;
        }
        #endregion

        private void UnSign_Click(object sender, RoutedEventArgs e)
        {
            Models.Data.Temp.UserData.ClearData();
            wrapPanel.Children.Clear();
            UserAccess();
            this.Visibility = Visibility.Hidden;
        }

        //
        //  Сохранение изменений пользователей
        //
        private void SaveDriveAccount_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {
                    Update sql = new Update();
                    Int32 temp;
                    for (Int32 index = 0; index < _personalsAccounts.Count; index++)
                    {
                        if (Int32.TryParse(_personalsAccounts[index].Access, out temp))
                        {
                            var result = sql.UpdateDataUser(_personalsAccounts[index].Login, _tempLogin[index],
                                _personalsAccounts[index].Password, _personalsAccounts[index].Question,
                                _personalsAccounts[index].Answer, Convert.ToInt32(_personalsAccounts[index].Access));
                            if (result == 0)
                            {
                                Alert.Save.UserIsNotSavingErrRequest(_personalsAccounts[index].Login);
                            }
                        }
                        else
                        {
                            Alert.Save.UserIsNotSavingErrLetters(_personalsAccounts[index].Login);
                        }
                    }
                }));
            })
            { IsBackground = true }.Start();

        }

        private void DelChapter_Click(object sender, RoutedEventArgs e)
        {
            CreateChapter createChapter = new CreateChapter(false);
            createChapter.ShowDialog();
            createChapter = null;

        }

        private void DelTopic_Click(object sender, RoutedEventArgs e)
        {
            CreateTopic createTopic = new CreateTopic(false);
            createTopic.ShowDialog();
            createTopic = null;
        }

        private void DelSlide_Click(object sender, RoutedEventArgs e)
        {
            DeleteSlide slide = new DeleteSlide();
            slide.ShowDialog();
            slide = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Update update = new Update();
            Int32 temp;
            if (login.Text != string.Empty
                && passwordPassBox.Password != string.Empty
                && secretQuestion.Text != string.Empty
                && answerQuestionPassBox.Password != string.Empty
                && accessTxtBox.Text != string.Empty)
            {
                if (int.TryParse(accessTxtBox.Text, out temp))
                {
                    var result = update.UpdateDataUser(login.Text, UserData.Login, passwordPassBox.Password, secretQuestion.Text, answerQuestionPassBox.Password, Convert.ToInt32(accessTxtBox.Text));

                    if (result > 0)
                        Alert.Save.UserData();
                    else
                        Alert.Save.UserIsNotSavingErrRequest(login.Text);
                }
                else
                {
                    Alert.Save.UserIsNotSavingErrLetters(login.Text);
                }
            }
            else
            {
                Alert.Save.ErrNotFullData();
            }
        }
    }
}

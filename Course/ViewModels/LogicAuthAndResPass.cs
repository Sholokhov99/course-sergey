using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Security;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Course.ViewModels
{
    class LogicAuthAndResPass : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChamged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #region Variables

        #region Globals
        public Boolean ClearPassBox = false;
        private String _uriVideo;
        public String UriVideo
        {
            get { return _uriVideo; }
            set
            {
                _uriVideo = value;
                OnPropertyChamged();
            }
        }

        //
        //  Видимость ошибки
        //
        public Boolean DisplayError
        {
            get { return _displayError; }
            set
            {
                _displayError = value;
                OnPropertyChamged();
            }
        }

        // Видимость формы смены пароля
        public Boolean ResetGrid
        { 
            get { return _visibilityResetGrid; }
            set
            {
                _visibilityResetGrid = value;
                OnPropertyChamged();
            }
        }

        struct ResetTemp
        {
            public String Login;
            public String AnswerUser;
            public String Password;
        }
        ResetTemp temp = new ResetTemp();

        #endregion


        #region Authenication

        #region private
        // Видимость формы авторизации
        private Boolean _visibilityAuthenication;

        // Текст ошибки
        private String _errorText;

        // Видимость ошибки
        private Boolean _displayError;

        // Логин пользователя
        private String _login;

        private bool canExecute = true;
        #endregion

        #region public

        // Видимость формы авторизации
        public Boolean VisibilityAuthenication
        {
            get { return _visibilityAuthenication; }
            set
            {
                _visibilityAuthenication = value;
                OnPropertyChamged();
            }
        }

        //
        //  Пользователь авторизирован
        //
        public Boolean IsAuth { get; set; } = false;

        //  Текст ошибки
        public String ErrorText
        {
            get { return _errorText;  }
            set
            {
                _errorText = value;
                OnPropertyChamged();
            }
        }

        //  Логин пользователя
        public String Login
        { 
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChamged();
            }
        }

        //  Кнопка авторизации
        public ICommand SignBtn { get; set; }

        //  Кнопка смены пароля
        public ICommand ResetPasswordBtn { get; set; }

        //  Пароль пользователя
        public SecureString SecurePassword { private get; set; }

        //  Обработка кнопки
        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
        }
        #endregion

        #endregion

        #region ResetPassword

        #region private
        private Boolean _visibilityAnswer;
        private Boolean _visibilitiResPassBox;

        private Boolean _visibilitiAdditional;

        // Тег формы ввода
        private String _tagAnswer;

        // Видимость формы смены пароля
        private Boolean _visibilityResetGrid;

        // Текст подсказка
        private String _additional;

        // Поле ввода
        private String _answer;

        // Шаг смены пароля
        private short _step = 0;

        #endregion

        #region public
        public Boolean VisibilityAnswer
        {
            get { return _visibilityAnswer; }
            set
            {
                _visibilityAnswer = value;
                OnPropertyChamged();
            }
        }
        public Boolean VisibilitiResPassBox
        {
            get { return _visibilitiResPassBox; }
            set
            {
                _visibilitiResPassBox = value;
                OnPropertyChamged();
            }
        }

        public Boolean VisibilitiAdditional
        {
            get { return _visibilitiAdditional; }
            set
            {
                _visibilitiAdditional = value;
                OnPropertyChamged();
            }
        }

        // Tag формы ввода
        public String TagAnswer 
        {
            get { return _tagAnswer; }
            set 
            {
                _tagAnswer = value;
                OnPropertyChamged();
            }
        }

        // Пароль пользователя
        public SecureString SecurePasswordResetPassword { private get; set; }

        // Кнопка следующий шаг смены пароля 
        public ICommand NextStep { get; set; }

        // Текст подсказка
        public String Additional
        {
            get { return _additional; }
            set 
            {
                _additional = value;
                OnPropertyChamged();
            }
        }

        // Видимость формы восстановления пароля
        public Boolean VisibilityResetGrid
        {
            get { return _visibilityResetGrid; }
            set
            {
                _visibilityResetGrid = value;
                OnPropertyChamged();
            }
        }

        // Поле ввода
        public String Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChamged();
            }
        }
        #endregion

        #endregion


        #endregion

        //
        //  Настройка формы
        //
        public LogicAuthAndResPass()
        {
            VisibilityAuthenication = true;
            VisibilityResetGrid = false;
            DisplayError = false;

            VisibilityAnswer = true;
            VisibilitiResPassBox = false;

            SignBtn = new Commands.Authenication.Authenication(Authenication, param => this.canExecute);
            ResetPasswordBtn = new Commands.Authenication.Authenication(ResetPassword, param => this.canExecute);
            NextStep = new Commands.Authenication.Authenication(PasswordRecoverySteps, param => this.canExecute);

            UriVideo = "pack://siteoforigin:,,,/Resources/backgroundauth.mp4";
        }

        //
        //  Авторизация
        //
        private void Authenication(Object obj)
        {
            String password = GetPassword(SecurePassword);
            if (_login.Length > 0 && password.Length > 0)
            {
                var result = Models.Data.Temp.UserData.Authenication(_login, password);
                if (!result) Alert("Пользователь не найден");
                else
                {
                    DisplayError = false;
                    IsAuth = true;
                }
            }
            else
            {
                Alert();
            }
        }

        //
        //  Смена пароля
        //
        private void ResetPassword(Object obj)
        {
            VisibilityAuthenication = false;
            VisibilityResetGrid = true;
            Alert("");

            TagAnswer = "Введите логин";

            _step++;
            // 
        }

        //
        //  Шаги смены пароля
        //
        private async void PasswordRecoverySteps(Object obj)
        {
            switch(_step)
            {
                // Обработка логина и загрузка ответа на вопрос
                case 1:
                    SQLiteQuery.SQL.Select selectSQL = new SQLiteQuery.SQL.Select();
                    SQLiteQuery.SQL.Update updateSQL = new SQLiteQuery.SQL.Update();
                    Alert("");

                    if (selectSQL.SelectUser(Answer))
                    {
                        var result = selectSQL.GetQuestion(Answer);
                        
                        // Подсказка
                        Additional = result[0][0];

                        // Ожидаемый ответ на вопрос
                        temp.AnswerUser = result[0][1];

                        // Логин пользователя
                        temp.Login = Answer;

                        // Скрытие поля ввода логина
                        VisibilityAnswer = false;

                        VisibilitiAdditional = true;

                        // Появление засекреченного поля ввода
                        VisibilitiResPassBox = true;

                        // Подсказка текстового поля
                        TagAnswer = "Введите ответ на вопрос";

                    }
                    else
                    {
                        _step--;
                        Alert("Пользователь не найден");
                    }
                    selectSQL = null;
                    break;
                    // Обработка ответа на секретный вопрос и ввод нового пароля
                case 2:
                    Alert("");
                    if(temp.AnswerUser == GetPassword(SecurePasswordResetPassword))
                    {
                        Additional = String.Empty;
                        await Task.Run(()=>ClearPasswordBox());
                        TagAnswer = "Введите новый пароль";
                    }
                    else
                    {
                        _step--;
                        Alert("Не вырный ответ");
                    }
                    break;
                    // Обработка введенного пароля
                case 3:
                    Alert("");
                    if (SecurePasswordResetPassword.Length > 0)
                    {
                        temp.Password = GetPassword(SecurePasswordResetPassword);
                        await Task.Run (()=> ClearPasswordBox());
                        TagAnswer = "Подтверждение пароля";
                    }
                    else
                    {
                        Alert("Все поля должны быть заполнены");
                        _step--;
                    }
                    break;
                case 4:
                    Alert("");
                    if (GetPassword(SecurePasswordResetPassword) == temp.Password)
                    {
                        updateSQL = new SQLiteQuery.SQL.Update();
                        var result = updateSQL.UpdatePassword(temp.Login, temp.Password);

                        if (result == 1)
                        {
                            Views.Alert.Save.PasswordChange();


                            temp.Login = null;
                            temp.Password = null;
                            temp.AnswerUser = null;

                            Answer = String.Empty;
                            Additional = String.Empty;


                            await Task.Run(() => ClearPasswordBox());

                            VisibilityResetGrid = false;
                            VisibilityAuthenication = true;

                            DisplayError = false;
                            
                            // Скрыть элементы
                            VisibilitiResPassBox = false;
                            VisibilityAnswer = true;
                            _step = -1;
                        }
                        else
                        {
                            Alert("");
                            Views.Alert.Save.UserIsNotSavingErrRequest(temp.Login);
                            _step--;
                        }

                    }
                    else
                    {
                        Alert("Пароли не совпадают");
                        _step--;
                    }
                    break;
            }
            _step++;
        }

        //
        //  Очистка секретного поля
        //
        private void ClearPasswordBox()
        {
                ClearPassBox = true;
                // Очистка засекреченного поля
                while (ClearPassBox)
                {
                    Thread.Sleep(10);
                }
        }

        //
        //  Измение текста ошибки
        //
        private void Alert(String text = "Все поля должны быть заполнены")
        {
            ErrorText = text;
            DisplayError = true;

            TagAnswer = "Введите логин";
        }

        //
        //  Получение пароля
        //
        private String GetPassword(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            catch
            {
                return null;  
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course.Views
{
    public class Alert
    {
        /*
         _e_ - Error
         _w_ - Warning
         _i_ - Information
         _s_ - successfully
        */
        private static String _e_save = "Ошибка сохранения / Saving error";
        private static String _s_successfully = "Успешно / Successfully";

        public class Save
        {
            //
            //  Ошибка сохранения данных пользователя
            //
            public static void UserIsNotSavingErrRequest(String login)
            {
                String messRus = $"Данные пользователя {login} не будут сохранены из-за проблемы запроса \n";
                String messEng = $"User data {login} will not be saved due to a query problem";

                Error(messRus + messEng, _e_save);
            }
            //
            //  Ошибка вызвана некорректного параметра
            //
            public static void UserIsNotSavingErrLetters(String login)
            {
                String messRus = $"Данные пользователя {login} не будут сохранены из-за наличия посторонних символов в поле доступа \n";
                String messEng = $"User data {login} will not be saved due to the presence of extraneous characters in the access field";

                Error(messRus + messEng, _e_save);
            }

            //
            //  Пароль успешно изменен
            //
            public static void PasswordChange()
            {
                String messRus = "Пароль успешно изменен\n";
                String messEng = "The password change is successful";

                Information(messRus + messEng, _s_successfully);
            }

            public static void Slide()
            {
                String messRus = "Слайд успешно изменен \n";
                String messEng = "The slide was saved successfully";

                Information(messRus + messEng, _s_successfully);
            }

            public static void UserData()
            {
                string messrus = "Данные пользователя успешно изменены\n";
                string messEng = "User data changed successfully";
                Information(messrus + messEng, _s_successfully);
            }

            public static void ErrNotFullData()
            {
                string messRus = "Сохранение данных не возможно, по причине заполнения всех полей\n";
                string messEnd = "Saving data is not possible due to the completion of all fields";

                Error(messRus + messEnd, _e_save);
            }
        }

        public class Create
        {
            public static void SuccessfullyChapter()
            {
                String messRus = "Создание новой главы прошло успешно \n";
                String messEng = "Creating a new Chapter was successful";

                Information(messRus + messEng, _s_successfully);
            }

            public static void SuccessfullyTopic()
            {
                String messRus = "Создание новой темы прошло успешно \n";
                String messEng = "Creating a new topic was successful";

                Information(messRus + messEng, _s_successfully);
            }

            public static void SuccessfullyUser()
            {
                String messRus = "Пользователь был успешно создан \n";
                String messEng = "The user was created successfully";

                Information(messRus + messEng, _s_successfully);
            }
        }

        public class Edit
        {
            public static void Slide()
            {
                String messRus = "Слайд был успешно изменен \n";
                String messEng = "The slide was successfully modified";

                Information(messRus + messEng, _e_save);
            }
        }

        private static void Information(String mess, String title) => Show(mess, title, MessageBoxIcon.Information, MessageBoxButtons.OK);

        private static void Error(String mess, String title) => Show(mess, title, MessageBoxIcon.Error, MessageBoxButtons.OK);

        private static void Show(String text, String title ,MessageBoxIcon icon, MessageBoxButtons button) =>
            MessageBox.Show(text, title, button, icon);
    }
}

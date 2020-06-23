using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
using Course.Models.Data.Temp;
using SQLiteQuery.SQL;

namespace Course.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateChapter.xaml
    /// </summary>
    public partial class CreateChapter : Window
    {
        private List<Chapters> _chapters = new List<Chapters>();
        private Boolean _isCreate { get; set; }

        public CreateChapter(Boolean isCreate)
        {
            InitializeComponent();
            _isCreate = isCreate;

            Setting();
            // Загрузка глав
            LoadChapters();
        }

        private void Setting()
        { 
            if(!_isCreate)
            {
                titleChapter.Visibility = Visibility.Collapsed;
                chapterTxtBox.Visibility = Visibility.Collapsed;
                btn.Content = "Удалдить";
            }
        }

        //
        //  Загрузка глав
        //
        private void LoadChapters()
        {
            Select select = new Select();

            _chapters.Clear();
            combobox.Items.Clear();

            if (!_isCreate)
            {
                nuberChapter.Content = "Выберите номер удаляемой главы";
            }
            else
            {
                // Обозначает главу пустышку, чтоьбы можно было добавить в начало
                _chapters.Add(new Chapters { Title = "Добавить в начало", Position = 0 });
            }

            var data = select.GetChapters();

            // Заполнение массива глав
            _chapters.AddRange(Chapters.AddArr(data));


            // Добавление глав с UI список
            foreach (Chapters value in _chapters)
                combobox.Items.Add(value.Title);

            combobox.SelectedIndex = 0;
            select = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(chapterTxtBox.Text == String.Empty && _isCreate)
            {
                errorLbl.Visibility = Visibility.Visible;
            }
            else
            {
                errorLbl.Visibility = Visibility.Hidden;

                if (_isCreate)
                {
                    // Создание

                    Insert sql = new Insert();

                    int result;
                    result = sql.InsertNewChapter(chapterTxtBox.Text, _chapters[combobox.SelectedIndex].Position + 1);

                    Alert.Create.SuccessfullyChapter();
                }
                else
                {
                    // Удаление
                    Delete delete = new Delete();
                    int id = _chapters[combobox.SelectedIndex].Id,
                        position = _chapters[combobox.SelectedIndex].Position;

                    var result = delete.DeleteChapter(id, position);

                    /*
                    if(result == -1) 
                        Alert.*/
                }
                LoadChapters();
            }
        }
    }
}

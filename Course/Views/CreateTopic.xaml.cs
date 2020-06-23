using Course.Models.Data.Temp;
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
using SQLiteQuery.SQL;

namespace Course.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateTopic.xaml
    /// </summary>
    public partial class CreateTopic : Window
    {
        private List<Chapters> _chapters = new List<Chapters>();
        private List<Topics> _topics = new List<Topics>();
        private Boolean _isCreate { get; set; }

        public CreateTopic(Boolean isCreate)
        {
            InitializeComponent();
            _isCreate = isCreate;

            if (!_isCreate)
                numberTopic.Content = "Выберите номер удаляемой темы";

            LoadChapters();
        }

        private void LoadChapters()
        {
            Select sql = new Select();

            // Загрузка глав
            var data = sql.GetChapters();

            _chapters.AddRange(Chapters.AddArr(data));

            // Заполнение глав
            foreach (Chapters value in _chapters)
                comboboxChapter.Items.Add(value.Title);

            comboboxChapter.SelectedIndex = 0;
            sql = null;
        }

        //
        //  Динамическая подгрузка тем
        //
        private void Chapter_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _topics.Clear();
            comboboxtopics.Items.Clear();

            var sql = new Select();

            if(_isCreate)
                _topics.Add(new Topics { Name = "Добавить в начало", Position = 1 });

            _topics.AddRange(Topics.AddArr(sql.GetTopics(_chapters[comboboxChapter.SelectedIndex].Id)));

            foreach (Topics value in _topics)
                comboboxtopics.Items.Add(value.Name);

            sql = null;

            comboboxtopics.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(chapterTxtBox.Text != String.Empty && comboboxChapter.Items.Count != 0)
            {
                errorLbl.Visibility = Visibility.Hidden;

                if (_isCreate)
                {
                    //
                    //  Создание темы
                    //

                    int position = _topics[comboboxtopics.SelectedIndex].Position + 1;
                    Insert sql = new Insert();
                    Int32 idChapter = _chapters[comboboxChapter.SelectedIndex].Id;
                    var result = sql.InsertNewTopic(idChapter, chapterTxtBox.Text, position);
                    Alert.Create.SuccessfullyTopic();
                }
                else
                {
                    //
                    //  Удаление темы
                    //
                    Delete sql = new Delete();
                    int idTopic = _topics[comboboxtopics.SelectedIndex].Id,
                        position = _topics[comboboxtopics.SelectedIndex].Position,
                        idChapter = _topics[comboboxtopics.SelectedIndex].IdChapter;


                    var result = sql.DeleteTopic(idTopic, position, idChapter);

                    Alert.Create.SuccessfullyTopic();
                }

                Chapter_SelectionChanged(null, null);
            }
            else
            {
                errorLbl.Visibility = Visibility.Visible;
            }
        }
    }
}

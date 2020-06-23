using Course.Models.Data.Temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using SQLiteQuery.SQL;

namespace Course.Views
{
    /// <summary>
    /// Логика взаимодействия для DeleteSlide.xaml
    /// </summary>
    public partial class DeleteSlide : Window
    {
        private List<Topics> _arrTopics = new List<Topics>();
        private List<Chapters> _arrChapters = new List<Chapters>();
        private List<Slides> _arrSlides = new List<Slides>();


        public DeleteSlide()
        {
            InitializeComponent();


            LoadChapters();

        }

        private void LoadChapters()
        {
            сhapter.Items.Clear();
            Select select = new Select();
            _arrChapters = Chapters.AddArr(select.GetChapters());
            foreach (Chapters value in _arrChapters)
                сhapter.Items.Add(value.Title);
            сhapter.SelectedIndex = 0;

            LoadTopics(сhapter.SelectedIndex);
        }

        private void LoadTopics(int idChapter)
        {
            topic.Items.Clear();
            Select select = new Select();

            _arrTopics = Topics.AddArr(select.GetTopics(idChapter));
            foreach (Topics value in _arrTopics)
                topic.Items.Add(value.Name);
            topic.SelectedIndex = 0;

            LoadSlides(topic.SelectedIndex);
        }

        private void LoadSlides(int idTopic)
        {
            slide.Items.Clear();
            Select select = new Select();

            _arrSlides = Slides.AddArr(select.GetSlides(idTopic));
            foreach (Slides value in _arrSlides)
                slide.Items.Add(value.Position);
            slide.SelectedIndex = 0;
        }

        //
        //	Динамическая загрузка тем
        //
        private void Chapter_SelectionChanged(object sender, RoutedEventArgs e)
        {
            new Thread(() => {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() => {
                    int selected = (сhapter.SelectedIndex < 0) ? 0 : сhapter.SelectedIndex;
                    LoadTopics(_arrChapters[selected].Id);

                }));
            })
            { IsBackground = true }.Start();

        }

        //
        //	Динамическая загрузка слайдов
        //
        private void Topic_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            new Thread(() => {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() => {
                    int selected = (topic.SelectedIndex < 0) ? 0 : topic.SelectedIndex;
                    LoadSlides(_arrTopics[selected].Id);
                }));
            })
            { IsBackground = true }.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Delete delete = new Delete();
            int idSlide = _arrSlides[slide.SelectedIndex].Id,
                position = _arrSlides[slide.SelectedIndex].Position,
                idTopic = _arrSlides[slide.SelectedIndex].IdTopic;

            var result = delete.DeleteSlide(idSlide, position, idTopic);

            if (result > 0)
                Alert.Edit.Slide();

            LoadSlides(idTopic);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using SQLiteQuery;
using System.Threading;
using System.Windows.Threading;
using Course.Models.Data.Temp;

namespace Course.Controllers
{
    class LogicFullScreenMenu
    {
        #region Переменые

        #region public
        // Список тем открыт
        public Boolean _isOpen { get; set; } = true;

        public Boolean _openTopic { get; set; } = true;

        // Список всех глав и вложенных тем для друких классов
        public List<Grid> Chapters { get { return _chapters; } }
        #endregion


        #region private
        private SQLiteQuery.SQL.Select select = new SQLiteQuery.SQL.Select();

        // Ширина контента одной главы
        private Int32 _widthGrid { get; } = 300;

        // Список всех глав и вложенных тем
        private List<Grid> _chapters { get; set; } = new List<Grid>();
        private const String _tagOpen = "Open";
        private const String _tagClose = "Close";

        #endregion

        #endregion


        public LogicFullScreenMenu(WrapPanel wp)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() => GenerateWrapPanel(wp)));
            })
            { IsBackground = true }.Start();
        }

        //
        //  Загрузка всего меню
        //
        public void GenerateWrapPanel(WrapPanel menu)
        {
            //
            //  Весь контент берется из БД
            //
            menu.Children.Clear();
            var data = select.LoadMenu();

            /*
                Структура
                [0] - Chapters.Id
                [1] - Chapters.Title
                [2] - Chapters.Number
                [3] - Topics.Id
                [4] - Topics.Id_chapter
                [5] - Topics.Name
                [6] - Topics.Position
            */
            for (Int32 track = 0; track < data.Count; track++)
            {
                try
                {
                    #region Variables
                    Grid grid;
                    Button title;
                    ListBox listBox;
                    #endregion

                    Int32 temp = 0;
                    Boolean isEmptyChapter = false;

                    // Проверка на пустую главу
                    if (data[track][6] != String.Empty)
                        temp = Convert.ToInt32(data[track][6]);
                    else isEmptyChapter = true;

                    // Текущий номер главы
                    Int32 nowNumber = Convert.ToInt32(data[track][2]);

                    // Создание панели размещения контента
                    grid = GenerateGrid(3, 1);

                    // Создание заголовка
                    title = GenerateButton("Title", data[track][1], Convert.ToInt32(data[track][0]), "DefaultButtonLabel", 20);

                    // Создание области размещения тем
                    listBox = GenerateListBox("Content", Convert.ToInt32(data[track][0]), new Thickness(10, 0, 0, 0), Brushes.Transparent, 14);

                    //
                    //  Заполнение темами
                    //
                    // Проверка на отсутствие тем
                    if (!isEmptyChapter)
                    {

                        // Указатель на следующ коллекцию с данными
                        Int32 index = track;

                        while (true)
                        {
                            try
                            {
                                if (Convert.ToInt32(data[index][2]) == nowNumber && index < data.Count)
                                {
                                    // Добавление темы к главе
                                    listBox.Items.Add(GenerateListBoxItem(data[index][5], Convert.ToInt32(data[index][3])));
                                }
                                else
                                {
                                    track = (index != data.Count) ? index - 1 : index;
                                    break;
                                }
                                index++;
                            }
                            catch {
                                break;
                            }
                        }
                    }

                    // Создание линия развертывания
                    var underLine = GenerateUnderLine(_isOpen);

                    Grid.SetRow(title, 0);
                    Grid.SetRow(listBox, 1);
                    Grid.SetRow(underLine, 2);

                    grid.Children.Add(title);
                    grid.Children.Add(listBox);
                    grid.Children.Add(underLine);

                    title.Click += (s, e) =>
                    {
                        if (listBox.Visibility == Visibility.Visible)
                        {
                            listBox.Visibility = Visibility.Collapsed;
                            grid.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Pixel);
                        }
                        else
                        {
                            listBox.Visibility = Visibility.Visible;
                            grid.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Auto);
                        }
                    };

                    listBox.SelectionChanged += ListBox_SelectionChanged;


                    title.Foreground = new SolidColorBrush(Colors.White);
                    listBox.Foreground = new SolidColorBrush(Colors.White);

                    listBox.Background = new SolidColorBrush(Colors.Transparent);
                    grid.Background = new SolidColorBrush(Colors.Transparent);
                    menu.Background = new SolidColorBrush(Colors.Transparent);


                    _chapters.Add(grid);
                    menu.Children.Add(_chapters.Last());
                }
                catch (Exception ex)
                {
                    SecurityModule.Logs.SetError(ex);
                }
            }

        }

        //
        //  Обработка выбора темы
        //
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {
                    // Получение доступа к списку тем
                    ListBox list = (ListBox)sender;
                    ListBoxItem item = (ListBoxItem)list.SelectedItem;

                    _openTopic = true;
                    // Загрузка слайдов
                    Models.Data.Temp.Sliders.EditList(select.LoadSlides(Convert.ToInt32(item.Tag)));
                }));
            })
            { IsBackground = true }.Start();
        }

        //
        //  Создание тела главы
        //
        public Grid GenerateGrid(Byte countRows, Byte countColumns)
        {
            Grid grid = new Grid();
            grid.Width = _widthGrid;
            grid.Margin = new Thickness(5, 0, 0, 5);
            grid.Background = Brushes.Transparent;
            grid.Tag = _tagOpen;

            // Создание строк
            for (Int16 index = 0; index < countRows; index++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions[index].Height = new GridLength(0, GridUnitType.Star);
            }

            // Создание столбцов
            for (Int16 index = 0; index < countColumns; index++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            return grid;
        }

        //
        //  Создание контента главы (темы)
        //
        private ListBox GenerateContent(String topicText, Int32 tagTopic, Int32 tagChapter)
        {
            var listBox = GenerateListBox("Content", tagChapter, new Thickness(10, 0, 0, 0), Brushes.Transparent, 14);
            // Генерация тем
            for (Int16 index = 0; index < 5; index++)
                listBox.Items.Add(new ListBoxItem { Content = topicText, Tag = tagTopic });
            // listBox.Items.Add("Тема");

            return listBox;
        }

        //
        //  Создание пункта темы для Главы
        //
        private ListBoxItem GenerateListBoxItem(String content, Int32 tag)
        {
            ListBoxItem item = new ListBoxItem();
            item.Content = content;
            item.Tag = tag;

            return item;
        }

        //
        //  Создание нижней линии
        //
        private StackPanel GenerateUnderLine(Boolean isOpen)
        {
            var panel = GenerateStackPanel(new Thickness(0, 0, 0, 0));

            // Создание нижней линии
            var underLine = GenerateRectangle("UnderLine", Brushes.Silver, VerticalAlignment.Top, HorizontalAlignment.Stretch, 2);

            // Создание треугольника (состояние списка)
            // Если список не раскрыт
            PointCollection points = new PointCollection();
            if (!isOpen)
            {
                // Вершина треугольника вниз
                points.Add(GeneratePoint(0, 0));
                points.Add(GeneratePoint(20, 0));
                points.Add(GeneratePoint(10, 7));
            }
            else
            {
                // вершина треугольника вверх
                points.Add(GeneratePoint(0, 7));
                points.Add(GeneratePoint(20, 7));
                points.Add(GeneratePoint(10, 0));
            }

            var triangle = GeneratePoligon("Triangle", Brushes.Black, points, HorizontalAlignment.Center);

            panel.Children.Add(underLine);
            panel.Children.Add(triangle);

            return panel;
        }

        //
        //  Создание названия главы
        //
        private Button GenerateButton(String name, String content, Int32 tag, String style, Byte fontSize)
        {
            //DefaultButtonLabel
            Button btn = new Button();
            btn.Style = (Style)btn.FindResource(style);
            btn.Content = content;
            btn.FontSize = fontSize;
            btn.Name = name;
            btn.Tag = tag;
            return btn;
        }

        //
        //  Генерация StackPanel
        //
        private StackPanel GenerateStackPanel(Thickness thickness)
        {
            StackPanel panel = new StackPanel();
            panel.Margin = thickness;

            return panel;
        }

        //
        //  Создание квадрата
        //
        private Rectangle GenerateRectangle(String name, Brush brush, VerticalAlignment verticalAligment, HorizontalAlignment horizontalAlignment, Byte height)
        {
            Rectangle rect = new Rectangle();

            rect.Fill = brush;
            rect.VerticalAlignment = verticalAligment;
            rect.HorizontalAlignment = horizontalAlignment;
            rect.Height = height;
            rect.Name = name;

            return rect;
        }

        //
        //  Создание ресунка
        //
        private Polygon GeneratePoligon(String name, Brush brush, PointCollection points, HorizontalAlignment horizontalAlignment)
        {
            Polygon polygon = new Polygon();
            polygon.Fill = brush;
            polygon.Points = new PointCollection();
            polygon.HorizontalAlignment = horizontalAlignment;
            polygon.Name = name;

            return polygon;
        }

        //
        //  Создание тем
        //
        private ListBox GenerateListBox(String name, Int32 tag, Thickness thickness, Brush brush, Byte fontSize)
        {
            ListBox list = new ListBox();
            list.Margin = thickness;
            list.BorderBrush = brush;
            list.FontSize = fontSize;
            list.Tag = tag;
            list.Name = name;

            return list;
        }

        //
        //  Создание точек для рисунка
        //
        private Point GeneratePoint(Int32 x, Int32 y)
        {
            Point point = new Point();
            point.X = x;
            point.Y = y;

            return point;
        }
    }
}

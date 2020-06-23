using Course.Views.UsersControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Timers;
using System.IO;
using System.Reflection;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Controls.Primitives;
using Course.Views;
using System.Windows.Documents;
using SQLiteQuery;

namespace Course.ViewModels
{
    class LogicMainWindow
    {
        #region Variables

        #region private

        // Путь к временному файлу
        private String _pathTemp = Path.GetDirectoryName(Application.ResourceAssembly.Location) + @"/data/temp/";

        // Отслеживание обновления темы
        private System.Timers.Timer timer = new System.Timers.Timer();
        private System.Timers.Timer _checkAuth = new System.Timers.Timer();
        // Время последнего обновления контента
        private String _lastUpdate = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString();
        // Номер текущего слайда
        private Int32 _indexSlide = 0;
        private Boolean _isAuth = false;

        #region UI

        #region Grid
        // Основная панель
        private Grid _grid { get; set; }
        // Панель времени
        private Grid _clock { get; set; }
        // Панель управления слайдами
        private Grid _driveSlideGrid { get; set; }
        #endregion

        #region Button
        // Иконка меню
        private Button _menuIcon { get; set; }
        // Прошлый слайд
        private Button _backSlide { get; set; }
        // Следующий слайд
        private Button _nextSlide { get; set; }
        private Button _authenication { get; set; }
        private Button _setting { get; set; }
        #endregion

        #region ViewBox

        // Област маштабирования времени
        private Viewbox _viewboxClock { get; set; }

        // Область маштабирования управления слайдами
        private Viewbox _viewboxDriveSlides { get; set; }

        #endregion

        #region Label
        // Название главы
        private Label _chapter { get; set; }
        #endregion

        #region FullScreenMenu
        // Меню
        private FullScreenMenu _fullScreenMenu { get; set; }
        #endregion

        #region ButtonBase
        private ButtonBase _buttonBase { get; set; }
        #endregion

        #region MainWindow
        private MainWindow _mainWindow { get; set; }
        #endregion

        #region WrapPanel
        private WrapPanel _rightControlPanel { get; set; }
        #endregion

        #region PersonalAccount
        private PersonalAccount _personalAccount { get; set; }
        #endregion


        #endregion

        #endregion

        #endregion
        public LogicMainWindow(ref Grid grid, MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            GetMethods(ref grid);

            _authenication.Click += _authenication_Click;

            _nextSlide.Click += NextSlide;
            _backSlide.Click += BackSlide;

            TimerStart();

            //_backSlide.Content = Models.FontAwersome.ChevronCircleLeft;
            //_nextSlide.Content = Models.FontAwersome.ChevronCircleRight;



            _nextSlide.Visibility = Visibility.Hidden;
            _backSlide.Visibility = Visibility.Hidden;

        }

        //
        //  Таймер отслеживания изменений в программе
        //
        private void TimerStart()
        {
            timer.Interval = 600;
            timer.Enabled = true;
            timer.Elapsed += (s, e) =>
            {
                // Проверка на изменение слайдера
                if (Models.Data.Temp.Sliders.lastUpdate != _lastUpdate)
                {
                    _lastUpdate = Models.Data.Temp.Sliders.lastUpdate;

                    LoadContent();

                    _indexSlide = 0;
                }

                // Проверка на выход из аккаунта
                if (_isAuth)
                {
                    if (Models.Data.Temp.UserData.Login == null)
                    {
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (Action)(() => _authenication.Content = "Авторизация"));
                        _isAuth = false;
                    }
                }
                GC.Collect();
            };
        }


        //
        //  Кнопка авторизации
        //
        private void _authenication_Click(object sender, RoutedEventArgs e)
        {
            if (Models.Data.Temp.UserData.Login != null)
            {
                // Тут открытие аккаунта
                _personalAccount.Visibility = (_personalAccount.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                Authenication authenication = new Authenication();
                authenication.ShowDialog();
                authenication = null;

                GC.Collect(2,GCCollectionMode.Forced);
                //  Проверка на авторизацию
                if (Models.Data.Temp.UserData.Login != null)
                {
                    _authenication.Content = Models.Data.Temp.UserData.Login;
                    //_slide.Visibility = Visibility.Hidden;
                    _personalAccount.Visibility = Visibility.Visible;
                    _isAuth = true;

                }
            }
            _personalAccount.LoadData();
        }

        //
        //  Обновление контента
        //
        private async void LoadContent()
        {
            await Task.Run(() =>
            {

                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    // Делает запрос и получает коллекцию слайдов
                    var slide = Models.Data.Temp.Sliders.GetSlide(_indexSlide);
                    SQLiteQuery.SQL.Select select = new SQLiteQuery.SQL.Select();


                    _mainWindow.content.NewSlide(slide[2]);
                    //
                    //
                    //
                    //
                    List<List<String>> data;
                    try
                    {

                        data = select.GetChapter(Convert.ToInt32(Encoding.Default.GetString(slide[1])));
                        _chapter.Content = data[0][0];

                        _nextSlide.Visibility = Visibility.Visible;

                        if (_indexSlide == Models.Data.Temp.Sliders.Count() - 1)
                        {
                            _nextSlide.Visibility = Visibility.Hidden;
                        }
                        else if (_indexSlide != 0)
                        {
                            _nextSlide.Visibility = Visibility.Visible;
                            _backSlide.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            _backSlide.Visibility = Visibility.Hidden;
                        }
                    }
                    catch
                    {
                        _nextSlide.Visibility = Visibility.Hidden;
                        _backSlide.Visibility = Visibility.Hidden;
                        _mainWindow.content.NewSlide(new Byte[] { });
                    }




                        select = null;
                }
                ));

            });
        }

        //
        //  Следующий слайд
        //
        private void NextSlide(object sender, RoutedEventArgs e)
        {
            _indexSlide++;
            LoadContent();
        }

        //
        //  Прошлый слайд
        //
        private void BackSlide(object sender, RoutedEventArgs e)
        {
            _indexSlide--;
            LoadContent();
            
        }

        //
        //  Загрузка всех методом
        //
        private void GetMethods(ref Grid grid)
        {
            _grid = grid;

            Nesting.FromGrid fromGrid = new Nesting.FromGrid();
            Nesting.FromViewBox fromViewBox = new Nesting.FromViewBox();
            Nesting.FromWrapPanel fromWrapPanel = new Nesting.FromWrapPanel();

            _menuIcon = fromGrid.GetButton(grid, "menuIcon");
            _chapter = fromGrid.GetLabel(grid, "chapter");

            _viewboxDriveSlides = fromGrid.GetViewbox(grid, "viewBoxDriveSlides");
            _personalAccount = fromGrid.GetPersonalAccount(grid, "personalAccount");

            _backSlide = fromViewBox.GetButton(_viewboxDriveSlides, "backSlide");
            _nextSlide = fromViewBox.GetButton(_viewboxDriveSlides, "nextSlide");

            _rightControlPanel = fromGrid.GetWrapPanel(grid, "rightControlPanel");
            _authenication = fromWrapPanel.GetButton(_rightControlPanel, "authenication");
            _setting = fromWrapPanel.GetButton(_rightControlPanel, "setting");

            /*
            _chapter = Nesting.FromGrid.GetLabel(grid, "chapter");
            _content = Nesting.FromGrid.GetWebBrowser(grid, "content");*/
            _fullScreenMenu = fromGrid.GetFullScreenMenu(grid, "fullScreenMenu");
        }

        //
        //  Отображение меню
        //
        public Visibility ChangeStateMenu(Boolean visible) => (visible) ? Visibility.Hidden : Visibility.Visible;
    }
}

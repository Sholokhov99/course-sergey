using Course.Controllers;
using Course.Models.Data.Temp;
using Course.ViewModels;
using Microsoft.Win32;
using SecurityModule;
using SQLiteQuery;
using SQLiteQuery.SQL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Course.Views.UsersControls
{
    /// <summary>
    /// Логика взаимодействия для Slide.xaml
    /// </summary>
    public partial class Slide : UserControl
    {

        private List<Topics> _arrTopics = new List<Topics>();
        private List<Chapters> _arrChapters = new List<Chapters>();
        private List<Slides> _arrSlides = new List<Slides>();
        public Boolean isCreate = true;

        public Slide()
        {
            InitializeComponent();

            // Настройка слайдера
            StartSetting();

            // Загрузка всех глав
            LoadChapters();

            typeSetSlide.Content = (isCreate) ? "После слайда" : "Текущий слад";
        }

        //
        //  Загрузка глав
        //
        private void LoadChapters()
        {
            try
            {
                Select sql = new Select();
                сhapter.Items.Clear();

                // Получение всех глав
                List<List<String>> data = sql.GetChapters();
                // Занесение глав в память
                _arrChapters = Chapters.AddArr(data);

                // Заполнение combobox с главами
                foreach (Chapters value in _arrChapters)
                    сhapter.Items.Add(value.Title);

                // Стартовая позиция выбора главы
                сhapter.SelectedIndex = 0;

                // Загрузка тем
                LoadTopic(_arrChapters[0].Id);
            }
            catch (Exception ex)
            {
                Logs.SetError(ex);
            }
        }

        private void StartSetting()
        {
            this.FontFamily = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/resources/"), "#FontAwesome");
            this.FontSize = 24;


            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };


            this.FontFamily = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/resources/"), "#FontAwesome");
            this.FontSize = 24;
        }

        //
        //	Отображение новгого слайда
        //
        public void NewSlide(Byte[] value)
        {

            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {
                    try
                    {

                        // Создание файла из массива байт
                        File.WriteAllBytes(Sliders.tempRead, value);
                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        using (FileStream fs = new FileStream(Sliders.tempRead, FileMode.Open))
                        {
                            range.Load(fs, DataFormats.Rtf);
                        }
                        ClearTempFile(Models.Data.Temp.Sliders.tempRead);
                    }
                    catch
                    {
                        rtbEditor.Document.Blocks.Clear();
                    }
                }));
            })
            { IsBackground = true }.Start();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                    if (dlg.ShowDialog() == true)
                    {
                        FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        range.Load(fileStream, DataFormats.Rtf);
                    }
                }));
            })
            { IsBackground = true }.Start();
        }
        //
        //	Сохранение содержимого на компьютер
        //
        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {


                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                    if (dlg.ShowDialog() == true)
                    {
                        using (FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create))
                        {
                            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                            range.Save(fileStream, DataFormats.Rtf);
                        }
                    }


                }));
            })
            { IsBackground = true }.Start();
        }

        //
        //	Сохранение файла
        //
        private void SaveFile(String path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }
        //
        //	Считывание файла
        //
        private Byte[] ReadFileBytes(String path)
        {
            Byte[] array;
            using (FileStream fs = File.OpenRead(path))
            {
                array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
            }
            return array;

        }

        //
        //  Очистка временных файлов
        //
        private void ClearTempFile(String way)
        {
            if (File.Exists(way))
            {
                FileStream fs = File.Create(way);
                fs.Close();
            }
        }

        //
        //	Сохранение содержимого в БД
        //
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {
                    // Проверка на заполнение всех полей
                    if (_arrTopics.Count != 0)
                    {
                        Insert insertSQL = new Insert();
                        Update updateSQL = new Update();

                        // Очистка временных файлов
                        ClearTempFile(Sliders.tempWrite);

                        // Сохоанение файла
                        SaveFile(Sliders.tempWrite);
                        // Считывание файла
                        var array = ReadFileBytes(Sliders.tempWrite);

                        int idTopic = _arrTopics[topic.SelectedIndex].Id;

                        int position = -1;

                        if (isCreate)
                        {
                            position = _arrSlides[slide.SelectedIndex].Position + 1;

                            var data = insertSQL.InsertNewSlide(idTopic, array, position);
                            Alert.Save.Slide();

                            LoadChapters();
                        }
                        else
                        {
                            position = _arrSlides[slide.SelectedIndex].Position;
                            int idSlide = _arrSlides[slide.SelectedIndex].Id;
                            var data = updateSQL.UpdateSlide(idSlide, idTopic, array, position);

                            if (data != 0)
                                Alert.Edit.Slide();
                        }

                        // Очистка временных файлов
                        //ClearTempFile(Sliders.tempWrite);
                        insertSQL = null;
                        updateSQL = null;
                        //Topic_SelectionChange(null, null);
                    }
                }));
            })
            { IsBackground = true }.Start();
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
        }

        private void btnSetImage_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "Images |*.bmp;*.jpg;*.png;*.gif;*.ico";
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            System.Windows.Forms.DialogResult result = openFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {


                System.Drawing.Image img = System.Drawing.Image.FromFile(openFileDialog1.FileName);

                BitmapSource bitmap = Imaging.CreateBitmapSourceFromHBitmap(
                    new Bitmap(img).GetHbitmap(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions()

                );

                Clipboard.SetImage(bitmap);
                rtbEditor.Paste();
                rtbEditor.Focus();
            }
            else
            {
                rtbEditor.Focus();
            }

            //25 марта
        }

        //
        //	Динамическая загрузка тем
        //
        private void сhapter_Selected(object sender, RoutedEventArgs e)
        {
            int selected = (сhapter.SelectedIndex < 0) ? 0 : сhapter.SelectedIndex;
            LoadTopic(_arrChapters[selected].Id);
        }

        //
        //	Динамическая загрузка слайдов
        //
        private void Topic_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            int selected = (topic.SelectedIndex < 0) ? 0 : topic.SelectedIndex;
            LoadSlides(_arrTopics[selected].Id);
        }


        private void Slide_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {
                    if (!isCreate)
                    {
                        Select select = new Select();

                        NewSlide(_arrSlides[slide.SelectedIndex].Content);
                    }
                }));
            })
            { IsBackground = true }.Start();


        }

        //
        //  Загрузка тем
        //
        private void LoadTopic(int idChapter)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {
                    topic.Items.Clear();
                    _arrTopics.Clear();

                    Select sql = new Select();
                    var data = sql.GetTopics(idChapter);

                    _arrTopics = Topics.AddArr(data);

                    foreach (Topics value in _arrTopics)
                        topic.Items.Add(value.Name);

                    topic.SelectedIndex = 0;

                    sql = null;
                }));
            })
            { IsBackground = true }.Start();
        }

        //
        //  Загрузка слайдов
        //
        private void LoadSlides(int idTopic)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {
                    try
                    {
                        slide.Items.Clear();
                        _arrSlides.Clear();

                        Select sql = new Select();
                        var data = sql.GetSlides(idTopic);

                        if (isCreate)
                            _arrSlides.Add(new Slides { Position = 0 });

                        _arrSlides.AddRange(Slides.AddArr(data));

                        // Заполнение списка с слайдами
                        foreach (Slides value in _arrSlides)
                            slide.Items.Add(value.Position);

                        sql = null;
                        slide.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        SecurityModule.Logs.SetError(ex);
                    }
                }));
            })
            { IsBackground = true }.Start();
        }
    }
}

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
using Course.ViewModels;
using System.Windows.Input;
using System.Windows.Media;
using Course.Views;
using System.Media;
using System.IO.Packaging;
using SecurityModule;

namespace Course
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LogicMainWindow logic;

        public MainWindow()
        {
            InitializeComponent();
            SQLiteQuery.Config.CreateBackupDB();

            logic = new LogicMainWindow(ref mainGrid, this);
            Models.Data.Temp.Sliders.Options();

            this.FontFamily = new FontFamily(new Uri("pack://application:,,,/resources/"), "#FontAwesome");

            content.toolBar.Visibility = Visibility.Collapsed;
            content.navigationGrid.Visibility = Visibility.Collapsed;
            content.rtbEditor.IsReadOnly = true;
            content.rtbEditor.Cursor = Cursors.Arrow;
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            menuIcon.Background = Brushes.Transparent;
        }

        private void menuIcon_Click(object sender, RoutedEventArgs e)
        {
            var visability = logic.ChangeStateMenu(fullScreenMenu.IsVisible);
            if (visability == Visibility.Visible)
                fullScreenMenu.UpdateMenu();

            fullScreenMenu.Visibility = visability;
        }
    }
}

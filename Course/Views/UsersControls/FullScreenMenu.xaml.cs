using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Course.Controllers;

namespace Course.Views.UsersControls
{
    /// <summary>
    /// Логика взаимодействия для FullScreenMenu.xaml
    /// </summary>
    public partial class FullScreenMenu : UserControl
    {
        LogicFullScreenMenu logic;
        public FullScreenMenu()
        {
            InitializeComponent();
            this.Foreground = new SolidColorBrush(Colors.White);
            scrollViewer.Background = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
            logic = new LogicFullScreenMenu(FullMenu);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void UpdateMenu() => logic.GenerateWrapPanel(FullMenu);
    }
}

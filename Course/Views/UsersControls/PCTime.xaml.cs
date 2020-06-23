using System.Windows;
using System.Windows.Controls;

namespace Course.Views.UsersControls
{
    /// <summary>
    /// Логика взаимодействия для PCTime.xaml
    /// </summary>
    public partial class PCTime : UserControl
    {
        public PCTime()
        {
            InitializeComponent();
            DataContext = new ViewModels.SystemTime();
        }

        private void time_Loaded(object sender, RoutedEventArgs e)
        {/*
            Timer time = new Timer();
            time.Interval = 600;
            time.Elapsed += (s, q) =>
            {
                pcTime.Content = $"{DateTime.Now.Hour} : {DateTime.Now.Minute}";
            };*/
        }
    }
}

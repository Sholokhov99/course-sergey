using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Course.ViewModels;

namespace Course.Views
{
    /// <summary>
    /// Логика взаимодействия для Authenication.xaml
    /// </summary>
    public partial class Authenication : Window
    {
        public delegate void InvokeDelegate();
        Timer timer = new Timer();
        public Authenication()
        {
            InitializeComponent();
            BackVideo.Play();
            this.DataContext = new LogicAuthAndResPass();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //logic = new LogicAuthAndResPass();
            this.Closed += Authenication_Closed;
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 600;
            timer.Start();
        }

        private void Authenication_Closed(object sender, EventArgs e)
        {
            mainGrid.Children.Clear();
            this.DataContext = null;
            GC.Collect(0, GCCollectionMode.Forced);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,(Action)(() => 
            {
                try
                {
                    if (this.DataContext != null)
                    {
                        if (((dynamic)this.DataContext).ClearPassBox == true)
                        {
                            pass.Password = null;

                            ((dynamic)this.DataContext).ClearPassBox = false;
                        }

                        if (((dynamic)this.DataContext).IsAuth)
                        {
                            timer.Stop();
                            this.Close();
                        }


                    }
                }
                catch (Exception ex)
                {
                    SecurityModule.Logs.SetError(ex);
                }
                GC.Collect();
            }));
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackVideo.Stop();
            BackVideo.Play();
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(this.DataContext != null)
                ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((dynamic)this.DataContext).SecurePasswordResetPassword = ((PasswordBox)sender).SecurePassword;
        }
    }
}

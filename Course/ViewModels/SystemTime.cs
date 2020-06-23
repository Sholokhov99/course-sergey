using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Timers;

namespace Course.ViewModels
{
    class SystemTime : INotifyPropertyChanged
    {
        private Timer _timer;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChamged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #region Variable
        //  8:05 PM
        private String _hour;
        private String _minute;
        private String _dot;
        private String _clock;


        public String Clock
        {
            get { return _clock; }
            set
            {
                _clock = value;
                OnPropertyChamged();
            }
        }


        public String Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                OnPropertyChamged();
            }
        }

        public String Minute
        {
            get { return _minute; }
            set
            {
                _minute = value;
                OnPropertyChamged();
            }
        }

        public String Dot
        {
            get { return _dot; }
            set
            {
                _dot = value;
                OnPropertyChamged();
            }
        }
        #endregion

        public SystemTime()
        {
            LoadData();
            _timer = new Timer()
            {
                Interval = 1000,
                AutoReset = true,
                Enabled = true,
            };
            _timer.Elapsed += (s, e) =>
            {
                LoadData();

            };
            _timer.Start();

        }

        private void LoadData()
        {
            Hour = DateTime.Now.Hour.ToString();
            Minute = (DateTime.Now.Minute.ToString().Length == 1) ? $"0{DateTime.Now.Minute.ToString()}" : DateTime.Now.Minute.ToString();

            if (Dot != String.Empty)
                Dot = String.Empty;
            else
                Dot = ":";
        }
    }
}

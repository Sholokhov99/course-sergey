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

namespace Course.Views
{
    /// <summary>
    /// Логика взаимодействия для NewSlide.xaml
    /// </summary>
    public partial class NewSlide : Window
    {
        public NewSlide(Boolean isCreate)
        {
            InitializeComponent();
            slide.isCreate = isCreate;
        }
    }
}

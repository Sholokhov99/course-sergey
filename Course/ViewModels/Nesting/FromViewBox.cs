using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Course.ViewModels.Nesting
{
    class FromViewBox
    {
        public Button GetButton(Viewbox viewbox, String name) => viewbox.FindName(name) as Button;
        public Label GetLabel(Viewbox viewbox, String name) => viewbox.FindName(name) as Label;
    }
}

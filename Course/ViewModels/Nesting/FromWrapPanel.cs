using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Course.ViewModels.Nesting
{
    class FromWrapPanel
    {
        public Button GetButton(WrapPanel grid, String name) => grid.FindName(name) as Button;
    }
}

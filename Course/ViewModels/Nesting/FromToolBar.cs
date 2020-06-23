using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Course.ViewModels.Nesting
{
    class FromToolBar
    {
        public ToggleButton GetToggleButton(ToolBar toolbar, String name) => toolbar.FindName(name) as ToggleButton;
        public Button GetButton(ToolBar toolbar, String name) => toolbar.FindName(name) as Button;
        public ComboBox GetComboBox(ToolBar toolbar, String name) => toolbar.FindName(name) as ComboBox;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Course.ViewModels.Nesting
{
    class FromDockPanel
    {
        public Grid GetGrid(DockPanel dockpanel, String name) => dockpanel.FindName(name) as Grid;
        public RichTextBox GetRichTextBox(DockPanel dockpanel, String name) => dockpanel.FindName(name) as RichTextBox;
        public ToolBar GetToolBar(DockPanel dockpanel, String name) => dockpanel.FindName(name) as ToolBar;
    }
}

using Course.Views.UsersControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Course.ViewModels.Nesting
{
    class FromGrid
    {
        public Label GetLabel(Grid grid, String name) => grid.FindName(name) as Label;
        public Grid GetGrid(Grid grid, String name) => grid.FindName(name) as Grid;
        public TextBox GetTextBox(Grid grid, String name) => grid.FindName(name) as TextBox;
        public PasswordBox GetPasswordBox(Grid grid, String name) => grid.FindName(name) as PasswordBox;
        public Button GetButton(Grid grid, String name) => grid.FindName(name) as Button;
        public Viewbox GetViewbox(Grid grid, String name) => grid.FindName(name) as Viewbox;
        public WebBrowser GetWebBrowser(Grid grid, String name) => grid.FindName(name) as WebBrowser;
        public ComboBox GetComboBox(Grid grid, String name) => grid.FindName(name) as ComboBox;
        public FullScreenMenu GetFullScreenMenu(Grid grid, String name) => grid.FindName(name) as FullScreenMenu;
        public WrapPanel GetWrapPanel(Grid grid, String name) => grid.FindName(name) as WrapPanel;
        public PersonalAccount GetPersonalAccount(Grid grid, String name) => grid.FindName(name) as PersonalAccount;
    }
}

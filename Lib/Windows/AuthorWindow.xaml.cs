using Lib.ViewModel;
using System.Windows;

namespace Lib.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorWindow.xaml
    /// </summary>
    public partial class AuthorWindow : Window
    {
        public AuthorWindow(AuthorWindowViewModel authorWindowViewModel)
        {
            InitializeComponent();

            authorWindowViewModel.ExitCommand = new Commands.RelayCommand(x => this.Close());

            this.DataContext = authorWindowViewModel;
        }
    }
}

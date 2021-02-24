using Lib.ViewModel;
using System.Windows;

namespace Lib.Windows
{
    /// <summary>
    /// Логика взаимодействия для BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        public BookWindow(BookWindowViewModel bookWindowViewModel)
        {
            InitializeComponent();

            bookWindowViewModel.ExitCommand = new Commands.RelayCommand(x => this.Close());

            this.DataContext = bookWindowViewModel;
        }
    }
}

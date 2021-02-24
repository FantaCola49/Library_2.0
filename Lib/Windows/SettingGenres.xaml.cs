using Lib.ViewModel;
using System.Windows;

namespace Lib.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingGenres.xaml
    /// </summary>
    public partial class SettingGenres : Window
    {
        public SettingGenres(GenreWindowViewModel genreWindowViewModel)
        {
            InitializeComponent();

            this.DataContext = genreWindowViewModel;
        }
    }
}

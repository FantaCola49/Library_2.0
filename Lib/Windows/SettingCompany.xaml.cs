using Lib.ViewModel;
using System.Windows;

namespace Lib.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingCompany.xaml
    /// </summary>
    public partial class SettingCompany : Window
    {
        public SettingCompany(CompaniesWindowViewModel companiesWindowViewModel)
        {
            InitializeComponent();

            this.DataContext = companiesWindowViewModel;
        }
    }
}

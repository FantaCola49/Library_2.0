using Autofac;
using Lib.DataTransfer;
using Lib.ViewModel;
using Lib.Windows;
using System;
using System.Windows;

namespace Lib
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer _container;

        [STAThread]
        static void Main()
        {
            App app = new App();
            app.Run(Build());
        }

        public static MainWindow Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AuthorWindowViewModel>().AsSelf();
            builder.RegisterType<BookWindowViewModel>().AsSelf();
            builder.RegisterType<CompaniesWindowViewModel>().AsSelf();
            builder.RegisterType<GenreWindowViewModel>().AsSelf();

            builder.RegisterType<TransferData>().AsSelf().SingleInstance();
            builder.RegisterType<LibContext>().As<IDbContext>().SingleInstance();

            builder.Register(x => new MainWindowViewModel(x.Resolve<TransferData>(), x.Resolve<IDbContext>()));
            builder.Register(x => new SettingCompany(x.Resolve<CompaniesWindowViewModel>()));
            builder.Register(x => new AuthorWindow(x.Resolve<AuthorWindowViewModel>()));
            builder.Register(x => new BookWindow(x.Resolve<BookWindowViewModel>()));
            builder.Register(x => new SettingGenres(x.Resolve<GenreWindowViewModel>()));

            _container = builder.Build();

            var mainViewModel = _container.Resolve<MainWindowViewModel>();
            mainViewModel.ExitCommand = new Commands.RelayCommand(_ => Environment.Exit(0));

            var view = new MainWindow
            {
                DataContext = mainViewModel
            };

            return view;
        }
    }
}

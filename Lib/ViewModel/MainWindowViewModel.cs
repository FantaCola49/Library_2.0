using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Lib.Commands;
using Lib.Windows;
using Lib.DataTransfer;
using Autofac;
using System.ComponentModel;

namespace Lib.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly mainEntities _dbContext;
        private readonly TransferData _transferData;

        private ObservableCollection<Authors> _authors { get; set; }
        private ObservableCollection<Books> _books { get; set; }

        private Books _selectedBook;
        private Authors _selectedAuthor;
        private string _filterAuthors;
        private string _filterBooks;

        #region BindingsCommands
        public RelayCommand DeleteBookCommand { get; private set; }
        public RelayCommand DeleteAuthorCommand { get; private set; }
        public RelayCommand AddBookCommand { get; private set; }
        public RelayCommand AddAuthorCommand { get; private set; }
        public RelayCommand EditBookCommand { get; private set; }
        public RelayCommand EditAuthorCommand { get; private set; }
        public RelayCommand GetInfoAboutApp { get; set; }
        public RelayCommand SettingCompaniesCommand { get; private set; }
        public RelayCommand SettingGenresCommand { get; private set; }
        public RelayCommand ExitCommand { get; set; }
        #endregion

        #region BindingsProperty
        public ObservableCollection<Authors> FilteredAuthors
        {
            get
            {
                if (string.IsNullOrEmpty(FilterAuthors)) return _authors;
                return new ObservableCollection<Authors>(_authors
                    .Where(x =>
                    (x.Surname + " " + x.Name + " " + ((string.IsNullOrEmpty(x.Patronymic)) ? string.Empty : x.Patronymic)).Contains(FilterAuthors) ||
                    (x.Surname + " " + x.Name[0] + ". " + ((string.IsNullOrEmpty(x.Patronymic)) ? "" : x.Patronymic[0] + ".")).Contains(FilterAuthors)));
            }
        }

        public string FilterAuthors
        {
            get { return _filterAuthors; }
            set
            {
                _filterAuthors = value;
                OnPropertyChanged("FilteredAuthors");
            }
        }

        public ObservableCollection<Books> FilteredBooks
        {
            get
            {
                if (string.IsNullOrEmpty(FilterBooks)) return _books;
                return new ObservableCollection<Books>(_books
                    .Where(x => x.Name.ToLower().Contains(FilterBooks.ToLower())
                    || (x.ISBN != null && x.ISBN.ToLower().Contains(FilterBooks.ToLower()))));
            }
        }
        public string FilterBooks
        {
            get { return _filterBooks; }
            set
            {
                _filterBooks = value;
                OnPropertyChanged("FilteredBooks");
                OnPropertyChanged("FilterBooks");
            }
        }

        public Authors SelectedAuthor
        {
            get
            { return _selectedAuthor; }
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged("SelectedAuthor");
                _books.Clear();
                if (_selectedAuthor != null)
                {
                    _dbContext.Authors.FirstOrDefault(x => x.ID_Author == _selectedAuthor.ID_Author).Books.ToList().ForEach(x => _books.Add(x));
                    FilterBooks = "";
                }
            }
        }
        public Books SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
        #endregion

        public MainWindowViewModel(TransferData transferData, IDbContext dbContext)
        {
            _dbContext = dbContext.DbContext;
            _transferData = transferData ?? throw new ArgumentNullException(nameof(transferData));

            _authors = new ObservableCollection<Authors>();
            FilterAuthors = "";
            _books = new ObservableCollection<Books>();
            FilterBooks = "";

            DeleteBookCommand = new RelayCommand(DeleteBook, x => SelectedBook != null);
            AddBookCommand = new RelayCommand(AddBook, x => SelectedAuthor != null);
            EditBookCommand = new RelayCommand(EditBook, x => SelectedBook != null);

            GetInfoAboutApp = new RelayCommand(x => MessageBox.Show("App.xaml.cs\n" +
                "Курсовой проект.\n" +
                "Корпоративная библиотека предприятия.\n" +
                "Студент группы МОИС-451, Неретин Е.М. 2021 год.\n"));

            DeleteAuthorCommand = new RelayCommand(DeleteAuthor, x => x != null);
            AddAuthorCommand = new RelayCommand(AddAuthor);
            EditAuthorCommand = new RelayCommand(EditAuthor, x => x != null);

            SettingCompaniesCommand = new RelayCommand(SettingCompanies);
            SettingGenresCommand = new RelayCommand(SettingGenre);

            RefreshListAuthors();
        }

        #region Setting 
        private void SettingGenre(object obj)
        {
            App._container.Resolve<SettingGenres>().ShowDialog();
        }

        private void SettingCompanies(object obj)
        {
            App._container.Resolve<SettingCompany>().ShowDialog();
        }
        #endregion

        #region GetData
        private void RefreshListAuthors()
        {
            _authors.Clear();
            GetAuthorsAndBooks().ForEach(x => _authors.Add(x));
        }

        private List<Lib.Authors> GetAuthorsAndBooks()
        {
            try
            {
                var result = _dbContext.Authors.ToList();
                return result;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
        }
        #endregion

        #region Book Methods
        private void EditBook(object obj)
        {
            if (SelectedBook != null)
            {
                _transferData.ID_Author = SelectedAuthor.ID_Author;
                _transferData.ID_Book = SelectedBook.ID_Book;

                var temp = _dbContext.Books.SingleOrDefault(x => x.ID_Book == _transferData.ID_Book);
                var tempBook = new Books()
                {
                    ID_Book = temp.ID_Book,
                    Name = temp.Name,
                    ID_Company = temp.ID_Company,
                    Year = (temp.Year is null) ? null : temp.Year,
                    ISBN = temp.ISBN,
                    Description = temp.Description,
                    ID_Genre = temp.ID_Genre
                };

                App._container.Resolve<BookWindow>().ShowDialog();

                var editBook = _dbContext.Books.SingleOrDefault(x => x.ID_Book == _transferData.ID_Book);
                SelectedBook = null;

                SelectedBook = editBook;
            }
        }

        private void AddBook(object obj)
        {
            _transferData.ID_Author = SelectedAuthor.ID_Author;
            _transferData.ID_Book = null;

            App._container.Resolve<BookWindow>().ShowDialog();

            if (_transferData.ID_Book != null)
            {
                var newBook = _dbContext.Books.Where(x => x.ID_Book == _transferData.ID_Book).FirstOrDefault();
                if (newBook != null)
                {
                    SelectedAuthor = SelectedAuthor;

                    SelectedBook = null;
                    SelectedBook = newBook;
                }
            }
        }

        private void DeleteBook(object obj)
        {
            var result = MessageBox.Show("Удалить книгу?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                var book = obj as Lib.Books;

                if (book != null)
                {
                    _dbContext.Books.Remove(_dbContext.Books.Where(x => String.Equals(x.ID_Book, book.ID_Book)).First());
                    _dbContext.SaveChanges();

                    SelectedAuthor = SelectedAuthor;
                }
            }
        }
        #endregion

        #region Author Methods
        private void AddAuthor(object obj)
        {
            _transferData.ID_Author = null;
            App._container.Resolve<AuthorWindow>().ShowDialog();

            var newAuthor = _dbContext.Authors.ToList().Except(_authors.ToList()).FirstOrDefault();
            if (newAuthor != null)
                _authors.Add(newAuthor);
        }

        private void EditAuthor(object obj)
        {
            if (SelectedAuthor != null)
            {
                _transferData.ID_Author = SelectedAuthor.ID_Author;

                var temp = _dbContext.Authors.SingleOrDefault(x => x.ID_Author == _transferData.ID_Author);
                var tempAuthor = new Authors
                {
                    ID_Author = temp.ID_Author,
                    Name = temp.Name,
                    Surname = temp.Surname,
                    Patronymic = temp.Patronymic,
                    Nickname = (temp.Nickname is null) ? "" : temp.Nickname.ToString()
                };

                App._container.Resolve<AuthorWindow>().ShowDialog();
            }
        }

        public void DeleteAuthor(object obj)
        {
            var result = MessageBox.Show("Удалить Автора и книги, к которым он причастен?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                var author = SelectedAuthor;

                // Удаление из 1:M
                _dbContext.Books.RemoveRange(author.Books);

                // Удаление из M:M
                _dbContext.Authors.First(x => x.ID_Author == author.ID_Author).Books.Clear();
                _dbContext.Authors.Remove(author);

                _dbContext.SaveChanges();
                RefreshListAuthors();
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
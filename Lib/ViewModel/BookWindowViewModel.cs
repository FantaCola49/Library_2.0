using Lib.Commands;
using Lib.DataTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Lib.ViewModel
{
    public class BookWindowViewModel : INotifyPropertyChanged
    {
        private readonly mainEntities _dbContext;
        private readonly TransferData _transferData;
        private Genres _selectedGenre;
        private Company _selectedCompany;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #region Commands
        public RelayCommand AcceptCommand { get; private set; }
        public RelayCommand ExitCommand { get; set; }
        #endregion

        public string Title { get; set; }
        public Books Book { get; private set; }

        public List<Genres> Genres { get; private set; }
        public List<Company> Companies { get; private set; }

        public Genres SelectedGenre
        {
            get { return _selectedGenre; }
            set
            {
                _selectedGenre = value;
                OnPropertyChanged("SelectedGenre");
            }
        }
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                OnPropertyChanged("SelectedCompany");
            }
        }

        public BookWindowViewModel(TransferData transferData, IDbContext dbContext)
        {
            _dbContext = dbContext.DbContext;
            _transferData = transferData ?? throw new ArgumentNullException(nameof(transferData));

            Genres = _dbContext.Genres.ToList();
            Companies = _dbContext.Company.ToList();

            Books book = _dbContext.Books.Where(x => x.ID_Book == transferData.ID_Book).SingleOrDefault();

            if (book is null)
            {
                AcceptCommand = new RelayCommand(AddBook);
                Book = new Books();
                Title = "Книга | Добавление";
            }
            else
            {
                AcceptCommand = new RelayCommand(EditBook);
                Title = "Книга | Изменение";

                Book = new Books()
                {
                    ID_Book = book.ID_Book.ToString(),
                    Name = book.Name.ToString(),
                    ID_Company = (book.ID_Company is null) ? null : book.ID_Company.ToString(),
                    Year = (book.Year is null) ? null : book.Year,
                    ISBN = (book.ISBN is null) ? "" : book.ISBN,
                    Description = (book.Description is null) ? "" : book.Description,
                    ID_Genre = (book.ID_Genre is null) ? null : book.ID_Genre.ToString()
                };

                var resGenre = _dbContext.Genres.Where(x => x.ID_Genre == Book.ID_Genre).FirstOrDefault();
                var resCom = _dbContext.Company.Where(x => x.ID_Company == Book.ID_Company).FirstOrDefault();

                if (resGenre != null)
                    SelectedGenre = resGenre;

                if (resCom != null)
                    SelectedCompany = resCom;
            }
        }

        private void AddBook(object obj)
        {
            Books book = new Books()
            {
                ID_Book = Guid.NewGuid().ToString(),
                Name = Book.Name,
                ID_Company = (SelectedCompany is null) ? null : SelectedCompany.ID_Company,
                Year = (Book.Year is null) ? null : Book.Year,
                ISBN = Book.ISBN,
                Description = Book.Description,
                ID_Genre = (SelectedGenre is null) ? null : SelectedGenre.ID_Genre
            };

            try
            {
                _dbContext.Books.Add(book);
                _dbContext.Authors.SingleOrDefault(x => x.ID_Author == _transferData.ID_Author).Books.Add(book);
                _dbContext.SaveChanges();

                _transferData.ID_Book = book.ID_Book;

                ExitCommand.Execute();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void EditBook(object obj)
        {
            var oldBook = _dbContext.Books.Where(x => x.ID_Book == Book.ID_Book).SingleOrDefault();

            if (oldBook != null)
            {
                oldBook.Name = Book.Name;
                oldBook.ID_Company = (SelectedCompany is null) ? null : SelectedCompany.ID_Company;
                oldBook.Year = (Book.Year is null) ? null : Book.Year;
                oldBook.ISBN = Book.ISBN;
                oldBook.Description = Book.Description;
                oldBook.ID_Genre = (SelectedGenre is null) ? null : SelectedGenre.ID_Genre;
            };

            _dbContext.SaveChanges();

            ExitCommand.Execute();
        }
    }
}
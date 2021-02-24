using Lib.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Lib.ViewModel
{
    public class GenreWindowViewModel : INotifyPropertyChanged
    {
        private readonly mainEntities _dbContext;

        private ObservableCollection<Genres> _genres;

        private string _filterGenre;
        private Genres _selectedGenre;
        private Genres _newGenre;

        #region BindingsCommands
        public RelayCommand DeleteGenreCommand { get; private set; }
        public RelayCommand EditGenreCommand { get; private set; }
        public RelayCommand AddGenreCommand { get; private set; }
        #endregion

        #region BindingProperty
        public ObservableCollection<Genres> FilteredGenres
        {
            get
            {
                if (string.IsNullOrEmpty(FilterGenre)) return _genres;
                return new ObservableCollection<Genres>(_genres
                    .Where(x =>
                    (x.Name.ToLower()).Contains(FilterGenre.ToLower())));
            }
            set
            {
                _genres = value;
                OnPropertyChanged("FilteredGenres");
            }
        }
        public string FilterGenre
        {
            get { return _filterGenre; }
            set
            {
                _filterGenre = value;
                OnPropertyChanged("FilteredGenres");
            }
        }
        #endregion


        public Genres NewGenre
        {
            get { return _newGenre; }
            set
            {
                _newGenre = value;
                OnPropertyChanged("NewGenre");
            }
        }
        public Genres SelectedGenre
        {
            get
            {
                if (_selectedGenre != null)
                {
                    return _dbContext.Genres.FirstOrDefault(x => x.ID_Genre == _selectedGenre.ID_Genre);
                }
                return null;
            }
            set
            {
                _selectedGenre = value;
                // Локальное сохранение ?????
                _dbContext.SaveChanges();
                OnPropertyChanged("SelectedGenre");
            }
        }

        public GenreWindowViewModel(IDbContext dbContext)
        {
            _dbContext = dbContext.DbContext;

            _newGenre = new Genres();
            _genres = new ObservableCollection<Genres>(_dbContext.Genres.ToList());
            FilterGenre = "";

            DeleteGenreCommand = new RelayCommand(DeleteGenre, x => x != null);
            EditGenreCommand = new RelayCommand(EditGenre, x => x != null);
            AddGenreCommand = new RelayCommand(AddGenre);
        }

        #region Genre Methods
        private void AddGenre(object obj)
        {
            if (NewGenre != null)
            {
                var newGenre = new Genres
                {
                    ID_Genre = Guid.NewGuid().ToString(),
                    Name = _newGenre.Name,
                };

                _dbContext.Genres.Add(newGenre);
                _dbContext.SaveChanges();

                SelectedGenre = newGenre;
                NewGenre = null;
                _genres.Add(_dbContext.Genres.ToList().Except(_genres.ToList()).FirstOrDefault());

                MessageBox.Show("Готово");
            }
        }

        private void EditGenre(object obj)
        {
            if (SelectedGenre != null)
            {
                var oldGenre = _dbContext.Genres.FirstOrDefault(x => x.ID_Genre == SelectedGenre.ID_Genre);
                _dbContext.Genres.Attach(oldGenre);
                _dbContext.SaveChanges();

                MessageBox.Show("Готово");
            }
        }

        private void DeleteGenre(object obj)
        {
            if (SelectedGenre != null)
            {
                var company = _dbContext.Genres.FirstOrDefault(x => x.ID_Genre == SelectedGenre.ID_Genre);
                _dbContext.Genres.Remove(company);
                _dbContext.SaveChanges();

                MessageBox.Show("Готово");

                FilteredGenres = new ObservableCollection<Genres>(_dbContext.Genres.ToList());
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
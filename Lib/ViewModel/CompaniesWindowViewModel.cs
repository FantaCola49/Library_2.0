using Lib.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Lib.ViewModel
{
    public class CompaniesWindowViewModel : INotifyPropertyChanged
    {
        private readonly mainEntities _dbContext;

        private ObservableCollection<Company> _companies;

        private string _filterCompany;
        private Company _selectedCompany;
        private Company _newCompany;

        #region BindingsCommands
        public RelayCommand DeleteCompanyCommand { get; private set; }
        public RelayCommand EditCompanyCommand { get; private set; }
        public RelayCommand AddCompanyCommand { get; private set; }
        #endregion

        #region BindingProperty
        public ObservableCollection<Company> FilteredCompanies
        {
            get
            {
                if (string.IsNullOrEmpty(FilterCompany)) return _companies;
                return new ObservableCollection<Company>(_companies
                    .Where(x =>
                    (x.Name.ToLower()).Contains(FilterCompany.ToLower())));
            }
            set
            {
                _companies = value;
                OnPropertyChanged("FilteredCompanies");
            }
        }
        public string FilterCompany
        {
            get { return _filterCompany; }
            set
            {
                _filterCompany = value;
                OnPropertyChanged("FilteredCompanies");
            }
        }
        #endregion


        public Company NewCompany
        {
            get { return _newCompany; }
            set
            {
                _newCompany = value;
                OnPropertyChanged("NewCompany");
            }
        }
        public Company SelectedCompany
        {
            get
            {
                if (_selectedCompany != null)
                {
                    return _dbContext.Company.FirstOrDefault(x => x.ID_Company == _selectedCompany.ID_Company);
                }
                return null;
            }
            set
            {
                _selectedCompany = value;
                _dbContext.SaveChanges();
                OnPropertyChanged("SelectedCompany");
            }
        }

        public CompaniesWindowViewModel(IDbContext dbContext)
        {
            _dbContext = dbContext.DbContext;

            _newCompany = new Company();
            _companies = new ObservableCollection<Company>(_dbContext.Company.ToList());
            FilterCompany = "";

            DeleteCompanyCommand = new RelayCommand(DeleteCompany, x => x != null);
            EditCompanyCommand = new RelayCommand(EditCompany, x => x != null);
            AddCompanyCommand = new RelayCommand(AddCompany);
        }

        #region Company Methods
        private void AddCompany(object obj)
        {
            if (NewCompany != null)
            {
                var newCompany = new Company
                {
                    ID_Company = Guid.NewGuid().ToString(),
                    Name = _newCompany.Name,
                    Address = _newCompany.Address,
                    Mail = _newCompany.Mail,
                    PhoneNumber = _newCompany.PhoneNumber
                };

                _dbContext.Company.Add(newCompany);
                _dbContext.SaveChanges();

                SelectedCompany = newCompany;
                NewCompany = null;
                _companies.Add(_dbContext.Company.ToList().Except(_companies.ToList()).FirstOrDefault());

                MessageBox.Show("Готово");
            }
        }

        private void EditCompany(object obj)
        {
            if (SelectedCompany != null)
            {
                var oldCompany = _dbContext.Company.FirstOrDefault(x => x.ID_Company == SelectedCompany.ID_Company);
                _dbContext.Company.Attach(oldCompany);
                _dbContext.SaveChanges();

                MessageBox.Show("Готово");
            }
        }

        private void DeleteCompany(object obj)
        {
            if (SelectedCompany != null)
            {
                var company = _dbContext.Company.FirstOrDefault(x => x.ID_Company == SelectedCompany.ID_Company);
                _dbContext.Company.Remove(company);
                _dbContext.SaveChanges();

                MessageBox.Show("Готово");

                FilteredCompanies = new ObservableCollection<Company>(_dbContext.Company.ToList());
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

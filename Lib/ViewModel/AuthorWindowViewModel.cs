using Lib.Commands;
using Lib.DataTransfer;
using System;
using System.Linq;
using System.Windows;

namespace Lib.ViewModel
{
    public class AuthorWindowViewModel
    {
        private readonly mainEntities _dbContext;
        private readonly TransferData _transferData;

        #region Commands
        public RelayCommand AcceptCommand { get; private set; }
        public RelayCommand ExitCommand { get; set; }
        #endregion

        public string Title { get; set; }
        public Authors Author { get; private set; }

        public AuthorWindowViewModel(TransferData transferData, IDbContext dbContext)
        {
            _dbContext = dbContext.DbContext;
            _transferData = transferData ?? throw new ArgumentNullException(nameof(transferData));

            Authors author = _dbContext.Authors.Where(x => x.ID_Author == transferData.ID_Author).SingleOrDefault();

            if (author is null)
            {
                AcceptCommand = new RelayCommand(AddAuthor);
                Author = new Authors();
                Title = "Автор | Добавление";
            }
            else
            {
                AcceptCommand = new RelayCommand(EditAuthor);
                Title = "Автор | Изменение";

                Author = new Authors()
                {
                    ID_Author = author.ID_Author.ToString(),
                    Name = author.Name.ToString(),
                    Surname = author.Surname.ToString(),
                    Patronymic = (author.Patronymic is null) ? "" : author.Patronymic.ToString(),
                    Nickname = (author.Nickname is null) ? "" : author.Nickname.ToString()
                };
            }
        }

        private void AddAuthor(object obj)
        {
            Authors author = new Authors()
            {
                ID_Author = Guid.NewGuid().ToString(),
                Name = Author.Name,
                Surname = Author.Surname,
                Patronymic = (Author.Patronymic is null) ? "" : Author.Patronymic,
                Nickname = (Author.Nickname is null) ? "" : Author.Nickname
            };

            try
            {
                _dbContext.Authors.Add(author);
                _dbContext.SaveChanges();

                _transferData.ID_Author = author.ID_Author;

                ExitCommand.Execute();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void EditAuthor(object obj)
        {
            var oldAuthor = _dbContext.Authors.Where(x => String.Equals(x.ID_Author, _transferData.ID_Author)).SingleOrDefault();

            if (oldAuthor != null)
            {
                oldAuthor.Name = Author.Name;
                oldAuthor.Surname = Author.Surname;
                oldAuthor.Patronymic = Author.Patronymic;
                oldAuthor.Nickname = Author.Nickname;

                _dbContext.SaveChanges();

                ExitCommand.Execute();
            }
        }
    }
}
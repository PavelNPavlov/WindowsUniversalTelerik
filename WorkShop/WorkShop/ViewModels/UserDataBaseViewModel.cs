using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkShop.Commands;
using WorkShop.Extentions;

namespace WorkShop.ViewModels
{
    public class UserDataBaseViewModel : BaseViewModel
    {
        private ObservableCollection<UserViewModel> users;
        private ICommand showAll;
        private ICommand showRegistration;
        private ICommand showLogIn;
        private ICommand registerCommand;
        private ICommand logInCommand;
        private bool displayUsers;
        private bool displayRegisterForm;
        private bool displayLogInForm;
        public bool showRegistrationError;
        public bool showLogInError;
        private UserViewModel userToBeRegistered;
        private UserViewModel userToBeLoggedIn;


        public UserDataBaseViewModel()
        {
            this.SeedUsers();
        }

        public bool DisplayeUsers
        {
            get
            {
                return displayUsers;
            }
            set
            {
                //check if the value is currently the same, and do nothing if true
                if (this.displayUsers == value)
                {
                    return;
                }

                this.displayUsers = value;
                this.OnPropertyChanged("DisplayeUsers");
            }
        }

        public bool DisplayRegisterForm
        {
            get
            {
                return displayRegisterForm;
            }
            set
            {
                //check if the value is currently the same, and do nothing if true
                if (this.displayRegisterForm == value)
                {
                    return;
                }

                this.displayRegisterForm = value;
                this.OnPropertyChanged("DisplayRegisterForm");
            }
        }

        public bool DisplayLogInForm
        {
            get
            {
                return displayLogInForm;
            }
            set
            {
                //check if the value is currently the same, and do nothing if true
                if (this.displayLogInForm == value)
                {
                    return;
                }

                this.displayLogInForm = value;
                this.OnPropertyChanged("DisplayLogInForm");
            }
        }

        public bool DisplayRegistrationErr
        {
            get
            {
                return this.showRegistrationError;
            }
            set
            {
                if(this.showRegistrationError == value)
                {
                    return;
                }

                this.showRegistrationError = value;
                this.OnPropertyChanged("DisplayRegistrationErr");
            }
        }

        public bool DisplayLogInErr
        {
            get
            {
                return this.showLogInError;
            }
            set
            {
                if (this.showLogInError == value)
                {
                    return;
                }

                this.showLogInError = value;
                this.OnPropertyChanged("DisplayLogInErr");
            }
        }

        public ICommand ShowAll
        {
            get
            {
                if (this.showAll == null)
                {
                    this.showAll = new DelegateCommand(this.ShowAllUsers);
                }
                return showAll;
            }
        }

        public ICommand ShowRegistration
        {
            get
            {
                if (this.showRegistration == null)
                {
                    this.showRegistration = new DelegateCommand(this.ShowRegistrationForm);
                }

                return this.showRegistration;
            }
        }

        public ICommand ShowLogIn
        {
            get
            {
                if (this.showLogIn == null)
                {
                    this.showLogIn = new DelegateCommand(this.ShowLogin);
                }

                return this.showLogIn;
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                if (this.registerCommand == null)
                {
                    this.registerCommand = new DelegateCommand(this.RegisterOnButtonClick);
                }

                return this.registerCommand;
            }
        }

        public ICommand LogInCommand
        {
            get
            {
                if (this.logInCommand == null)
                {
                    this.logInCommand = new DelegateCommand(this.LogInOnButtonClick);
                }

                return this.logInCommand;
            }
        }

        public UserViewModel UserToBeRegistered
        {
            get
            {
                if (this.userToBeRegistered == null)
                {
                    this.userToBeRegistered = new UserViewModel();
                }

                return this.userToBeRegistered;
            }
            set
            {
                this.userToBeRegistered = value;
                this.OnPropertyChanged("UserToBeRegistered");
            }
        }

        public UserViewModel UserToBeLoggedIn
        {
            get
            {
                if (this.userToBeLoggedIn == null)
                {
                    this.userToBeLoggedIn = new UserViewModel();
                }

                return this.userToBeLoggedIn;
            }
            set
            {
                this.userToBeLoggedIn = value;
                this.OnPropertyChanged("UserToBeLoggedIn");
            }
        }
        public IEnumerable<UserViewModel> Users
        {
            get
            {
                if (this.users == null)
                {
                    this.users = new ObservableCollection<UserViewModel>();
                }

                return users;
            }
            set
            {
                if (this.users == null)
                {
                    this.users = new ObservableCollection<UserViewModel>();
                }

                this.users.Clear();
                value.ForEach(users.Add);
            }
        }

        public void Register()
        {
            this.users.Add(this.UserToBeRegistered);
            this.DisplayRegisterForm = false;
        }

        private void ShowAllUsers()
        {
            this.HideAll();
            this.DisplayeUsers = true;
        }

        private void ShowRegistrationForm()
        {
            this.HideAll();
            this.DisplayRegisterForm = true;
            this.UserToBeRegistered = new UserViewModel();
        }

        private void ShowLogin()
        {
            this.HideAll();
            this.DisplayLogInForm = true;
            this.UserToBeLoggedIn = new UserViewModel();
        }

        private void LogInOnButtonClick()
        {
            if (!this.ValidateLogIn())
            {
                this.DisplayLogInErr = true;
                return;
            }
            this.HideAll();
        }

        private bool ValidateLogIn()
        {
            var user = this.UserToBeLoggedIn;

            if (user.UserName == null || user.Password == null)
            {
                return false;
            }

            return this.Users.Any(x => x.UserName == user.UserName && x.Password == user.Password);
        }
        private void RegisterOnButtonClick()
        {
            if (this.users == null)
            {
                this.users = new ObservableCollection<UserViewModel>();
            }

            if(!this.RegistrationValidation())
            {
                this.DisplayRegistrationErr = true;
                return;
            }
            var user = UserViewModel.FromUser(this.UserToBeRegistered);
            this.users.Add(user);

            this.DisplayRegisterForm = false;
        }

        private bool RegistrationValidation()
        {
            //check for nulls
            if(this.UserToBeRegistered.UserName==null || this.UserToBeRegistered.Password == null)
            {
                return false;
            }
            //che for lenght
            if(this.UserToBeRegistered.UserName.Length<3 || this.UserToBeRegistered.Password.Length<3 
                || this.UserToBeRegistered.UserName.Length>10 || this.UserToBeRegistered.Password.Length>10)
            {
                return false;
            }
            //check for content
            return this.UserToBeRegistered.UserName.All(char.IsLetterOrDigit) && this.UserToBeRegistered.Password.All(char.IsLetterOrDigit);
        }

        private void SeedUsers()
        {
            var users = new ObservableCollection<UserViewModel>();
            users.Add(new UserViewModel
            {
                UserName = "ArgiDux",
                Password = "12345",
                ImageUrl = "http://screenrant.com/wp-content/uploads/obi-wan-kenobi-star-wars-alec-guiness.jpg"
            });

            users.Add(new UserViewModel
            {
                UserName = "TheBest",
                Password = "12345",
                ImageUrl = "http://screenrant.com/wp-content/uploads/obi-wan-kenobi-star-wars-alec-guiness.jpg"
            });


            this.Users = users;
        }

        private void HideAll()
        {
            this.DisplayLogInForm = false;
            this.DisplayRegisterForm = false;
            this.DisplayeUsers = false;
            this.DisplayRegistrationErr = false;
            this.DisplayLogInErr = false;
        }

       
    }
}

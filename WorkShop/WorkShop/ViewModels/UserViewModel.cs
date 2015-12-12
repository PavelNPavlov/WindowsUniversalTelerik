using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShop.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private string userName;
        private string password;
        private string imageUrl;

        public string UserName {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
                this.OnPropertyChanged("UserName");
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                this.OnPropertyChanged("Password");
            }
        }

        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }
            set
            {
                this.imageUrl = value;
                this.OnPropertyChanged("ImageUrl");
            }
        }

        public static UserViewModel FromUser(UserViewModel user)
        {
            return new UserViewModel
            {
                UserName = user.UserName,
                Password = user.Password,
                ImageUrl = "http://schmoesknow.com/wp-content/uploads/2013/08/obiwankenobi.jpeg"
            };
        }
    }
}

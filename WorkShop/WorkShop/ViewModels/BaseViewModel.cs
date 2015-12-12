

namespace WorkShop.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if(this.PropertyChanged == null)
            {
                return;
            }
            var propertyEventArgs = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged.Invoke(this, propertyEventArgs);
        }
    }
}

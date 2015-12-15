
namespace WorkShop2.ViewModels
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using System;
    using Windows.Devices.Sensors;
    using Windows.Foundation;
    using System.Threading.Tasks;
    using Windows.UI.Core;

    public class MainPageViewModel : BaseViewModel
    {
        private double angle;

        public double Angle
        {
            get
            {
                return this.angle;
            }
            set
            {
                this.angle = value;
                this.OnPropertyChanged("Angle");
            }
        }


    }
}
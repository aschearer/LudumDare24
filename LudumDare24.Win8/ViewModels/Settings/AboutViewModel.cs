using System;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LudumDare24.Models;

namespace LudumDare24.Win8.ViewModels.Settings
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isVisible;

        public AboutViewModel()
        {
            this.LearnMoreCommand = new RelayCommand(this.LearnMore);
        }

        private void LearnMore()
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(Constants.SpottedZebraUrl));
        }

        public ICommand LearnMoreCommand { get; private set; }

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                if (this.isVisible != value)
                {
                    this.isVisible = value;
                    this.OnPropertyChanged("IsVisible");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
using LudumDare24.Win8.ViewModels.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LudumDare24.Win8.Views.Settings
{
    public sealed partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            this.InitializeComponent();
        }

        private AboutViewModel ViewModel
        {
            get { return (AboutViewModel)this.DataContext; }
        }

        private void LearnMoreClicked(object sender, RoutedEventArgs e)
        {
            this.ViewModel.LearnMoreCommand.Execute(null);
        }
    }
}

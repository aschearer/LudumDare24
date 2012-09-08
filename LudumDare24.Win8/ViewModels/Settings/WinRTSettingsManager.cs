using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LudumDare24.Models;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;

namespace LudumDare24.Win8.ViewModels.Settings
{
    public class WinRTSettingsManager
    {
        private readonly AboutViewModel aboutViewModel;

        public WinRTSettingsManager(AboutViewModel aboutViewModel)
        {
            ICommand command = new RelayCommand(this.ReturnToSettings);
            this.aboutViewModel = aboutViewModel;
        }

        public int Initialize()
        {
            SettingsPane.GetForCurrentView().CommandsRequested += this.OnSettingsRequested;
            return 1;
        }

        public void ShowSettings()
        {
            SettingsPane.Show();
        }

        public void ReturnToSettings()
        {
            SettingsPane.Show();
        }

        private void OnSettingsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand aboutCommand = new SettingsCommand("AboutCommand", "About", this.ShowAbout);
            SettingsCommand privacyCommand = new SettingsCommand("PrivacyCommand", "Privacy Policy", this.ShowPrivacy);
            args.Request.ApplicationCommands.Add(aboutCommand);
            args.Request.ApplicationCommands.Add(privacyCommand);
        }

        private async void ShowPrivacy(IUICommand command)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(Constants.PrivacyUrl));
        }

        private void ShowAbout(IUICommand command)
        {
            this.aboutViewModel.IsVisible = true;
        }

        public void ShowAbout()
        {
            SettingsPane.Show();
        }
    }
}

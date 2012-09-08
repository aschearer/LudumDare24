using System.ComponentModel;
using Callisto.Controls;
using LudumDare24.Win8.ViewModels.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace LudumDare24.Win8.Views.Settings
{
    public class AboutView
    {
        private readonly AboutViewModel viewModel;
        private readonly AboutControl aboutControl;
        private SettingsFlyout flyout;

        public AboutView(AboutViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += this.OnVisibilityChanged;
            this.aboutControl = new AboutControl() { DataContext = this.viewModel };
        }

        public bool IsVisible
        {
            get { return this.viewModel.IsVisible; }
        }

        private void OnVisibilityChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.viewModel.IsVisible)
            {
                this.flyout = new SettingsFlyout();
                this.flyout.HeaderText = "About";
                this.flyout.HeaderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 115, 125, 214));
                this.flyout.Background = new SolidColorBrush(Colors.White);
                this.flyout.Content = this.aboutControl;
                this.flyout.IsOpen = true;
                this.flyout.Closed += this.OnFlyoutClosed;
            }
            else
            {
                this.flyout.IsOpen = false;
                this.flyout.Closed -= this.OnFlyoutClosed;
                this.flyout = null;
            }
        }

        private void OnFlyoutClosed(object sender, object e)
        {
            this.viewModel.IsVisible = false;
        }

        public void LoadContent(ContentManager content)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
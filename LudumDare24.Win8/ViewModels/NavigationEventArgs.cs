using System;

namespace LudumDare24.ViewModels
{
    public class NavigationEventArgs : EventArgs
    {
        public NavigationEventArgs(Type targetViewModel)
        {
            this.TargetViewModel = targetViewModel;
        }

        public Type TargetViewModel { get; private set; }
    }
}
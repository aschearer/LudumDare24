using System;

namespace LudumDare24.ViewModels
{
    public class ConductorViewModel : IConductorViewModel
    {
        public event EventHandler<NavigationEventArgs> PushViewModel;
        public event EventHandler<EventArgs> PopViewModel;
        public event EventHandler<NavigationEventArgs> SetTopViewModel;

        public ConductorViewModel()
        {
            
        }

        public void Push(Type viewModel)
        {
            this.PushViewModel(this, new NavigationEventArgs(viewModel));
        }

        public void Pop()
        {
            this.PopViewModel(this, new EventArgs());
        }

        public void SetTop(Type viewMode)
        {
            this.SetTopViewModel(this, new NavigationEventArgs(viewMode));
        }
    }
}
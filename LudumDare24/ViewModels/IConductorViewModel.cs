﻿using System;

namespace LudumDare24.ViewModels
{
    public interface IConductorViewModel
    {
        event EventHandler<NavigationEventArgs> PushViewModel;
        event EventHandler<EventArgs> PopViewModel;
        event EventHandler<NavigationEventArgs> SetTopViewModel;

        void Push(Type viewModel);
        void Pop();
        void SetTop(Type viewMode);
    }
}
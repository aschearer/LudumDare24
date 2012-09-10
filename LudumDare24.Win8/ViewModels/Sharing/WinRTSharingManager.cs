using System;
using LudumDare24.Models;
using Windows.ApplicationModel.DataTransfer;

namespace LudumDare24.ViewModels.Sharing
{
    public class WinRTSharingManager
    {
        public void Initialize()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += this.OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            args.Request.Data.Properties.Title = Constants.GameTitle;
            args.Request.Data.SetUri(new Uri(Constants.GameUrl));
        }

    }
}
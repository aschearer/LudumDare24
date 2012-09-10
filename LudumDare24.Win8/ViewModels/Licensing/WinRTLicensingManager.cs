using System;
using Windows.ApplicationModel.Store;

namespace LudumDare24.Win8.ViewModels.Licensing
{
    public class WinRTLicensingManager
    {
        public event EventHandler<EventArgs> LicenseExpired;
        public event EventHandler<EventArgs> ProductPurchased;

        private readonly LicenseInformation licenseInfo;
        private bool isTrial;

        public WinRTLicensingManager(LicenseInformation licenseInfo)
        {
            this.licenseInfo = licenseInfo;
            this.licenseInfo.LicenseChanged += this.OnLicenseChanged;
            this.isTrial = this.licenseInfo.IsTrial;
            this.IsTrialExpired = this.licenseInfo.IsTrial && this.licenseInfo.ExpirationDate == DateTimeOffset.MinValue;
        }

        public bool IsTrialExpired { get; private set; }

        public bool IsPurchasingGame { get; private set; }

        public void CheckForPurchase()
        {
        }

        public void CheckForExpiration()
        {
        }

        public void Expire()
        {
            this.IsTrialExpired = true;
            this.LicenseExpired(this, new EventArgs());
        }

        public async void PurchaseGame()
        {
            this.IsPurchasingGame = true;
            string result = await CurrentApp.RequestAppPurchaseAsync(false);
            this.IsPurchasingGame = false;
        }

        private void OnLicenseChanged()
        {
            if (this.licenseInfo.IsActive)
            {
                if (this.licenseInfo.IsTrial && this.licenseInfo.ExpirationDate == DateTimeOffset.MinValue)
                {
                    this.IsTrialExpired = true;
                    this.LicenseExpired(this, new EventArgs());
                }
                else if (this.isTrial && !this.licenseInfo.IsTrial)
                {
                    this.isTrial = false;
                    this.IsTrialExpired = false;
                    this.ProductPurchased(this, new EventArgs());
                }
            }
        }
    }
}
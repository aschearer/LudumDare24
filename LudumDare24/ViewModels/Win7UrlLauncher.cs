using System;
using System.Diagnostics;

namespace LudumDare24.ViewModels
{
    public class Win7UrlLauncher : IUrlLauncher
    {
        public void OpenUrl(string url)
        {
            Process.Start(url);
        }
    }
}
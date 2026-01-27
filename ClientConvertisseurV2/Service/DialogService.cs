using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.Service
{
    public static class DialogService
    {
        public static async Task ShowErrorAsync(string message, string title = "Erreur")
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK"
            };

            // ✅ Comme demandé par le sujet
            if (App.MainRoot != null)
                dialog.XamlRoot = App.MainRoot.XamlRoot;

            await dialog.ShowAsync();
        }
    }
}

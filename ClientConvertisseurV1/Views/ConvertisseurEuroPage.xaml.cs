using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ClientConvertisseurV1.Views
{
    public sealed partial class ConvertisseurEuroPage : Page
    {
        private readonly IService service;

        public ConvertisseurEuroPage()
        {
            InitializeComponent();

            // ⚠️ Mets ici le PORT de TON API
            // Exemple de l’énoncé : http://localhost:5093/api/
            service = new WSService("http://localhost:5093/api/");

            Loaded += ConvertisseurEuroPage_Loaded;
            BtnConvertir.Click += BtnConvertir_Click;
        }

        private async void ConvertisseurEuroPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ChargerDevisesAsync();
        }

        private async Task ChargerDevisesAsync()
        {
            List<Devise>? devises = await service.GetDevisesAsync("devises");

            if (devises == null || devises.Count == 0)
            {
                await AfficherErreurAsync("Impossible de récupérer les devises depuis l’API.");
                return;
            }

            // Affichage : NomDevise dans la ComboBox
            CmbDevises.ItemsSource = devises;
            CmbDevises.DisplayMemberPath = "NomDevise";
            CmbDevises.SelectedIndex = 0;
        }

        private async void BtnConvertir_Click(object sender, RoutedEventArgs e)
        {
            if (CmbDevises.SelectedItem is not Devise devise)
            {
                await AfficherErreurAsync("Veuillez sélectionner une devise.");
                return;
            }

            // Accepte "100", "100.5", "100,5"
            string input = (TxtMontantEuro.Text ?? "").Trim().Replace(',', '.');

            if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double montantEuro))
            {
                await AfficherErreurAsync("Montant en euros invalide.");
                return;
            }

            // Hypothèse standard : Taux = valeur d'1€ dans la devise
            double resultat = montantEuro * devise.Taux;

            TxtMontantDevise.Text = $"{resultat:0.00}";
        }

        private async Task AfficherErreurAsync(string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Erreur",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }
    }
}

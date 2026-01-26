using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClientConvertisseurV2.ViewModels
{
    public partial class ConvertisseurEuroViewModel : ObservableObject
    {
        private readonly IService service;

        public ObservableCollection<Devise> Devises { get; } = new();

        [ObservableProperty]
        private Devise? deviseSelectionnee;

        [ObservableProperty]
        private string montantEuro = "";

        [ObservableProperty]
        private string montantDevise = "";

        [ObservableProperty]
        private string messageErreur = "";

        public ConvertisseurEuroViewModel()
        {
            // ⚠️ Mets ICI le port de ton API
            service = new WSService("https://localhost:5093/api/");
            _ = ChargerDevisesAsync();
        }

        [RelayCommand]
        public async Task ChargerDevisesAsync()
        {
            MessageErreur = "";

            try
            {
                var devises = await service.GetDevisesAsync("devises");

                if (devises == null || devises.Count == 0)
                {
                    MessageErreur = "Aucune devise reçue (liste vide).";
                    return;
                }

                Devises.Clear();
                foreach (var d in devises) Devises.Add(d);

                DeviseSelectionnee = Devises[0];
            }
            catch (System.Exception ex)
            {
                MessageErreur = ex.Message; // tu verras la vraie cause (SSL/404/etc.)
            }
        }

        [RelayCommand]
        public void Convertir()
        {
            MessageErreur = "";
            MontantDevise = "";

            if (DeviseSelectionnee == null)
            {
                MessageErreur = "Veuillez sélectionner une devise.";
                return;
            }

            var input = (MontantEuro ?? "").Trim().Replace(',', '.');

            if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var euro))
            {
                MessageErreur = "Montant en euros invalide.";
                return;
            }

            var resultat = euro * DeviseSelectionnee.Taux;
            MontantDevise = resultat.ToString("0.00", CultureInfo.InvariantCulture);
        }
    }
}

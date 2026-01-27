using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using ClientConvertisseurV2.Service;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurEuroViewModel : ObservableObject
    {
        private double montantEuro;
        private Devise? deviseSelectionnee;
        private string? montantDevise;

        public ObservableCollection<Devise> Devises { get; set; }
        public IRelayCommand BtnSetConversion { get; }

        public double MontantEuro
        {
            get => montantEuro;
            set => SetProperty(ref montantEuro, value);
        }

        public Devise? DeviseSelectionnee
        {
            get => deviseSelectionnee;
            set => SetProperty(ref deviseSelectionnee, value);
        }

        public string? MontantDevise
        {
            get => montantDevise;
            set => SetProperty(ref montantDevise, value);
        }

        public ConvertisseurEuroViewModel()
        {
            Devises = new ObservableCollection<Devise>();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
            GetDataOnLoadAsync();
        }

        public async void GetDataOnLoadAsync()
        {
            try
            {
                WSService service = new WSService("http://localhost:5093/api/");
                List<Devise>? result = await service.GetDevisesAsync("devises");

                if (result == null || result.Count == 0)
                {
                    await DialogService.ShowErrorAsync("Impossible de récupérer les devises depuis l'API.");
                    return;
                }

                Devises.Clear();
                foreach (var d in result) Devises.Add(d);

                DeviseSelectionnee = Devises[0];
            }
            catch (System.Exception ex)
            {
                await DialogService.ShowErrorAsync($"Erreur lors de l'appel au service : {ex.Message}");
            }
        }

        public async void ActionSetConversion()
        {
            if (DeviseSelectionnee == null)
            {
                await DialogService.ShowErrorAsync("Veuillez sélectionner une devise.");
                return;
            }

            if (MontantEuro <= 0)
            {
                await DialogService.ShowErrorAsync("Veuillez saisir un montant en euros valide.");
                return;
            }

            double resultat = MontantEuro * DeviseSelectionnee.Taux;
            MontantDevise = resultat.ToString("N2");
        }
    }
}
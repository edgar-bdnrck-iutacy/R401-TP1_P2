using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurDeviseEuroViewModel : ObservableObject
    {
        private readonly WSService service;

        public ObservableCollection<Devise> Devises { get; } = new();

        private Devise? _deviseSelectionnee;
        public Devise? DeviseSelectionnee
        {
            get => _deviseSelectionnee;
            set => SetProperty(ref _deviseSelectionnee, value);
        }

        private double _montantDevise;
        public double MontantDevise
        {
            get => _montantDevise;
            set => SetProperty(ref _montantDevise, value);
        }

        private double _montantEuro;
        public double MontantEuro
        {
            get => _montantEuro;
            set => SetProperty(ref _montantEuro, value);
        }

        public IRelayCommand BtnSetConversion { get; }

        public ConvertisseurDeviseEuroViewModel()
        {
            service = new WSService("http://localhost:5093/api/");
            BtnSetConversion = new RelayCommand(ActionSetConversion);
            GetDataOnLoadAsync();
        }

        public async void GetDataOnLoadAsync()
        {
            var result = await service.GetDevisesAsync("devises");
            if (result != null)
            {
                Devises.Clear();
                foreach (var d in result)
                    Devises.Add(d);
            }
        }

        private void ActionSetConversion()
        {
            if (DeviseSelectionnee != null && MontantDevise > 0)
            {
                MontantEuro = MontantDevise / DeviseSelectionnee.Taux;
            }
        }
    }
}

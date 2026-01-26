using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ClientConvertisseurV2.ViewModels;

namespace ClientConvertisseurV2.Views
{
    public sealed partial class ConvertisseurEuroPage : Page
    {
        public ConvertisseurEuroPage()
        {
            InitializeComponent();

            var vm = new ConvertisseurEuroViewModel();
            DataContext = vm;

            Loaded += async (_, __) => await vm.ChargerDevisesAsync();
        }
    }
}

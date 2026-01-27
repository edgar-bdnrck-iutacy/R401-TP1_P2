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

            ConvertisseurEuroViewModel convertisseurEuroViewwModel = new ConvertisseurEuroViewModel();
            DataContext = convertisseurEuroViewwModel;
        }
    }
}

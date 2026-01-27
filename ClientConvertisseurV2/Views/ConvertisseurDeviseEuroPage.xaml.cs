using ClientConvertisseurV2.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ClientConvertisseurV2.Views
{
    public sealed partial class ConvertisseurDeviseEuroPage : Page
    {
        public ConvertisseurDeviseEuroPage()
        {
            this.InitializeComponent();
            DataContext = App.Current.Services.GetService<ConvertisseurDeviseEuroViewModel>();
        }
    }
}

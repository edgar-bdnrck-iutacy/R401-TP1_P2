using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ClientConvertisseurV2.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ClientConvertisseurV2.Views
{
    public sealed partial class ConvertisseurEuroPage : Page
    {
        public ConvertisseurEuroPage()
        {
            InitializeComponent();

            DataContext = App.Current.Services.GetService<ConvertisseurEuroViewModel>();
        }
    }
}

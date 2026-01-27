using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels.Tests
{
    [TestClass()]
    public class ConvertisseurEuroViewModelTests
    {
        /// </summary>
        [TestMethod()]
        public void ConvertisseurEuroViewModelTest()
        {
           ConvertisseurEuroViewModel convertisseurEuro = new
           ConvertisseurEuroViewModel();
           Assert.IsNotNull(convertisseurEuro);
        }

        /// <summary>
        /// Test GetDataOnLoadAsyncTest OK
        /// </summary> 
        [TestMethod()]
        public void GetDataOnLoadAsyncTest_OK()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            //Act
            convertisseurEuro.GetDataOnLoadAsync();
            //Assert
            Assert.IsNotNull(convertisseurEuro.Devises);
        }

        /// <summary>
        /// Test conversion OK
        /// </summary>
        [TestMethod]
        public void ActionSetConversionTest()
        {
            // Arrange
            var vm = new ConvertisseurEuroViewModel();
            vm.MontantEuro = 100;

            Devise devise = new Devise { NomDevise = "Dollar", Taux = 1.07 };
            vm.DeviseSelectionnee = devise;

            // Act (on déclenche la commande, qui appelle ActionSetConversion)
            vm.BtnSetConversion.Execute(null);

            // Assert
            // MontantDevise est une string formatée "N2" => "107,00" en FR
            // Pour éviter les soucis de virgule/point selon la culture :
            var montant = double.Parse(vm.MontantDevise!, CultureInfo.CurrentCulture);
            Assert.AreEqual(107.00, montant, 0.01);
        }
    }
}
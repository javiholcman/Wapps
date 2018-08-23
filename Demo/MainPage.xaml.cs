using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Cases;
using Wapps.Forms;
using Xamarin.Forms;

namespace Demo
{
    public partial class MainPage : Demo.Cases.ContentPageBase
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FUtils.InvokeOnMainThread(200, () =>
            {
                DisplayAlert("Pepe", "Pepep", "Cancelar");
            });
        }
    }
}

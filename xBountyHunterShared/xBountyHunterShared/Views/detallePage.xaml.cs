using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xBountyHunterShared.Extras;
using xBountyHunterShared.Models;

namespace xBountyHunterShared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detallePage : ContentPage
    {
        mFugitivos Fugitivo = new mFugitivos();
        databaseManager DB = new databaseManager();

        public detallePage(mFugitivos fugitivo)
        {
            InitializeComponent();

            Fugitivo = fugitivo;
            Title = fugitivo.Name;
            img.Source = ImageSource.FromFile(fugitivo.Foto);
        }

        async void Beliminar_Clicked(object sender, EventArgs e)
        {
            int result = DB.deleteItem(Fugitivo.ID);
            if (result == 1)
            {
                await DisplayAlert("Eliminado", "El fugitivo " + Fugitivo.Name + " ha sido eliminado", "Aceptar");
            }
            else
            {
                await DisplayAlert("Error", "Error al borrar el fugitivo", "Aceptar");
            }
            DB.closeConnection();
            MessagingCenter.Send<Page>(this, "Update");
            await Navigation.PopAsync();
        }

        async void bmap_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mapPage(Fugitivo));
        }
	}
}
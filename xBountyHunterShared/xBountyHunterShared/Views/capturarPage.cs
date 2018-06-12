using System.Collections.Generic;
using Xamarin.Forms;
using xBountyHunterShared.DependencyServices;
using xBountyHunterShared.Extras;
using xBountyHunterShared.Models;

namespace xBountyHunterShared.Views
{
    public class capturarPage : ContentPage
    {
        mFugitivos Fugitivo = new mFugitivos();
        databaseManager DB = new databaseManager();
        Image img;
        Label fugitivoSuelto;
        Button bcapturar;
        Button beliminar;
        StackLayout imageContainer;
        Button bfoto;
        StackLayout verticalStackLayout;

        string udid;
        string imagePath;

        public capturarPage(mFugitivos fugitivo)
        {
            Fugitivo.Name = fugitivo.Name;
            Fugitivo.ID = fugitivo.ID;

            fugitivoSuelto = new Label
            {
                Text = "El fugitivo sigue suelto ...",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center
            };

            bcapturar = new Button
            {
                Text = "Capturar",
                WidthRequest = 200,
                HorizontalOptions = LayoutOptions.Center
            };

            beliminar = new Button
            {
                Text = "Eliminar",
                BorderColor = Color.Black,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200
            };

            imageContainer = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 100,
                HeightRequest = 100,
                BackgroundColor = Color.Gray
            };

            img = new Image
            {
                Aspect = Aspect.Fill,
                WidthRequest = 100,
                HeightRequest = 100
            };

            bfoto = new Button
            {
                Text = "Tomar Foto",
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200
            };

            verticalStackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Title = Fugitivo.Name;

            imageContainer.Children.Add(img);

            verticalStackLayout.Children.Add(fugitivoSuelto);
			verticalStackLayout.Children.Add(bfoto);
            verticalStackLayout.Children.Add(imageContainer);
            verticalStackLayout.Children.Add(bcapturar);
            verticalStackLayout.Children.Add(beliminar);

            Content = verticalStackLayout;

            bcapturar.IsEnabled = false;

            bcapturar.Clicked += Bcapturar_Clicked;
            beliminar.Clicked += Beliminar_Clicked;
            bfoto.Clicked += Bfoto_Clicked;
        }

        async void Beliminar_Clicked(object sender, System.EventArgs e)
        {
            int result = DB.deleteItem(Fugitivo.ID);
            if(result == 1)
            {
                await DisplayAlert("Eliminado","El fugitivo " + Fugitivo.Name + " ha sido eliminado","Aceptar");
            }
            else
            {
                await DisplayAlert("Error", "Error al borrar el fugitivo", "Aceptar");
            }
            DB.closeConnection();
            MessagingCenter.Send<Page>(this, "Update");
            await Navigation.PopAsync();
        }

        async void Bcapturar_Clicked(object sender, System.EventArgs e)
        {
            webServicesConnection ws = new webServicesConnection(this);
            udid = DependencyService.Get<IUDID>().getUDID();
            Dictionary<string, string> location = DependencyService.Get<IGetLocation>().getLocation();

            Fugitivo.Capturado = true;
            Fugitivo.Foto = imagePath;
            Fugitivo.Lat = location?["Lat"];
            Fugitivo.Lon = location?["Lon"];

            int result = DB.updateItem(Fugitivo);

            string message = ws.connectPOST(udid);
            if (result == 1)
            {
                await DisplayAlert("Capturado", "El fugitivo " + Fugitivo.Name + " ha sido capturado\n" + message, "Aceptar");
            }
            else
            {
                await DisplayAlert("Error", "Error al capturar el fugitivo", "Aceptar");
            }
            DB.closeConnection();
            MessagingCenter.Send<Page>(this, "Update");
            await Navigation.PopAsync();
        }

        async void Bfoto_Clicked(object sender, System.EventArgs e)
        {
            imagePath = await DependencyService.Get<ICamera>().TakePhoto();
            if(imagePath == "" || imagePath == "Cancel" || imagePath == null)
            {
                bcapturar.IsEnabled = false;
            }
            else
            {
                img.Source = ImageSource.FromFile(imagePath);
                bcapturar.IsEnabled = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IGetLocation>().activarGPS();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DependencyService.Get<IGetLocation>().apagarGPS();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using xBountyHunterShared.Extras;
using xBountyHunterShared.Models;

namespace xBountyHunterShared.Views
{
    public class agregarFugitivo : ContentPage
    {
        StackLayout verticalStackLayout;
        StackLayout horizontalStackLayout;
        Button bagregar;
        Button bcancelar;
        Entry enewname;

        public agregarFugitivo()
        {
            verticalStackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            horizontalStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center
            };

            enewname = new Entry
            {
                TextColor = Color.Black,
                BackgroundColor = Color.FromHex("#d3d3d3"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            bagregar = new Button
            {
                Text = "Agregar",
                BorderColor = Color.Black,
                BorderWidth = 1
            };

            bcancelar = new Button
            {
                Text = "Cancelar",
                BorderColor = Color.Black,
                BorderWidth = 1
            };

            verticalStackLayout.Children.Add(enewname);
            verticalStackLayout.Children.Add(horizontalStackLayout);

            horizontalStackLayout.Children.Add(bagregar);
            horizontalStackLayout.Children.Add(bcancelar);

            Content = verticalStackLayout;

            bagregar.Clicked += Bagregar_Clicked;
        }

        async void Bagregar_Clicked(object sender, EventArgs e)
        {
            databaseManager db = new databaseManager();
            mFugitivos fugitivos = new mFugitivos();
            fugitivos.Name = enewname.Text;
            fugitivos.Capturado = false;
            int result = db.insertItem(fugitivos);
            if(result == 1)
            {
                await DisplayAlert("Agregado", "Se ha agregado el fugitivo", "Ok");
                MessagingCenter.Send<Page>(this, "Update");
                await Navigation.PopAsync();
            }

            db.closeConnection();
        }
    }
}
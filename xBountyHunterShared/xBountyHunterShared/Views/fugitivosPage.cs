using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using xBountyHunterShared.Extras;
using xBountyHunterShared.Models;
using System.ComponentModel;

namespace xBountyHunterShared.Views
{
    //public class FugitivosPageVm : INotifyPropertyChanged
    //{
    //    bool IsRefreshing { get; set; }
    //}
    
    public class fugitivosPage : ContentPage
    {
        ListView list = new ListView();

        bool IsRefreshing { get; set; }

        public fugitivosPage()
        {
            Title = "Fugitivos";
            listaFugitivos listaFigitivos = new listaFugitivos();
            list.ItemTemplate = new DataTemplate(typeof(ListViewCell));
            MessagingCenter.Subscribe<Page>(this, "Update", messageCallback);
            list.ItemsSource = listaFigitivos.getFugitivos();
            list.ItemTapped += List_ItemTapped;
            Content = list;

            //BindingContext = new FugitivosPageVm();
            //list.SetBinding(ListView.IsRefreshingProperty, nameof(IsRefreshing));
        }

        void messageCallback(Page obj)
        {
            listaFugitivos listaFugitivos = new listaFugitivos();
            list.ItemsSource = listaFugitivos.getFugitivos();
        }

        void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var fugitivo = e.Item as mFugitivos;
            Navigation.PushAsync(new capturarPage(fugitivo));
        }
    }
}
using System;
using Xamarin.Forms;
using xBountyHunterShared.Extras;
using xBountyHunterShared.Models;
using xBountyHunterShared.ViewModels;

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

        fugitivosVm _vm;

        public fugitivosPage()
        {
            Title = "Fugitivos";

            ActivityIndicator t = new ActivityIndicator();
            t.SetBinding(ActivityIndicator.IsVisibleProperty, nameof(_vm.IsBusy));

            listaFugitivos listaFigitivos = new listaFugitivos();
            list.ItemTemplate = new DataTemplate(typeof(ListViewCell));
            MessagingCenter.Subscribe<Page>(this, "Update", messageCallback);

            _vm = new fugitivosVm();
            BindingContext = _vm;

            list.ItemsSource = listaFigitivos.getFugitivos();
            list.ItemTapped += List_ItemTapped;
            Content = list;

            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                try
                {
                    Device.BeginInvokeOnMainThread(() => 
                    {
                        list.ItemsSource = listaFigitivos.getFugitivos(); 
                    });
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

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
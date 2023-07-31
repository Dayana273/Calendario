using System;
using Xamarin.Forms;
using Calendario.Models.Calendario.Models;
using Calendario.Views;
using SQLite;
using System.Collections.ObjectModel;

namespace Calendario
{
    public partial class MainPage : ContentPage

    {


        private ObservableCollection<Event> events;
        private Event selectedEvent;
        private string dbPath;
        public MainPage()
        {
            InitializeComponent();


            events = new ObservableCollection<Event>();
            eventListView.ItemsSource = events;

            // Establecer el camino de la base de datos SQLite
            dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "events.db3");
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<Event>();
            }
        }
    


        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshEventList();
        }

        private void RefreshEventList()
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                events.Clear();
                var query = connection.Table<Event>().OrderBy(e => e.Date);
                foreach (var ev in query)
                {
                    events.Add(ev);
                }
            }
        }

        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEventPage());
        }

        private async void OnDeleteEventClicked(object sender, EventArgs e)
        {
            if (selectedEvent != null)
            {
                using (var connection = new SQLiteConnection(dbPath))
                {
                    connection.Delete(selectedEvent);
                    events.Remove(selectedEvent);
                }
                await DisplayAlert("Éxito", "Evento eliminado correctamente.", "OK");
            }
        }

        public Event SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
                OnPropertyChanged(nameof(IsEventSelected));
            }
        }

        public bool IsEventSelected => selectedEvent != null;
    }
}

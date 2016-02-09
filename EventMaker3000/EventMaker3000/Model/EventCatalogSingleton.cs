using System;
using System.Collections.ObjectModel;
using EventMaker3000.Persistency;

namespace EventMaker3000.Model
{
    ///7 The responsibley of the eventcatalog singleton is only to maintin the events.
    public class EventCatalogSingleton
    {

        //Creates a private static reference to an instance
        private static EventCatalogSingleton _instance;

        //Constructor
        private EventCatalogSingleton()
        {
            Events = new ObservableCollection<Event>();

            // The purpose of this is to make some instances of events. It creates instances of events and adds it to the observable collection.
            LoadEventAsync();
        }

        //Checks if an instance already exists, if no it will create one. Makes sure we only have one instance
        public static EventCatalogSingleton getInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                _instance = new EventCatalogSingleton();
                return _instance;
            }
        }

        // Creates an observable collection
        public ObservableCollection<Event> Events { get; set; }

        public void AddEvent(Event newEvent)
        {
            Events.Add(newEvent);
            PersistencyService.SaveEventsAsJsonAsync(Events);
        }

        public void AddEvent(int id, string name, string description, string place, DateTime date)
        {
            Events.Add(new Event(id, name, description, place, date));
            PersistencyService.SaveEventsAsJsonAsync(Events);
        }


        public void RemoveEvent(Event myEvent)
        {
            Events.Remove(myEvent);
            PersistencyService.SaveEventsAsJsonAsync(Events);
        }

        public async void LoadEventAsync()
        {

            var events = await PersistencyService.LoadEventsFromJsonAsync();
            if (events != null)
                foreach (var ev in events)
                {
                    Events.Add(ev);
                }

            // Statically adds elements to test the observable collection: Not needed

            //Events.Add(new Event(1, "Football practice", "First practice this year!", "Parken", DateTime.Now + TimeSpan.FromDays(1)));
            //Events.Add(new Event(2, "Bjarkes Birthday", "Surprise party of the year", "Vejnavn 9, 3.th", DateTime.Now + TimeSpan.FromDays(1)));
            //Events.Add(new Event(3, "The Black Keys", "Concert with Jeppe", "Vega", DateTime.Now + TimeSpan.FromDays(1)));
        }
    }
}
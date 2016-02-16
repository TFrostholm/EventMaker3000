using System;
using System.Collections.ObjectModel;
using EventMaker3000.Persistency;

namespace EventMaker3000.Model
{
    /// The responsibility of the eventcatalogsingleton is only to maintain the events.
    public class EventCatalogSingleton
    {

        //Creates a private static reference to an instance
        private static EventCatalogSingleton _instance;

        //Constructor
        private EventCatalogSingleton()
        {
            Events = new ObservableCollection<Event>();
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

        }
    }
}
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using EventMaker3000.Common;
using EventMaker3000.Model;

namespace EventMaker3000.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        public static int IdCounter = 0;
        private int _id;
        private string _name;
        private string _description;
        private string _place;
        private DateTimeOffset _date;
        private TimeSpan _time;
        private ICommand _createEventCommand;
        private ICommand _deleteEventCommand;
        private string _deleteButtonVisibility = "Collapsed";
        private ICommand _changeVisibilityCommand;


        public EventCatalogSingleton EventCatalogSingleton { get; set; }
        public Handler.EventHandler EventHandler { get; set; }

        //Everything we want to be able to bind to in the view should be set to public
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        public string Place
        {
            get { return _place; }
            set { _place = value; OnPropertyChanged(); }
        }

        public DateTimeOffset Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }

        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; OnPropertyChanged(); }
        }

        public static Event SelectedEvent { get; set; }

        //

        public ICommand CreateEventCommand
        {
            get { return _createEventCommand; }
            set { _createEventCommand = value; }
        }
        public ICommand DeleteEventCommand
        {
            get { return _deleteEventCommand; }
            set { _deleteEventCommand = value; }
        }

        public ICommand SelectEventCommand { get; set; }

        public string DeleteButtonVisibility
        {
            get
            {
                return _deleteButtonVisibility;
            }
            set
            {
                _deleteButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeVisibilityCommand
        {
            get
            {
                return _changeVisibilityCommand;
            }
            set
            {
                _changeVisibilityCommand = value;
            }
        }




        public EventViewModel()
        {
            //Initializes Date and Time with some values that are bound to controls.
            DateTime dt = System.DateTime.Now;
            _date = new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, new TimeSpan());
            _time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);

            EventCatalogSingleton = EventCatalogSingleton.getInstance();
            EventHandler = new Handler.EventHandler(this);

            // Creates an instance of the RelayCommand and passes the CreateEvent method as a parameter
            _createEventCommand = new RelayCommand(EventHandler.CreateEvent);

            _deleteEventCommand = new RelayCommand(EventHandler.GetDeleteConfirmationAsync);

            _changeVisibilityCommand=new RelayCommand(EventHandler.ChangeVisibility);

            // Assigns a unique Id to each newly created event. TODO Not working correctly
            Id = System.Threading.Interlocked.Increment(ref IdCounter);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]     string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
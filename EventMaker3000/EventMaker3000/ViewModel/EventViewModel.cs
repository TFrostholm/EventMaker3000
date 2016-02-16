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
        private bool _deleteButtonEnableOrNot = false;
        private ICommand _enableOrNotCommand;

        //TextBlock for no items in listview
        //private string _textBlockVisibility = "Visible";
        //private ICommand _textBlockVisibilityCommand;


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

        // Disable or enable Deletebutton
        public bool DeletebuttonEnableOrNot
        {
            get { return _deleteButtonEnableOrNot; }
            set
            {
                _deleteButtonEnableOrNot = value;
                OnPropertyChanged();
            }
        }

        public ICommand EnableOrNotCommand
        {
            get { return _enableOrNotCommand; }
            set { _enableOrNotCommand = value; }
        }

        // Set TextBlock visibility
        //public string TextBlockVisibility
        //{
        //    get { return _textBlockVisibility; }
        //    set
        //    {
        //        _textBlockVisibility = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public ICommand TextBlockVisibilityCommand
        //{
        //    get { return _textBlockVisibilityCommand; }
        //    set { _textBlockVisibilityCommand = value; }
        //}

        // Constructor
        public EventViewModel()
        {
            //Initializes Date and Time with some values that are bound to controls.
            DateTime dt = System.DateTime.Now;
            _date = new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, new TimeSpan());
            _time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);

            EventCatalogSingleton = EventCatalogSingleton.getInstance();
            EventHandler = new Handler.EventHandler(this);

            // Creates an instance of the RelayCommand and passes necessary method as a parameter
            _createEventCommand = new RelayCommand(EventHandler.CreateEvent);

            _deleteEventCommand = new RelayCommand(EventHandler.GetDeleteConfirmationAsync);

            _enableOrNotCommand = new RelayCommand(EventHandler.EnableOrNot);

            //_textBlockVisibilityCommand = new RelayCommand(EventHandler.TextBlockVisibility);

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]     string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}